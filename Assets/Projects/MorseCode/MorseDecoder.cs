using UnityEngine;

public class MorseDecoder : MonoBehaviour
{
    public string msg;

    private MorseDictionaries morseDictionaries = new MorseDictionaries();

    private void Awake()
    {
        DecodeNumericMorseToTextString(msg);
        DecodeMorseTextToTextString(msg);
    }

    //Encodes a text string to a numeric morse string.
    public void DecodeNumericMorseToTextString(string numericMorse)
    {
        string numMorseStr = string.Empty;

        for (int i = 0; i < numericMorse.Length; i++)
        {

            ////Java has charAt, why not C#? 
            //if (morseDictionaries.numericMorseCodes.TryGetValue(s.ToUpper()[i], out string numericCode))
            //{
            //    //if there's a character infront, add a character separator

            //    //if (!(i++ == i))
            //    //{
            //    //    numMorseStr += numericCode;
            //    //}
            //    //else
            //    //{
            //    //    numMorseStr += numericCode + "2";
            //    //}

            //    numMorseStr += numericCode + "2";
            //}

            if (morseDictionaries.numericMorseCodes.TryGetValue(numericMorse.ToUpper()[i], out string numericCode))
            {



            }
        }

        //TODO: Hack fix, untill I find a better way to add character separators.
        //return numMorseStr.Remove(numMorseStr.Length - 1);
        //numMorseStr.Remove(numMorseStr.Length - 1);

        //Debug.Log(numMorseStr.Remove(numMorseStr.Length - 1));

    }

    public void DecodeMorseTextToTextString(string morseText)
    {
        string morseTextStr = string.Empty;

        for (int i = 0; i < morseText.Length; i++)
        {

            //Java has charAt, why not C#? 
            if (morseDictionaries.textMorseCodes.TryGetValue(morseText.ToUpper()[i], out string numericCode))
            {
                //if there's a character infront, add a character separator

                //if (!(i++ == i))
                //{
                //    numMorseStr += numericCode;
                //}
                //else
                //{
                //    numMorseStr += numericCode + " ";
                //}

                morseTextStr += numericCode + " ";
            }
        }

        //TODO: Hack fix, untill I find a better way to add character separators.
        //return numMorseStr.Remove(numMorseStr.Length - 1);
        //numMorseStr.Remove(numMorseStr.Length - 1);

        Debug.Log(morseTextStr.Remove(morseTextStr.Length - 1));
    }
}