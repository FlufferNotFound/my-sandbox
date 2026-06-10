/*
LICENSE:
Do what the heck you wish to do with this, I don't care.
*/

using System.Collections;
using System.ComponentModel;
using System.Text;
using UnityEngine;

public class MorseBeeper : MonoBehaviour
{
    [Header("Playback settings")]

    [Tooltip("Frequency for morse code dots in Hertz (Hz).")]
    [Range(0, 30)]
    [SerializeField]
    public int wordsPerMinute = 20;

    [Tooltip(
    "Frequency for each word separator of a sentence in Hertz (Hz)."
)]
    [Range(0, 4000)]
    [SerializeField]
    public int monoTone = 800;

    [Tooltip("Allows the code to loop indefinetly, as long as this value is true.")]
    [DefaultValue(false)]
    [SerializeField]
    public bool useMonoTone = false;

    [Tooltip("Allows the code to loop indefinetly, as long as this value is true.")]
    [DefaultValue(false)]
    [SerializeField]
    public bool loop = false;

    [Tooltip("Plays the morse code when the scene is loaded and opened.")]
    [DefaultValue(false)]
    [SerializeField]
    public bool playOnAwake = true;

    [Header("Debugging options")]
    [Tooltip("Shows debugging options.")]
    [DefaultValue(false)]
    [SerializeField]
    public bool showDebug = false;

    [HideInInspector]
    private float dotLenght = 1; // Dot

    [HideInInspector]
    public float dashLength = 3; //Dash

    [HideInInspector]
    public float symbolGap = 1; //Space between each Dot/Dash

    [HideInInspector]
    public float letterGap = 3; //Character separator

    [HideInInspector]
    public float wordGap = 7; //Word separator

    [HideInInspector]
    private int[] code;

    [HideInInspector]
    private AudioSource beeperSource;

    [HideInInspector]
    private ToneGenerator toneGenerator;

    void Awake()
    {
        beeperSource = GetComponent<AudioSource>();
        toneGenerator = GetComponent<ToneGenerator>();

        SetCode(new int[] { 0, 0, 0, 2, 1, 1, 1, 2, 0, 0, 0 });

        if (playOnAwake)
        {
            StartCoroutine(CodeBeeper());
        }
    }

    public void PlayMorseCode()
    {
        StartCoroutine(CodeBeeper());
    }

    public void StopMorseCode()
    {
        StopCoroutine(CodeBeeper());
    }

    public void SetCode(int[] newCode)
    {
        code = newCode;
    }

    public int[] GetCode()
    {
        return code;
    }

    private IEnumerator CodeBeeper()
    {
        int index = 0;
        int sampleRate = 44100;

        if (code == null || code.Length == 0)
        {
            Debug.LogWarning("No code to play.");
            yield break;
        }

        while (true)
        {
            if (index == code.Length)
            {
                if (!loop)
                    break;

                index = 0;
            }

            if (!beeperSource.isPlaying)
            {
                switch (code[index])
                {
                    //Dot
                    case 0:
                        beeperSource.clip = toneGenerator.GenerateSineWaveTone(
                            monoTone,
                            dotLenght / wordsPerMinute,
                            sampleRate
                        );
                        break;

                    //Dash
                    case 1:
                        beeperSource.clip = toneGenerator.GenerateSineWaveTone(
                            monoTone,
                            dashLength / wordsPerMinute,
                            sampleRate
                        );
                        break;

                    //Letter Separator
                    case 2:
                        /*beeperSource.clip = toneGenerator.GenerateSineWaveTone(
							monoTone,
							letterGap / wordsPerMinute,
							sampleRate
                               );*/
                        break;

                    //Word Separator
                    case 3:
                        /*beeperSource.clip = toneGenerator.GenerateSineWaveTone(
                            monoTone,
                            wordGap / wordsPerMinute,
                            sampleRate
                        );*/
                        break;

                    //Wtf
                    default:
                        Debug.LogFormat("Invalid '{0}' code.", code[index]);
                        break;
                }

                beeperSource.Play();
                index++;
            }

            yield return new WaitForSeconds(symbolGap / wordsPerMinute);
        }
    }
}