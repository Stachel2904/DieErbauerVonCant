using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum PlayerColor {BLUE, RED, ORANGE, WHITE}

public class Player
{
    public Inventory inventory = new Inventory();
    public string name;
    public string color;
    public int clientID = -1;
    public string avatar = "DEFAULT";
    public int victoryPoints = 0;
    public int hand = 0;

    public Player(string _name, string _color)
    {
        name = _name;
        color = _color;
    }
	/// <summary>
    /// inkrement Victory Point by 1, if the player has 10 points, call the Win Function
    /// </summary>
	public void AddVictoryPoints()
    {
        victoryPoints++;
        GameObject.Find("NetworkServerManager").GetComponent<NetworkServerGUI>().UpdateVictoryPoints(color);
        if (victoryPoints == 10)
        {
            GamePlay.Main.GameWon(color);
        }
       
    }
	
}
