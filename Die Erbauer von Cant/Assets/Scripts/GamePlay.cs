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

    public Pawn buildedPawn;
    public Player[] players;
    public int currentPlayer;
    private GameBoard mainBoard = new GameBoard();

    // Trading Variables
    public bool dealAccepted = false;
    

    void Start()
    {
        players = new Player[]
        {
            new Player(1, "Player1", "White"),
            new Player(2, "Player2", "blue"),
            new Player(3, "Player3", "yellow"),
            new Player(4, "Player4", "red")
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

    public void TryBuild(Pawn tryBuildedPawn)
    {
        //set buildedPawn back to null
        if (tryBuildedPawn == null)
        {
            buildedPawn = null;
            return;
        }

        //Ressourcen überprüfen
        if (!GetCurrentPlayer().inventory.CheckInventory(tryBuildedPawn.type))
        {
            Debug.Log("You have not enough Ressources...");
            return;
        }

        buildedPawn = tryBuildedPawn;

        //Alle möglichen Positionen ausgeben
        mainBoard.GetAllPositions(buildedPawn);     
    }

    public void buildPawn(Place destination)
    {
        if(buildedPawn == null)
        {
            Debug.Log("Which pawn do you want to build?");
            return;
        }

        //Ressourcenmanagement/Rohstoffe abziehen etc.
        GetCurrentPlayer().inventory.RemoveItem(buildedPawn.type);

        //An die richtige Position setzen und die angrenzenden Tiles updaten
        for (int i = 0; i < mainBoard.tiles.Length; i++)
        {
            for (int j = 0; j < destination.usedFields.Length; j++)
            {
                if (mainBoard.tiles[i].Equals(destination.usedFields[j]))
                {
                    mainBoard.tiles[i].pawns[destination.posAtField[j]] = buildedPawn;
                }
            }
        }
    }

    // TRADING //
    /// <summary>
    /// Call this if the Player wants to trade 4 : 1 with the System, beforehand you need to check if enough ressources are available
    /// </summary>
    /// <param name="givenRessource"> The string of the 4 Ressource the player wants to trade in </param>
    /// <param name="wantedRessource"> The string of the 1 Ressource the player wants to get </param>
    public void tradeSystem4to1(string givenRessource, string wantedRessource)
    {
        

        foreach (KeyValuePair<string, int> item in main.GetCurrentPlayer().inventory.inven)
        {
            if (item.Key == givenRessource)
            {
                 main.GetCurrentPlayer().inventory.RemoveItem(givenRessource, 4);
            }

            if (item.Key == wantedRessource)
            {
                main.GetCurrentPlayer().inventory.AddItem(wantedRessource, 1);
            }
           
        }
        
    }


    /// <summary>
    /// Make a trade offer with another player
    /// </summary>
    /// <param name="tradeOffer"> Contains the struct of the Tradeoffer that has been proposed </param>
    public void Trytrade(Trade tradeOffer)
    {
        

        // if deal was Accepted
        if (dealAccepted)
        {
            Trading(tradeOffer);
            dealAccepted = false;
        }
    }

    /// <summary>
    /// Removes and adds items of the trading Players
    /// </summary>
    /// <param name="tradeOffer"> Contains the struct of the Tradeoffer that has been proposed </param>
    public void Trading(Trade tradeOffer)
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].name == tradeOffer.giver)
            {
                for (int j = 0; j < tradeOffer.givenRessources.Length; j++)
                {
                    Main.GetCurrentPlayer().inventory.RemoveItem(tradeOffer.givenRessources[i], 1);
                }
                for (int j = 0; j < tradeOffer.givenRessources.Length; j++)
                {
                    Main.GetCurrentPlayer().inventory.AddItem(tradeOffer.askedRessources[i], 1);
                }
            }

            if (players[i].name == tradeOffer.taker)
            {
                Player taker;

                switch (players[i].name)
                {
                    case "player1":
                        {
                            taker = main.players[0];
                        } break;
                    case "player2":
                        {
                            taker = main.players[1];
                        } break;
                    case "player3":
                        {
                            taker = main.players[2];
                        } break;
                    case "player4":
                        {
                            taker = main.players[3];
                        }
                        break;

                    default:
                        taker = null;
                        break;
                }
                if (taker != null)
                {
                    
                    for (int j = 0; j < tradeOffer.givenRessources.Length; j++)
                    {
                        taker.inventory.RemoveItem(tradeOffer.askedRessources[i], 1);
                    }
                    for (int j = 0; j < tradeOffer.givenRessources.Length; j++)
                    {
                        taker.inventory.AddItem(tradeOffer.givenRessources[i], 1);
                    }
                }
                

            }
        }
    }

    public void GameWon(string color)
    {
        print("Player" + color + "won");
    }
}
