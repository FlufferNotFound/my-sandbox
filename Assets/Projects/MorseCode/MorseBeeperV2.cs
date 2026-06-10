using System.Collections;
using System.ComponentModel;
using System.Text;
using UnityEngine;

public class MorseBeeperV2 : MonoBehaviour
{
    /*
	This beeper is compliant with the 1:3:7, Paris, Farnsworth and Wordsworth timing standards.
	*/

    public string textMessage = string.Empty;

    [Range(600, 800)]
    public int transmitionFrequency = 600;

    [Range(1, 30)]
    public int wordsPerMinute = 12;

    //This value cannot be changed while morse code is being played.
    //To change the timing, the beeping must be stopped, and then
    //the timing can be selected.
    public enum timingTypes { Basic, Paris, Farnsworth, Wordsworth }
    public timingTypes timingMethod = timingTypes.Basic;
    public enum waveForms { SineWave, SquareWave, SawtoothWave, TriangleWave }
    public waveForms waveForm = waveForms.SineWave;

    public bool playOnAwake = true;

    public bool loop = false;

    public bool showDebug = false;

    private AudioSource audioSrc;
    private ToneGenerator toneGen;
    private MorseEncoder enc;

    private int dotLenght = 1;
    private int dashLenght = 3;
    private int symGapLenght = 1;
    private int letterLenght = 3;
    private int wordLenght = 7;
    private int[] code = { 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 };

    void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
        toneGen = GetComponent<ToneGenerator>();

        switch (timingMethod)
        {
            case timingTypes.Basic:
                StartCoroutine(BasicBeeper());
                break;

            case timingTypes.Paris:

                break;

            case timingTypes.Farnsworth:

                break;

            case timingTypes.Wordsworth:

                break;
        }
    }

    private IEnumerator BasicBeeper()
    {
        int index = 0;
        int sampleRate = 44100;

        int unitDelay = 0;

        int toneLength = 0;
        int len = 10;

        if (code == null || code.Length == 0)
        {
            Debug.LogWarning("No code to play.");
            yield break;
        }

        while (true)
        {
            Debug.Log("HAHA");
            /*Debug.Log("HAHA");
            if (index == code.Length)
            {
                if (!loop)
                    break;

                index = 0;
            }

            if (!audioSrc.isPlaying)
            {
                switch (code[index])
                {
                    //Dot
                    case 0:

                        break;

                    //Dash
                    case 1:

                        break;
                }

                //audioSrc.clip = toneGen.GenerateSineWaveTone(transmitionFrequency, toneLength, sampleRate);
                audioSrc.Play();
                index++;
            }
            yield return new WaitForSeconds(symGapLenght * len);*/
        }

        yield return null;
    }
}