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

    public void UpdateBoard(Pawn buildedPawn, Place destination)
    {
        if(buildedPawn.type != "Street")
        {
            GetCurrentPlayer().victoryPoints++;
            GameObject.Find("ServerManager").GetComponent<NetworkServerGUI>().UpdateVictoryPoints(buildedPawn.color);
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
    public void UpdateInventory(int clientID)
    {
        //string stringTemp;

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].clientID == clientID && players[i].clientID != -1)
            {
                //stringTemp = "Brick|" + players[i].inventory.inven["Brick"].ToString();
                //GameObject.Find("ServerManager").GetComponent<NetworkServerMessageHandler>().SendInventoryToClient(clientID, stringTemp);
                //stringTemp = "Wheat|" + players[i].inventory.inven["Wheat"].ToString();
                //GameObject.Find("ServerManager").GetComponent<NetworkServerMessageHandler>().SendInventoryToClient(clientID, stringTemp);
                //stringTemp = "Ore|" + players[i].inventory.inven["Ore"].ToString();
                //GameObject.Find("ServerManager").GetComponent<NetworkServerMessageHandler>().SendInventoryToClient(clientID, stringTemp);
                //stringTemp = "Wood|" + players[i].inventory.inven["Wood"].ToString();
                //GameObject.Find("ServerManager").GetComponent<NetworkServerMessageHandler>().SendInventoryToClient(clientID, stringTemp);
                //stringTemp = "Wool|" + players[i].inventory.inven["Wool"].ToString();
                //GameObject.Find("ServerManager").GetComponent<NetworkServerMessageHandler>().SendInventoryToClient(clientID, stringTemp);
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
        Debug.Log(" TradeOffer.giver ist " + tradeOffer.giver + " und tradeoffer.taker ist " + tradeOffer.taker);
        for (int i = 0; i < players.Length; i++)
        {

            if (players[i].name == tradeOffer.giver)
            {
                Debug.Log(" Aufgerufen name ist " + players[i].clientID);
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

            if (players[i].name == tradeOffer.taker)
            {
                Player taker;

                switch (players[i].name)
                {
                    case "player1":
                        {
                            taker = main.players[0];
                        }
                        break;
                    case "player2":
                        {
                            taker = main.players[1];
                        }
                        break;
                    case "player3":
                        {
                            taker = main.players[2];
                        }
                        break;
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
        print("Player" + color + "won");
    }
}
