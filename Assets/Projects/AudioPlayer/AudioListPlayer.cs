
using System.Collections;
using System.ComponentModel;
using UnityEngine;

[System.Serializable]
public class AudioTrack
{
    [Tooltip("Audio clip to play")]
    public AudioClip track;

    [Tooltip("Volume to play the track")]
    [Range(0.0f, 100.0f)]
    [DefaultValue(100.0f)]
    public float volume = 100.0f;
}

public class AudioListPlayer : MonoBehaviour
{

    [Tooltip("Adds a time delay between audio clips")]
    [DefaultValue(false)]
    public bool playWithDelay = false;

    [Tooltip("Minimum time delay before playing the next clip")]
    [DefaultValue(0.0f)]
    public float minDelayTime = 0.0f;

    [Tooltip("Maximum time delay before playing the next clip")]
    [DefaultValue(25.0f)]
    public float maxDelayTime = 25.0f;

    [Tooltip("Audio clip list with properties")]
    public AudioTrack[] AudioTrack;

    [Tooltip("Show debugging info")]
    [DefaultValue(false)]
    public bool showDebug = false;

    private AudioTrack audioList;

    [HideInInspector]
    public AudioSource audioSource;

    private bool isDelayed = false;

    private int audioIndex;

    private float trackDuration;

    private float delayTime;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = true;
        audioSource.loop = false;

        StartCoroutine(PlayNextTrack());
    }


    private IEnumerator PlayNextTrack()
    {
        if (playWithDelay == false)
        {
            if (!audioSource.isPlaying)
            {
                audioList = SelectClip();
                audioSource.clip = audioList.track;
                audioSource.volume = (audioList.volume * 1) / 100;
                audioSource.Play();
            }
        }
        else
        {
            if (!audioSource.isPlaying && !isDelayed)
            {
                StartCoroutine(PlayWithDelay());
            }
        }

        if (showDebug)
        {
            ShowDebugInfo();
        }

        trackDuration = audioSource.clip.length;

        yield return new WaitForSeconds(trackDuration);
    }

    private IEnumerator PlayWithDelay()
    {
        isDelayed = true;

        delayTime = Random.Range(minDelayTime, maxDelayTime);

        yield return new WaitForSeconds(delayTime);

        audioList = SelectClip();

        audioSource.clip = audioList.track;
        audioSource.volume = (audioList.volume * 1) / 100;
        audioSource.Play();

        isDelayed = false;
    }

    private AudioTrack SelectClip()
    {
        //Check if there's tracks in the list
        if (AudioTrack == null || AudioTrack.Length == 0)
        {
            Debug.LogError("Audio Clip list is empty!");
            return null;
        }
        else
        {
            //Grab a random track and return it
            audioIndex = Random.Range(0, AudioTrack.Length);
            return AudioTrack[audioIndex];
        }
    }

    void ShowDebugInfo()
    {
        Debug.LogFormat(
            "Currently playing track: {0}\n" +
            "Audio length: {1}\n" +
            "At index: {2}\n" +
            "At volume: {3}%\n" +
            "Delayed play: {4}\n" +
            "Is waiting: {5}\n" +
            "Time delay: {6}\n" +
            "Min delay time: {7}\n" +
            "Max delay time: {8}",
            audioSource.clip.name,
            audioSource.clip.length,
            audioIndex,
            audioList.volume,
            playWithDelay,
            isDelayed,
            delayTime,
            minDelayTime,
            maxDelayTime
        );

    }
}
