using UnityEngine;
using UnityEngine.UI;

public class DiceGenerator
{
    private static DiceGenerator main;
    public static DiceGenerator Main
    {
        get
        {
            if (main == null)
            {
                main = new DiceGenerator();
            }
            return main;
        }
    }

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
            
            result += number;
        }

        Print(result);
        GamePlay.Main.DistributeRolledRessources(result);


    }

    private void Print(int number)
    {
        GameObject.Find("Window").transform.Find("Dice").Find("Image").Find("Text").gameObject.GetComponent<Text>().text = number.ToString();

    }

    //Get rolled Number
    private int GetNumber()
    {
        int rolledNumber = 0;

        rolledNumber = Random.Range(1, 6);

        return rolledNumber;
    }

    public void GetOrderRoll(int clientId)
    {
        int result = 0;

        for (int i = 0; i < 2; i++)
        {
            int number = GetNumber();

            result += number;
        }
        
        GamePlay.Main.SaveDiceRoll(clientId, result);

    }
}
