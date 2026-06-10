using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "MorseEncoder", menuName = "Scriptable Objects/MorseEncoder")]
public class MorseEncoder : ScriptableObject
{
    /*
  0 = Dot --Dots
  1 = Dash --Dashes
  2 = Character separator --Separates characters of a word
  3 = Word separator --Separates words in a sentence
  */

    private Dictionary<char, string> numericMorseCodes = new()
    {
		//Letters
        { ' ', "3" },
        { 'A', "01" },
        { 'B', "1000" },
        { 'C', "1010" },
        { 'D', "100" },
        { 'E', "0" },
        { 'F', "0010" },
        { 'G', "110" },
        { 'H', "0000" },
        { 'I', "00" },
        { 'J', "0111" },
        { 'K', "101" },
        { 'L', "0100" },
        { 'M', "11" },
        { 'N', "10" },
        { 'O', "111" },
        { 'P', "0110" },
        { 'Q', "1101" },
        { 'R', "010" },
        { 'S', "000" },
        { 'T', "1" },
        { 'U', "001" },
        { 'V', "0001" },
        { 'W', "011" },
        { 'X', "1001" },
        { 'Y', "1011" },
        { 'Z', "1100" },
		
		//Digits
		{ '0', "11111" },
        { '1', "01111" },
        { '2', "00111" },
        { '3', "00011" },
        { '4', "00001" },
        { '5', "00000" },
        { '6', "10000" },
        { '7', "11000" },
        { '8', "11100" },
        { '9', "11110" },
		
		//Punctuation marks
		{ '&', "01000" },
        { '\'', "011110" },
        { '@', "011010" },
        { ')', "101101" },
        { '(', "10110" },
        { ':', "111000" },
        { ',', "110011" },
        { '=', "10001" },
        { '!', "101011" },
        { '.', "010101" },
        { '-', "100001" },
        { '*', "1001" },
        { '%', "11111210010211111" },
        { '+', "01010" },
        { '\"', "010010" },
        { '?', "001100" },
        { '/', "10010" },
        };

    //Encode as numeric string
    public void EncodeNumericMorseString(string textMessage)
    {
        string numericMessage = string.Empty;
		//char messageChar = '\0';
        
        foreach (char messageChar in textMessage)
        {
            if (numericMorseCodes.TryGetValue(messageChar, out string code))
            {
                numericMessage += code + "2";
            }
        }
		
		Debug.Log(numericMessage);
		
    }
}