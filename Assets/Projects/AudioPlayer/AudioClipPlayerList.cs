/*
 This audio clip player script is designed to give complete control over the audios clips played.
One thing it doesn't do for now is a curve editor to control the volume rolloff, but will be added later.
 */

using System;
using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

[System.Serializable]
public class ClipSettings
{
    [SerializeField]
    [Tooltip("Ignores this clip and skips to the previous or next clip on the list.")]
    [DefaultValue(false)]
    public bool ignoreThisClip = false;

    [SerializeField]
    [Tooltip("Mutes this clip but still plays.")]
    [DefaultValue(false)]
    public bool muteThisClip = false;

    [SerializeField]
    [Tooltip("Allows this clip to bypass any effects.")]
    [DefaultValue(false)]
    public bool canBypassEffects = false;

    [SerializeField]
    [Tooltip("Allows this clip to bypass any listener effects.")]
    [DefaultValue(false)]
    public bool canBypassListenerEffects = false;

    [SerializeField]
    [Tooltip("Allows this clip to bypass any reverb zones")]
    [DefaultValue(false)]
    public bool canBypassReverbZone = false;

    //Later
    //[SerializeField]
    //[Tooltip("Allows this clip to loop")]
    //[DefaultValue(false)]
    //public bool canLoop = false;

    [SerializeField]
    [Tooltip("Allows this clip to override the priority of this object's AudioSource")]
    [DefaultValue(false)]
    public bool canOverrideAudioSourcePriority = false;

    [SerializeField]
    [Tooltip("Sets this object's AudioSource priority.\nNote that sounds with larger priorities are likely to be stolen by those with smaller priorities")]
    [Range(0, 256)]
    [DefaultValue(128)]
    public int clipPriority = 128;

    [SerializeField]
    [Tooltip("Sets the volume to play this clip at.\nNote that this value is shown in the Inspector as 0 to 100 range, but this is normalized internally, as Unity handles volumes in a 0.0 to 1.0 range.")]
    [Range(0f, 100f)]
    [DefaultValue(100f)]
    public float clipVolume = 100f;

    [SerializeField]
    [Tooltip("Sets the pitch to play this clip at.")]
    [Range(-3f, 3f)]
    [DefaultValue(1f)]
    public float clipPitch = 1f;

    [SerializeField]
    [Tooltip("Sets the stereo pan to play this clip at." +
        "\nMore negative values make the clip be more audible on the left sound channel, while more posiitve values make the clip be more audible on the right audio channel. Values at zero make the clip audible on both channels.")]
    [Range(-1f, 1f)]
    [DefaultValue(0f)]
    public float clipStereoPan = 0f;

    [SerializeField]
    [Tooltip("Sets the spatial blend to play this clip at. This controls how the AudioSource behaves in 3D space and how it affects the audio clip it's playing. See documentation for more information.")]
    [Range(0f, 1f)]
    [DefaultValue(0f)]
    public float clipSpatialBlend = 0f;

    [SerializeField]
    [Tooltip("Sets the amount of the output signal that gets routed to the reverb zones. The amount is linear in the 0 to 1 range, but allows for a 10 dB amplification in the 1 to 1.1 range which can be useful to achieve the effect of near-field and distant sounds.")]
    [Range(0, 1.1f)]
    [DefaultValue(1f)]
    public float clipReverbZoneMix = 1f;
}

//Later
[System.Serializable]
public class SpatialAudioSettings
{
    public float clipDopplerLevel;

    public float clipSpread;

    public float clipMinDistance;

    public float clipMaxDistance;
}

[System.Serializable]
public class PlayBackSettings
{
    [SerializeField]
    [Tooltip("Starts playing audio when the scene launches. Playback methods are called in Start()")]
    [DefaultValue(false)]
    public bool playOnStart = false;

    [SerializeField]
    [Tooltip("Plays the clips in random order")]
    public bool playRandomOrder = false;

    [SerializeField]
    [Tooltip("Adds a delay before playing the next clip")]
    [DefaultValue(false)]
    public bool playWithDelay = false;

    [SerializeField]
    [Tooltip("Time delay before playing next clip")]
    [DefaultValue(0f)]
    public float delayBeforePlay = 0f;

    [SerializeField]
    [Tooltip("Adds a random delay between a range, before playing the next clip")]
    [DefaultValue(false)]
    public bool playWithRandomDelay = false;

    [SerializeField]
    [Tooltip("Minimum time delay before playing the next clip")]
    [DefaultValue(0f)]
    public float minDelayBeforePlay = 0f;

    [SerializeField]
    [Tooltip("Maximum time delay before playing the next clip")]
    [DefaultValue(1f)]
    public float maxDelayBeforePlay = 1f;
}

[System.Serializable]
public class AudioClips
{
    [SerializeField]
    [Tooltip("The audio clip to be played by this object's audio source")]
    [DefaultValue(null)]
    public AudioClip audioClip = null;

    public AudioMixerGroup audioMixerGroup;

    [SerializeField]
    [Tooltip("Settings for each audio clip. Note that each clip's values cannot be changed once a clip is being played.")]
    [DefaultValue(null)]
    public ClipSettings clipSettings = null;

    //[SerializeField]
    //public SpatialAudioSettings spatialSettings;

    [SerializeField]
    public bool isPlaying = false;
}

public class AudioClipPlayerList : MonoBehaviour
{
    [SerializeField]
    public bool showBasicDebug = false;

    [SerializeField]
    public bool showDetailedDebug = false;

    [SerializeField]
    public PlayBackSettings playBackSettings;

    [SerializeField]
    public AudioClips[] audioClipList;

    [HideInInspector]
    private AudioSource audioSource;

