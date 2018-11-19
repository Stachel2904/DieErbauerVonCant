using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Player[] players;
    public int currentPlayer;

    public GameObject VictoryWindow;

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

    }

    public void StartGame()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].inventory.AddItem("Brick", 4);
            players[i].inventory.AddItem("Wheat", 2);
            players[i].inventory.AddItem("Wood", 4);
            players[i].inventory.AddItem("Wool", 2);

            UpdateInventory(players[i].clientID);
        }
        Debug.Log("START GAME");
        GameObject.Find("ServerManager").GetComponent<NetworkServerMessageHandler>().SendToAllClients("Start");
        Debug.Log("START GAME 2");
        GameObject.Find("ServerManager").GetComponent<NetworkServerMessageHandler>().SendToClient(GamePlay.main.GetCurrentPlayer().clientID, "Go");

        GameBoard.MainBoard.Init();
    }

    public void NextPlayer()
    {
        if (currentPlayer == 2)
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

    public void DistributeRolledRessources(int number)
    {
        for (int i = 0; i < GameBoard.MainBoard.tilesGrid.Length; i++)
        {
            for (int j = 0; j < GameBoard.MainBoard.tilesGrid[i].Length; j++)
            {
                if (GameBoard.MainBoard.tilesGrid[i][j].chipNumber == number)
                {
                    for (int k = 0; k < GameBoard.MainBoard.tilesGrid[i][j].pawns.Length; k++)
                    {
                        if (GameBoard.MainBoard.tilesGrid[i][j].pawns[k] != null)
                        {
                            if (GameBoard.MainBoard.tilesGrid[i][j].pawns[k].type == "Village")
                            {
                                for (int l = 0; l < players.Length; l++)
                                {
                                    if (players[l].color == GameBoard.MainBoard.tilesGrid[i][j].pawns[k].color)
                                    {
                                        players[l].inventory.AddItem(GameBoard.MainBoard.tilesGrid[i][j].resourceName);
                                        UpdateInventory(players[l].clientID);
                                    }
                                }
                            }
                            if (GameBoard.MainBoard.tilesGrid[i][j].pawns[k].type == "Town")
                            {
                                for (int l = 0; l < players.Length; l++)
                                {
                                    if (players[l].color == GameBoard.MainBoard.tilesGrid[i][j].pawns[k].color)
                                    {
                                        players[l].inventory.AddItem(GameBoard.MainBoard.tilesGrid[i][j].resourceName, 2);
                                        UpdateInventory(players[l].clientID);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void UpdateBoard(Pawn buildedPawn, int[] place)
    {
        Field[] usedFields = new Field[place.Length / 3];
        int[] posAtField = new int[place.Length / 3];
        for (int i = 0; i < usedFields.Length * 3; i += 3)
        {
            usedFields[i / 3] = GameBoard.MainBoard.tilesGrid[place[i]][place[i + 1]];
            posAtField[i / 3] = place[i + 2];
        }

        Vector3 pos = GetPosInWorld(usedFields[0], posAtField[0], buildedPawn.type);

        if (buildedPawn.type != "Street")
        {
            GetCurrentPlayer().victoryPoints++;
            GameObject.Find("ServerManager").GetComponent<NetworkServerGUI>().UpdateVictoryPoints(buildedPawn.color);
        }

        //Pawn kreieren (erst nur mesh, dann Farbe, dann position)
        Transform createdPawn = Instantiate(Resources.Load<Transform>("Prefabs/" + buildedPawn.type), GameObject.Find("Board").transform);
        createdPawn.gameObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/" + buildedPawn.color);
        createdPawn.position = pos;

        //Variablen Updaten
        GameBoard.MainBoard.pawns[(int)ConvertColor(buildedPawn.color)].Add(buildedPawn);
        for (int i = 0; i < usedFields.Length; i++)
        {
            if (usedFields[i] != null)
            {
                GameBoard.MainBoard.tilesGrid[usedFields[i].row][usedFields[i].column].pawns[posAtField[i]] = buildedPawn;
            }
        }

        if (buildedPawn.type == "Town")
        {
            for (int i = 0; i < GameObject.Find("Board").transform.childCount; i++)
            {
                GameObject currentPawn = GameObject.Find("Board").transform.GetChild(i).gameObject;

                if (currentPawn.name == "Village(Clone)")
                {
                    if (Vector3.Distance(pos, currentPawn.transform.position) < 0.5f)
                    {
                        GameObject.Destroy(currentPawn);
                    }
                }
            }
        }
        else if (buildedPawn.type == "Street")
        {
            createdPawn.Rotate(0.0f, 30.0f * posAtField[0], 0.0f);
        }

        //Ressourcen Updaten
        GetCurrentPlayer().inventory.RemoveItem(buildedPawn.type);
    }

    public PlayerColor ConvertColor(string color)
    {
        PlayerColor result = PlayerColor.NONE;
        switch (color)
        {
            case "Blue":
                result = PlayerColor.BLUE;
                break;
            case "Red":
                result = PlayerColor.RED;
                break;
            case "Orange":
                result = PlayerColor.ORANGE;
                break;
            case "White":
                result = PlayerColor.WHITE;
                break;
            default:
                Debug.Log("Die zu bauende Spielfigur hat die undefinierte Farbe: " + color);
                break;
        }
        return result;
    }

    private Vector3 GetPosInWorld(Field usedField, int posAtField, string type)
    {
        Vector3 result = new Vector3();

        //Get pos of Field
        result.x = usedField.column * -6 - 6;
        result.y = 0;
        result.z = usedField.row * 10.5f + 8.5f;

        //get Pos from PosAtField
        result += Quaternion.Euler(0, 30 * posAtField, 0) * Vector3.back * ((type == "Street") ? 6.0f : 14.0f / 2.0f);

        return result;
    }

    public void UpdateInventory(int clientID)
    {
        //string stringTemp;

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].clientID == clientID && players[i].clientID != -1)
            {
                
                GameObject.Find("ServerManager").GetComponent<NetworkServerUI>().UpdateClientInventoryGUI(clientID, "Brick", players[i].inventory.inven["Brick"]);
                GameObject.Find("ServerManager").GetComponent<NetworkServerUI>().UpdateClientInventoryGUI(clientID, "Wheat", players[i].inventory.inven["Wheat"]);
                GameObject.Find("ServerManager").GetComponent<NetworkServerUI>().UpdateClientInventoryGUI(clientID, "Ore", players[i].inventory.inven["Ore"]);
                GameObject.Find("ServerManager").GetComponent<NetworkServerUI>().UpdateClientInventoryGUI(clientID, "Wood", players[i].inventory.inven["Wood"]);
                GameObject.Find("ServerManager").GetComponent<NetworkServerUI>().UpdateClientInventoryGUI(clientID, "Wool", players[i].inventory.inven["Wool"]);

                players[i].hand = players[i].inventory.inven["Brick"] + players[i].inventory.inven["Wheat"] + players[i].inventory.inven["Ore"] + players[i].inventory.inven["Wood"] + players[i].inventory.inven["Wool"];
                GameObject.Find("ServerManager").GetComponent<NetworkServerGUI>().UpdateHand(players[i].clientID);
            }

        }
        
       //ToDo: Straßen, Siedlungen, Städte updaten

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
        GamePlay.Main.GetCurrentPlayer().inventory.RemoveItem(givenRessource, 4);
        UpdateInventory(Main.GetCurrentPlayer().clientID);
    }

    /// <summary>
    /// Removes and adds items of the trading Players
    /// </summary>
    /// <param name="tradeOffer"> Contains the struct of the Tradeoffer that has been proposed </param>
    public void Trading(Trade tradeOffer)
    {
        for (int i = 0; i < players.Length; i++)
        {

            if (players[i].color == tradeOffer.giver)
            {
                Main.GetCurrentPlayer().inventory.AddItem("Brick", tradeOffer.askedRessources[0]);
                Main.GetCurrentPlayer().inventory.AddItem("Wheat", tradeOffer.askedRessources[1]);
                Main.GetCurrentPlayer().inventory.AddItem("Ore", tradeOffer.askedRessources[2]);
                Main.GetCurrentPlayer().inventory.AddItem("Wood", tradeOffer.askedRessources[3]);
                Main.GetCurrentPlayer().inventory.AddItem("Wool", tradeOffer.askedRessources[4]);

                Main.GetCurrentPlayer().inventory.RemoveItem("Brick", tradeOffer.givenRessources[0]);
                Main.GetCurrentPlayer().inventory.RemoveItem("Wheat", tradeOffer.givenRessources[1]);
                Main.GetCurrentPlayer().inventory.RemoveItem("Ore", tradeOffer.givenRessources[2]);
                Main.GetCurrentPlayer().inventory.RemoveItem("Wood", tradeOffer.givenRessources[3]);
                Main.GetCurrentPlayer().inventory.RemoveItem("Wool", tradeOffer.givenRessources[4]);

                UpdateInventory(players[i].clientID);
            }

            if (players[i].color == tradeOffer.taker)
            {
                Player taker;

                switch (players[i].color)
                {
                    case "Orange":
                        {
                            taker = main.players[0];
                        }
                        break;
                    case "Blue":
                        {
                            taker = main.players[1];
                        }
                        break;
                    case "White":
                        {
                            taker = main.players[2];
                        }
                        break;
                    case "Red":
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
                    taker.inventory.RemoveItem("Wheat", tradeOffer.askedRessources[1]);
                    taker.inventory.RemoveItem("Ore", tradeOffer.askedRessources[2]);
                    taker.inventory.RemoveItem("Wood", tradeOffer.askedRessources[3]);
                    taker.inventory.RemoveItem("Wool", tradeOffer.askedRessources[4]);

                    UpdateInventory(taker.clientID);

                   
                }
            }
        }
    }
    

    public void GameWon(string color)
    {
        VictoryWindow.SetActive(true);
        VictoryWindow.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = "Player " + color + " Won!";
    }
}
