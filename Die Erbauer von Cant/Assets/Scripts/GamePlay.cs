using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    //Singleton
    private static GamePlay main;
    public static GamePlay Main
    {
        get
        {
            if(main == null)
            {
                main = new GamePlay();
            }
            return main;
        }
    }

    private Dictionary<string, Player> players = new Dictionary<string, Player>();
    private GameBoard board1 = new GameBoard();
    
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

    void PrintAllPossiblePositions(Pawn buildedPawn)
    {
        //Ressourcen überprüfen

        //Alle möglichen Positionen ausgeben

        //Ressourcenmanagement/Rohstoffe abziehen etc.
    }

    void buildPawn(Place destination, Pawn buildedPawn)
    {

    }
}
