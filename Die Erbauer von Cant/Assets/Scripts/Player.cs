using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public Inventory inventory = new Inventory();
    string name;
    string color;

    private int victoryPoints = 0;

    public Player(string _name, string _color)
    {
        name = _name;
        color = _color;
    }
	/// <summary>
    /// inkrement Victory Point by 1, if the player has 10 points, call the Win Function
    /// </summary>
	public void GetVictoryPoints()
    {
        victoryPoints++;

        if (victoryPoints == 10)
        {
            GamePlay.Main.GameWon(color);
        }
       
    }
	
}
