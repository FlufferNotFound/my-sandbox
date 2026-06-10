using UnityEngine;

public class ToneGenerator : MonoBehaviour
{
    public AudioClip GenerateSineWaveTone(int frequency, float milisecondDuration, int sampleRate)
    {
        int samples = (int)(sampleRate * milisecondDuration);
        AudioClip clip = AudioClip.Create("SineWave", samples, 1, sampleRate, false);
        float[] data = new float[samples];
        float increment = frequency * 2f * Mathf.PI / sampleRate;
        float phase = 0f;

        for (int i = 0; i < samples; i++)
        {
            data[i] = Mathf.Sin(phase) * 0.3f;
            phase += increment;
        }

        clip.SetData(data, 0);
        return clip;
    }

    public AudioClip GenerateSquareWaveTone(int frequency, float milisecondDuration, int sampleRate)
    {
        int samples = (int)(sampleRate * milisecondDuration);
        AudioClip clip = AudioClip.Create("SquareWave", samples, 1, sampleRate, false);
        float[] data = new float[samples];

        float increment = frequency * 2f * Mathf.PI / sampleRate;
        float phase = 0f;
        float amplitude = 0.3f;

        for (int i = 0; i < samples; i++)
        {
            data[i] = Mathf.Sin(phase) >= 0f ? amplitude : -amplitude;

            phase += increment;

            if (phase > 2f * Mathf.PI)
                phase -= 2f * Mathf.PI;
        }

        clip.SetData(data, 0);
        return clip;
    }

    public AudioClip GenerateSawtoothWaveTone(int frequency, float milisecondDuration, int sampleRate)
    {
        int samples = (int)(sampleRate * milisecondDuration);
        AudioClip clip = AudioClip.Create("SawtoothWave", samples, 1, sampleRate, false);
        float[] data = new float[samples];

        float phase = 0f;
        float increment = (float)frequency / sampleRate;

        for (int i = 0; i < samples; i++)
        {
            data[i] = ((phase * 2f) - 1f) * 0.3f;

            phase += increment;

            if (phase >= 1f)
                phase -= 1f;
        }

        clip.SetData(data, 0);
        return clip;
    }

    public AudioClip GenerateTriangleWaveTone(int frequency, float milisecondDuration, int sampleRate)
    {
        int samples = (int)(sampleRate * milisecondDuration);
        AudioClip clip = AudioClip.Create("TriangleWave", samples, 1, sampleRate, false);
        float[] data = new float[samples];

        float phase = 0f;
        float increment = (float)frequency / sampleRate;

        for (int i = 0; i < samples; i++)
        {
            // Triangle wave: range -1 to 1
            float value = 2f * Mathf.Abs(2f * phase - 1f) - 1f;

            data[i] = value * 0.3f;

            phase += increment;

            // Wrap phase to [0,1)
            if (phase >= 1f)
                phase -= 1f;
        }

        clip.SetData(data, 0);
        return clip;
    }
}