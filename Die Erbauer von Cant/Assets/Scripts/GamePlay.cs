using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Players
{
    WHITE,
    BLUE,
    ORANGE,
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
                main = GameObject.Find("GamePlay").GetComponent<GamePlay>();
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
            new Player("Player1", "Orange"),
            new Player("Player2", "Blue"),
            new Player("Player3", "White"),
            new Player("Player4", "Red")
        };
        currentPlayer = 0;
        // Delete this // Debugging stuff
        Main.GetCurrentPlayer().inventory.AddItem("Brick", 5);

        //Create Board
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                GameBoard.MainBoard.tilesGrid[i][j] = new Field("", 3);
                GameBoard.MainBoard.tilesGrid[i][j].row = i;
                GameBoard.MainBoard.tilesGrid[i][j].column = j;
            }
        }

        //Set startPawns
        GameBoard.MainBoard.tilesGrid[2][4].pawns[3] = new Pawn("Street", "Orange");
        GameBoard.MainBoard.tilesGrid[2][6].pawns[9] = GameBoard.MainBoard.tilesGrid[2][4].pawns[3];
        GameBoard.MainBoard.pawns[(int)PlayerColor.ORANGE].Add(GameBoard.MainBoard.tilesGrid[2][4].pawns[3]);

        GameBoard.MainBoard.tilesGrid[2][4].pawns[5] = new Pawn("Street", "Orange");
        GameBoard.MainBoard.tilesGrid[3][5].pawns[11] = GameBoard.MainBoard.tilesGrid[2][4].pawns[5];
        GameBoard.MainBoard.pawns[(int)PlayerColor.ORANGE].Add(GameBoard.MainBoard.tilesGrid[2][4].pawns[5]);

    }

    public void DebugBuild()
    {
        TryBuild("Village");
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
    public void TryBuild(string type)
    {
        //set buildedPawn back to null
        if (type == "")
        {
            buildedPawn = null;
            return;
        }

        //Ressourcen überprüfen
        //if (!GetCurrentPlayer().inventory.CheckInventory(type))
        //{
        //    Debug.Log("You have not enough Ressources...");
        //    return;
        //}

        buildedPawn = new Pawn(type, GetCurrentPlayer().color);

        //Alle möglichen Positionen ausgeben
        Place[] possiblePlaces = GameBoard.MainBoard.GetAllPositions(buildedPawn);

        if (possiblePlaces.Length == 0)
        {
            Debug.Log("Du kannst nirgendwo ein" + ((buildedPawn.type == "Village") ? " Dorf" : ((buildedPawn.type == "Street") ? "e Straße" : "e Stadt")) + " bauen...");
        }

        for (int i = 0; i < possiblePlaces.Length; i++)
        {
            //create PlaceObject
            Place createdPlace = Instantiate(Resources.Load<Place>("Prefabs/PossiblePlace"), GameObject.Find("Places").transform);
            createdPlace.posAtField = possiblePlaces[i].posAtField;
            createdPlace.usedFields = possiblePlaces[i].usedFields;
            
            createdPlace.gameObject.transform.position = GetPosInWorld(createdPlace.usedFields[0], createdPlace.posAtField[0]);
        }
    }

    private Vector3 GetPosInWorld(Field usedField, int posAtField)
    {
        Vector3 result = new Vector3();

        //Get pos of Field
        result.x = usedField.column * -6 + 6;
        result.y = 0;
        result.z = usedField.row * 6.93f + (3.46f + 6.93f / 2);

        //get Pos from PosAtField
        result += Quaternion.Euler(0, 30 * posAtField, 0) * Vector3.back * ((buildedPawn.type == "Street") ? 6.0f : 16.86f / 2.0f);

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
        for (int i = 0; i < GameBoard.MainBoard.tilesGrid.Length; i++)
        {
            for (int j = 0; j < GameBoard.MainBoard.tilesGrid[i].Length; j++)
            {
                for (int k = 0; k < destination.usedFields.Length; k++)
                {
                    if (GameBoard.MainBoard.tilesGrid[i][j].Equals(destination.usedFields[k]))
                    {
                        GameBoard.MainBoard.tilesGrid[i][j].pawns[destination.posAtField[k]] = buildedPawn;
                    }
                }
            }
        }

        //Pawn kreieren (erst nur mesh, dann Farbe, dann position)
        Transform createdPawn = Instantiate(Resources.Load<Transform>("Prefabs/" + buildedPawn.type), GameObject.Find("Board").transform);
        createdPawn.gameObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/" + buildedPawn.color);
        createdPawn.position = destination.gameObject.transform.position;

        //Places löschen
        for (int i = 0; i < GameObject.Find("Places").transform.childCount; i++)
        {
            GameObject.Destroy(GameObject.Find("Places").transform.GetChild(i).gameObject);
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

        GamePlay.Main.GetCurrentPlayer().inventory.AddItem(wantedRessource);
        string stringTemp = wantedRessource+"|" + Main.GetCurrentPlayer().inventory.inven[wantedRessource].ToString();
        GameObject.Find("ClientManager").GetComponent<NetworkServerMessageHandler>().SendInventoryToClient(Main.GetCurrentPlayer().clientID, stringTemp);
        GamePlay.Main.GetCurrentPlayer().inventory.RemoveItem(givenRessource, 4);
        stringTemp = givenRessource + "|" + Main.GetCurrentPlayer().inventory.inven[givenRessource].ToString();
        GameObject.Find("ClientManager").GetComponent<NetworkServerMessageHandler>().SendInventoryToClient(Main.GetCurrentPlayer().clientID, stringTemp);
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
        string stringTemp;
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].name == tradeOffer.giver)
            {
                Main.GetCurrentPlayer().inventory.AddItem("Brick", tradeOffer.askedRessources[0]);
                Main.GetCurrentPlayer().inventory.AddItem("Wheat", tradeOffer.askedRessources[1]);
                Main.GetCurrentPlayer().inventory.AddItem("Ore", tradeOffer.askedRessources[2]);
                Main.GetCurrentPlayer().inventory.AddItem("Wood", tradeOffer.askedRessources[3]);
                Main.GetCurrentPlayer().inventory.AddItem("Wool", tradeOffer.askedRessources[4]);

                Main.GetCurrentPlayer().inventory.RemoveItem("Brick", tradeOffer.givenRessources[0]);
                stringTemp = "Brick|" + players[i].inventory.inven["Brick"].ToString();
                GameObject.Find("ClientManager").GetComponent<NetworkServerMessageHandler>().SendInventoryToClient(players[i].clientID, stringTemp);
                Main.GetCurrentPlayer().inventory.RemoveItem("Wheat", tradeOffer.givenRessources[1]);
                stringTemp = "Wheat|" + players[i].inventory.inven["Wheat"].ToString();
                GameObject.Find("ClientManager").GetComponent<NetworkServerMessageHandler>().SendInventoryToClient(players[i].clientID, stringTemp);
                Main.GetCurrentPlayer().inventory.RemoveItem("Ore", tradeOffer.givenRessources[2]);
                stringTemp = "Ore|" + players[i].inventory.inven["Ore"].ToString();
                GameObject.Find("ClientManager").GetComponent<NetworkServerMessageHandler>().SendInventoryToClient(players[i].clientID, stringTemp);
                Main.GetCurrentPlayer().inventory.RemoveItem("Wood", tradeOffer.givenRessources[3]);
                stringTemp = "Wood|" + players[i].inventory.inven["Wood"].ToString();
                GameObject.Find("ClientManager").GetComponent<NetworkServerMessageHandler>().SendInventoryToClient(players[i].clientID, stringTemp);
                Main.GetCurrentPlayer().inventory.RemoveItem("Wool", tradeOffer.givenRessources[4]);
                stringTemp = "Wool|" + players[i].inventory.inven["Wool"].ToString();
                GameObject.Find("ClientManager").GetComponent<NetworkServerMessageHandler>().SendInventoryToClient(players[i].clientID, stringTemp);
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
                    taker.inventory.AddItem("Brick", tradeOffer.givenRessources[0]);
                    taker.inventory.AddItem("Wheat", tradeOffer.givenRessources[1]);
                    taker.inventory.AddItem("Ore", tradeOffer.givenRessources[2]);
                    taker.inventory.AddItem("Wood", tradeOffer.givenRessources[3]);
                    taker.inventory.AddItem("Wool", tradeOffer.givenRessources[4]);

                    taker.inventory.RemoveItem("Brick", tradeOffer.askedRessources[0]);
                    stringTemp = "Brick|" + taker.inventory.inven["Brick"].ToString();
                    GameObject.Find("ClientManager").GetComponent<NetworkServerMessageHandler>().SendInventoryToClient(taker.clientID, stringTemp);
                    taker.inventory.RemoveItem("Wheat", tradeOffer.askedRessources[1]);
                    stringTemp = "Wheat|" + taker.inventory.inven["Wheat"].ToString();
                    GameObject.Find("ClientManager").GetComponent<NetworkServerMessageHandler>().SendInventoryToClient(taker.clientID, stringTemp);
                    taker.inventory.RemoveItem("Ore", tradeOffer.askedRessources[2]);
                    stringTemp = "Ore|" + taker.inventory.inven["Ore"].ToString();
                    GameObject.Find("ClientManager").GetComponent<NetworkServerMessageHandler>().SendInventoryToClient(taker.clientID, stringTemp);
                    taker.inventory.RemoveItem("Wood", tradeOffer.askedRessources[3]);
                    stringTemp = "Wood|" + taker.inventory.inven["Wood"].ToString();
                    GameObject.Find("ClientManager").GetComponent<NetworkServerMessageHandler>().SendInventoryToClient(taker.clientID, stringTemp);
                    taker.inventory.RemoveItem("Wool", tradeOffer.askedRessources[4]);
                    stringTemp = "Wool|" + taker.inventory.inven["Wool"].ToString();
                    GameObject.Find("ClientManager").GetComponent<NetworkServerMessageHandler>().SendInventoryToClient(taker.clientID, stringTemp);

                    //for (int j = 0; j < tradeOffer.givenRessources.Length; j++)
                    //{
                    //    //taker.inventory.RemoveItem(tradeOffer.askedRessources[i], 1);
                    //}
                    //for (int j = 0; j < tradeOffer.givenRessources.Length; j++)
                    //{
                    //    //taker.inventory.AddItem(tradeOffer.givenRessources[i], 1);
                    //}
                }
            }
        }
    }

    public void GameWon(string color)
    {
        print("Player" + color + "won");
    }
}
