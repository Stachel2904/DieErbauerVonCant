using UnityEngine;
using UnityEngine.UI;

public class DiceGenerator : MonoBehaviour
{
    //public Image[] diceOutputImage;
    public Text[] diceOutputImage;

    public Sprite[] diceSprites = new Sprite[6];
    

    /// <summary>
    /// Roll The Dice!
    /// </summary>
    /// <returns>
    /// return the complete result from 2 - 12
    /// </returns>
    public void DiceRoll()
    {
        int result = 0;

        for (int i = 0; i < 2; i++)
        {
            int number = GetNumber();

            Print(number, i);

            result += number;
        }

        Debug.Log("You rolled a " + result);

        //or if ready spread the ressources
        ResourceManager.SpreadResources();
    }

    //Get rolled Number
    private int GetNumber()
    {
        int rolledNumber = 0;

        rolledNumber = Random.Range(1, 6);

        return rolledNumber;
    }

    //Print rolled Number
    private void Print(int output, int diceNumber)
    {

        diceOutputImage[diceNumber].text = output.ToString();

        //if textures are ready:
        //
        //diceOutputImage.sprite = diceSprites[output - 1];
    }
}
