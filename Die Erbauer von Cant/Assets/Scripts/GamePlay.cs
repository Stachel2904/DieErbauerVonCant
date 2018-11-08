using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Players
{
    WHITE,
    BLUE,
    YELLOW,
    RED
}

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

    public Player[] players;
    public int currentPlayer;
    private GameBoard board1 = new GameBoard();


    void Start()
    {
        players = new Player[]
        {
            new Player("Player1", "White"),
            new Player("Player2", "blue"),
            new Player("Player3", "yellow"),
            new Player("Player4", "red")
        };
        currentPlayer = 0;
	}

    public void NextPlayer()
    {

        if (currentPlayer == 3)
        {
            currentPlayer = 0;
        }
        else
        {
            currentPlayer++;
        }
        
    }

    public Player GetCurrentPlayer()
    {
        
        return players[currentPlayer];
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

    public void GameWon(string color)
    {
        print("Player" + color + "won");
    }
}