    [HideInInspector]
    private int originalPriority;

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();

        originalPriority = audioSource.priority;

        if (audioSource == null)
        {
            //Should this stay like this or throw a null reference exception instead?
            throw new System.Exception(string.Format("GameObject '{0}' has no AudioSource attached to it!", gameObject.name));
        }

        if (audioClipList.Length == 0)
        {
            throw new System.Exception("Audio clip list is empty!");
        }
    }

    private void Start()
    {
        if (audioClipList.Length != 0)
        {
            if (playBackSettings.playOnStart)
            {
                if (playBackSettings.playRandomOrder)
                {
                    StartCoroutine(PlayInRandomOrder());
                }
                else
                {
                    StartCoroutine(PlayInOrder());
                }
            }
        }
    }

    private AudioClip GetClipAtIndex(int index)
    {
        AudioClip selectedClip = audioClipList[index].audioClip;

        //if (showDetailedDebug) ShowDetailedDebug();

        if (index > audioClipList.Length)
        {
            //If the index is out of bounds, throw an out of bounds error and return null
            return null;
            throw new System.IndexOutOfRangeException(string.Format("Index '{0}' doesn't exist!", index));
        }
        else
        {
            return selectedClip;
        }
    }

    //Plays clips in order
    public IEnumerator PlayInOrder()
    {

        int index = 0;

        float trackLength = 0f;

        while (true)
        {
            //Check if the clip ignoreThisClip
            if (audioClipList[index].clipSettings.ignoreThisClip)
            {
                //Skip to the next index
                index++;

                //To prevent out of bounds errors.
                if (index == audioClipList.Length)
                {
                    index = 0;
                }

                trackLength = GetClipAtIndex(index).length;

                //Tower of hell.
                audioSource.clip = GetClipAtIndex(index);

                audioSource.outputAudioMixerGroup = audioClipList[index].audioMixerGroup;

                audioSource.mute = audioClipList[index].clipSettings.muteThisClip;

                audioSource.bypassEffects = audioClipList[index].clipSettings.canBypassEffects;

                audioSource.bypassListenerEffects = audioClipList[index].clipSettings.canBypassListenerEffects;

                audioSource.bypassReverbZones = audioClipList[index].clipSettings.canBypassReverbZone;

                //audioSource.loop = audioClipList[index].clipSettings.canLoop;

                if (audioClipList[index].clipSettings.canOverrideAudioSourcePriority)
                {
                    audioSource.priority = audioClipList[index].clipSettings.clipPriority;
                }
                else
                {
                    audioSource.priority = originalPriority;
                }

                audioSource.volume = (audioClipList[index].clipSettings.clipVolume * 1) / 100;

                audioSource.pitch = audioClipList[index].clipSettings.clipPitch;

                audioSource.panStereo = audioClipList[index].clipSettings.clipStereoPan;

                audioSource.spatialBlend = audioClipList[index].clipSettings.clipSpatialBlend;

                audioSource.reverbZoneMix = audioClipList[index].clipSettings.clipReverbZoneMix;

                audioSource.Play();

                audioClipList[index].isPlaying = true;

                if (showBasicDebug) ShowSimpleDebug(GetClipAtIndex(index), index, audioSource.volume);

                yield return new WaitForSeconds(trackLength);

                audioClipList[index].isPlaying = false;

                index++;
            }

        }
    }

    //Plays clips in random order
    public IEnumerator PlayInRandomOrder()
    {

        int index = 0;

        float trackLength = 0f;

        while (true)
        {

            //if (audioClipList[index].useCustomSettings)
            //{
            //    audioSource.mute = audioClipList[index].clipSettings.muteThisClip;
            //    audioSource.priority = audioClipList[index].clipSettings.canOverrideAudioPriority ? audioClipList[index].clipSettings.clipPriority : audioSource.priority;
            //    audioSource.volume = audioClipList[index].clipSettings.clipVolume / 100f;
            //    audioSource.pitch = audioClipList[index].clipSettings.clipPitch;
            //    audioSource.panStereo = audioClipList[index].clipSettings.clipStereoPan;
            //    audioSource.spatialBlend = audioClipList[index].clipSettings.clipSpatialBlend;
            //    audioSource.reverbZoneMix = audioClipList[index].clipSettings.clipReverbZoneMix;
            //    audioSource.bypassEffects = audioClipList[index].clipSettings.canBypassEffects;
            //    audioSource.bypassListenerEffects = audioClipList[index].clipSettings.canBypassListenerEffects;
            //    audioSource.bypassReverbZones = audioClipList[index].clipSettings.canBypassReverbZone;
            //    audioSource.loop = audioClipList[index].clipSettings.canLoop;
            //}

            index = UnityEngine.Random.Range(0, audioClipList.Length);

            trackLength = GetClipAtIndex(index).length;

            audioSource.clip = GetClipAtIndex(index);

            audioSource.Play();

            audioClipList[index].isPlaying = true;

            if (showBasicDebug) ShowSimpleDebug(GetClipAtIndex(index), index, audioSource.volume);

            yield return new WaitForSeconds(trackLength);

            audioClipList[index].isPlaying = false;
        }
    }

    void ShowSimpleDebug(AudioClip clip, int index, float volume)
    {
        Debug.LogFormat("Playing '{0}', index '{1}', volume '{2}'", clip.name, index, volume);
    }

    //void ShowDetailedDebug(AudioClip clip, int index, float volume)
    //{
    //    Debug.LogFormat("Playing '{0}', index '{1}', volume '{2}'", clip.name, index, volume);
    //}

    public void StopPlayback()
    {
        StopCoroutine(PlayInOrder());
        StopCoroutine(PlayInRandomOrder());
    }
}
