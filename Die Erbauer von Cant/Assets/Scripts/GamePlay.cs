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

    /// <summary>
    /// Check if you have enough Ressources and print possible Positions
    /// </summary>
    /// <param name="tryBuildedPawn">The pawn you wish to build</param>
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
        Place[] possiblePlaces = GameBoard.MainBoard.GetAllPositions(buildedPawn);

        if (possiblePlaces.Length == 0)
        {
            Debug.Log("Du kannst nirgendwo ein" + ((buildedPawn.type == "Village") ? " Dorf" : ((buildedPawn.type == "Street") ? "e Straße" : "e Stadt")) + "bauen...");
        }

        for (int i = 0; i < possiblePlaces.Length; i++)
        {
            //create PlaceObject
            Place createdPlace = Instantiate(Resources.Load<Place>("PlacePrefab"), GameObject.Find("Places").transform);
            createdPlace.posAtField = possiblePlaces[i].posAtField;
            createdPlace.usedFields = possiblePlaces[i].usedFields;

            createdPlace.gameObject.transform.position = GetPosInWorld(createdPlace.usedFields[0], createdPlace.posAtField[0]);
        }
    }

    private Vector3 GetPosInWorld(Field usedField, int posAtField)
    {
        Vector3 result = new Vector3();

        //Get pos of Field
        result.x = usedField.row;
        result.y = 0;
        result.z = usedField.column / 2;

        //get Pos from PosAtField
        result += Quaternion.Euler(0, 30, 0) * Vector3.forward;

        return result;
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
        for (int i = 0; i < GameBoard.MainBoard.tiles.Length; i++)
        {
            for (int j = 0; j < destination.usedFields.Length; j++)
            {
                if (GameBoard.MainBoard.tiles[i].Equals(destination.usedFields[j]))
                {
                    GameBoard.MainBoard.tiles[i].pawns[destination.posAtField[j]] = buildedPawn;
                }
            }
        }

        //Pawn kreieren (erst nur mesh, dann Farbe, dann position)
        Transform createdPawn = Instantiate(Resources.Load<Transform>(buildedPawn.type), GameObject.Find("GameBoard").transform);
        createdPawn.gameObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>(buildedPawn.color);
        createdPawn.position = destination.gameObject.transform.position;
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
