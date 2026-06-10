using UnityEngine;

public class Blinkers : MonoBehaviour
{
    //This array holds all the blinkers
    //so one script can control a lot of them
    public GameObject[] blinkers;

    //List of colors for the blinkers
    public Color[] blinkerColors;

    //Duration of the blink effect
    public float blinkDuration = 0.2f;

    //Random color
    private Color rndColor;

    //Store the length of the blinkerColors
    private int blinkerLength;

    //This function is called when the script instance is loaded
    //to initialize anything, like these variables down there
    void Awake()
    {
        blinkerLength = blinkerColors.Length;

        //This one calls Blink, sleeps and repat
        InvokeRepeating(nameof(Blink), 0f, blinkDuration);
    }

    void Blink()
    {
        for(int i = 0; i < blinkers.Length; i++)
        {
            //Get a random color from the blinkerColors array
            rndColor = blinkerColors[Random.Range(0, blinkerLength)];

            //Change emission color
            blinkers[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", rndColor);

            //Change base color, or in Roblox BrickColor
            blinkers[i].GetComponent<Renderer>().material.color = rndColor;
        }
    }
}
