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

        int[] numbers = new int[2];

        for (int i = 0; i < 2; i++)
        {
            int number = GetNumber();
            numbers[i] = number;
            result += number;
        }

        Print(numbers);
        GamePlay.Main.DistributeRolledRessources(result);
    }

    private void Print(int[] numbers)
    {
        for (int i = 1; i <= 2; i++)
        {
            GameObject.Find("Window").transform.Find("Dice").Find("Image").Find(i.ToString()).gameObject.GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("Sprites/Würfel")[(numbers[i - 1] - 1)];
        }

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
