using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    
    Dictionary<string, Player> players = new Dictionary<string, Player>();
    GameBoard board1 = new GameBoard();

    // Use this for initialization
    void Start ()
    {
        Player player1 = new Player("Player1", "White");
        Player player2 = new Player("Player2", "blue");
        Player player3 = new Player("Player3", "yellow");
        Player player4 = new Player("Player4", "red");

        players.Add("white", player1);
        players.Add("blue", player2);
        players.Add("yellow", player3);
        players.Add("red", player4);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
