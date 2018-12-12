using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    
    bool buildingStarted = false;
    public bool gameStarted = false;

    public Player[] players;
    public Player[] newPlayerOrder;

    public int currentPlayer;
    public int maxPlayer = 3;
    public int orderNumber = 0;
    public int victoryPoints = 5;

    public bool sound = true;

    // for Gamestart Rule
    public bool firstRoundFinished = false;
    bool firstRound = false;
    bool secondRoundFinished = false;
    bool secondRound = false;
    int tempPlayerFirstRound;
    bool tempPlayerFirstRoundInitialised = false;
    int tempPlayerSecondRound;
    bool tempPlayerSecondRoundInitialised = false;
    bool specialStartingcase = false;
    bool secondSpecialStartingcase = false;

    public GameObject VictoryWindow;
    public GameObject StartGameButton;

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
    /// <summary>
    /// Save the Starting Player Dice Roll and compares if other players have rolled the same
    /// </summary>
    /// <param name="clientId"> the id of the player that Rolled his Dice </param>
    /// <param name="DiceRolled"> The number of the DiceRoll </param>
    public void SaveDiceRoll(int clientId, int DiceRolled)
    {
        for (int i = 0; i < maxPlayer + 1; i++)
        {
            if (players[i].clientID == clientId)
            {
                players[i].beginningNumber = DiceRolled;
            }

        }
        if (players[0].beginningNumber != 0 && players[0].clientID == clientId)
        {
            if (players[0].beginningNumber == players[1].beginningNumber)
            {
                players[0].beginningNumber = 0;
                players[0].orderCheck = false;
                players[1].beginningNumber = 0;
                players[1].orderCheck = false;
                RollAgain(players[0].clientID);
                RollAgain(players[1].clientID);
            }
            else if (players[0].beginningNumber == players[2].beginningNumber)
            {
                players[0].beginningNumber = 0;
                players[0].orderCheck = false;
                players[2].beginningNumber = 0;
                players[2].orderCheck = false;
                RollAgain(players[0].clientID);
                RollAgain(players[2].clientID);
            }
            else if (players[0].beginningNumber == players[3].beginningNumber)
            {
                players[0].beginningNumber = 0;
                players[0].orderCheck = false;
                players[3].beginningNumber = 0;
                players[3].orderCheck = false;
                RollAgain(players[0].clientID);
                RollAgain(players[3].clientID);
            }
            else
            {
                players[0].orderCheck = true;
                GameObject.Find("ServerManager").GetComponent<NetworkServerMessageHandler>().SendToClient(clientId, "Roll Waiting");
                GameObject.Find("TextManager").GetComponent<HostTextManager>().Player1DiceText.text = "Number " + players[0].beginningNumber.ToString();
            }
        }
        else if (players[1].beginningNumber != 0 && players[1].clientID == clientId)
        {
            if (players[1].beginningNumber == players[0].beginningNumber)
            {
                players[0].beginningNumber = 0;
                players[0].orderCheck = false;
                players[1].beginningNumber = 0;
                players[1].orderCheck = false;
                RollAgain(players[0].clientID);
                RollAgain(players[1].clientID);
            }
            else if (players[1].beginningNumber == players[2].beginningNumber)
            {
                players[1].beginningNumber = 0;
                players[1].orderCheck = false;
                players[2].beginningNumber = 0;
                players[2].orderCheck = false;
                RollAgain(players[1].clientID);
                RollAgain(players[2].clientID);
            }
            else if (players[1].beginningNumber == players[3].beginningNumber)
            {
                players[1].beginningNumber = 0;
                players[1].orderCheck = false;
                players[3].beginningNumber = 0;
                players[3].orderCheck = false;
                RollAgain(players[1].clientID);
                RollAgain(players[3].clientID);
            }
            else
            {
                players[1].orderCheck = true;
                GameObject.Find("ServerManager").GetComponent<NetworkServerMessageHandler>().SendToClient(clientId, "Roll Waiting");
                GameObject.Find("TextManager").GetComponent<HostTextManager>().Player2DiceText.text = "Number " + players[1].beginningNumber.ToString();
            }
        }
        else if (players[2].beginningNumber != 0 && players[2].clientID == clientId)
        {
            if (players[2].beginningNumber == players[0].beginningNumber)
            {
                players[0].beginningNumber = 0;
                players[0].orderCheck = false;
                players[2].beginningNumber = 0;
                players[2].orderCheck = false;
                RollAgain(players[0].clientID);
                RollAgain(players[2].clientID);
            }
            else if (players[2].beginningNumber == players[1].beginningNumber)
            {
                players[1].beginningNumber = 0;
                players[1].orderCheck = false;
                players[2].beginningNumber = 0;
                players[2].orderCheck = false;
                RollAgain(players[1].clientID);
                RollAgain(players[2].clientID);
            }
            else if (players[2].beginningNumber == players[3].beginningNumber)
            {
                players[2].beginningNumber = 0;
                players[2].orderCheck = false;
                players[3].beginningNumber = 0;
                players[3].orderCheck = false;
                RollAgain(players[2].clientID);
                RollAgain(players[3].clientID);
            }
            else
            {
                players[2].orderCheck = true;
                GameObject.Find("ServerManager").GetComponent<NetworkServerMessageHandler>().SendToClient(clientId, "Roll Waiting");
                GameObject.Find("TextManager").GetComponent<HostTextManager>().Player3DiceText.text = "Number " + players[2].beginningNumber.ToString();
            }
        }
        else if (players[3].beginningNumber != 0 && players[3].clientID == clientId)
        {
            if (players[3].beginningNumber == players[0].beginningNumber)
            {
                players[0].beginningNumber = 0;
                players[0].orderCheck = false;
                players[3].beginningNumber = 0;
                players[3].orderCheck = false;
                RollAgain(players[0].clientID);
                RollAgain(players[3].clientID);
            }
            if (players[3].beginningNumber == players[1].beginningNumber)
            {
                players[1].beginningNumber = 0;
                players[1].orderCheck = false;
                players[3].beginningNumber = 0;
                players[3].orderCheck = false;
                RollAgain(players[1].clientID);
                RollAgain(players[3].clientID);
            }
            if (players[3].beginningNumber == players[2].beginningNumber)
            {
                players[2].beginningNumber = 0;
                players[2].orderCheck = false;
                players[3].beginningNumber = 0;
                players[3].orderCheck = false;
                RollAgain(players[2].clientID);
                RollAgain(players[3].clientID);
            }
            else
            {
                players[3].orderCheck = true;
                GameObject.Find("ServerManager").GetComponent<NetworkServerMessageHandler>().SendToClient(clientId, "Roll Waiting");
                GameObject.Find("TextManager").GetComponent<HostTextManager>().Player4DiceText.text = "Number " + players[3].beginningNumber.ToString();
            }
        }
       
        

        if (maxPlayer == 3)
        {
            if (players[0].orderCheck && players[1].orderCheck && players[2].orderCheck && players[3].orderCheck )
            {
                InitializeNewPlayerOrder();
            }
        }
        if (maxPlayer == 2)
        {
            if (players[0].orderCheck && players[1].orderCheck && players[2].orderCheck)
            {
                InitializeNewPlayerOrder();
            }
        }
        if (maxPlayer == 1)
        {
            if (players[0].orderCheck && players[1].orderCheck)
            {
                InitializeNewPlayerOrder();
            }
        }
        if (maxPlayer == 0)
        {
            if (players[0].orderCheck)
            {
                InitializeNewPlayerOrder();
            }
        }
    }
    /// <summary>
    /// Opens the Roll again Window on the Clients
    /// </summary>
    /// <param name="ClientId"> Id of the player that have the same Dice numbers </param>
    public void RollAgain(int ClientId)
    {
        GameObject.Find("ServerManager").GetComponent<NetworkServerMessageHandler>().SendToClient(ClientId, "Roll Again");
    }
    /// <summary>
    /// Opens the Rolling Window on the Clients to determine startPlayer
    /// </summary>
    public void InitializeGame()
    {
        GameObject.Find("ServerManager").GetComponent<NetworkServerMessageHandler>().SendToAllClients("PlayerOrder");
    }
    /// <summary>
    /// Determine Starting Player from Client Dice Rolls
    /// </summary>
    public void InitializeNewPlayerOrder()
    {
        GameObject.Find("ServerManager").GetComponent<NetworkServerMessageHandler>().SendToAllClients("FinishOrderRoll");
        
        for (int i = 12; i > 1; i--)
        {
            for (int j = 0; j < maxPlayer + 1; j++)
            {
                if (players[j].beginningNumber == i)
                {
                    currentPlayer = j;
                    
                    BeginBuilding();
                    return;
                }
            }
        }
    }
    public bool running = false;
    /// <summary>
    /// Initiate the game with the first 2 Building round before the actual game Starts
    /// </summary>
    public void BeginBuilding()
    {
        if (maxPlayer >= 0)
        {
            GameObject.Find("Player1DiceText").SetActive(false);
        }
        if (maxPlayer >= 1)
        {
            GameObject.Find("Player2DiceText").SetActive(false);
        }
        if (maxPlayer >= 2)
        {
            GameObject.Find("Player3DiceText").SetActive(false);
        }
        if (maxPlayer == 3)
        {
            GameObject.Find("Player4DiceText").SetActive(false);
        }

        GameObject.Find("ServerManager").GetComponent<NetworkServerMessageHandler>().SendToAllClients("Start");
        GameObject.Find("ServerManager").GetComponent<NetworkServerMessageHandler>().SendToClient(GamePlay.main.GetCurrentPlayer().clientID, "FirstRoundGo");
        GameBoard.MainBoard.Init();
        running = true;

        buildingStarted = true;
    }

    
    public void StartGame()
    {
        GameObject.Find("ServerManager").GetComponent<NetworkServerMessageHandler>().SendToClient(GamePlay.main.GetCurrentPlayer().clientID, "Go");
        GameObject.Find("ServerManager").GetComponent<NetworkServerMessageHandler>().SendToAllClients("maxPlayer"+maxPlayer);
        gameStarted = true;
    }

    /// <summary>
    /// Increase the Number of maximum Players in the Host start Option
    /// </summary>
    public void IncreaseMaxPlayer()
    {
        if (maxPlayer < 3)
        {
            maxPlayer++;
            GameObject.Find("ServerManager").GetComponent<NetworkServerMessageHandler>().slots++;
        }
    }
    /// <summary>
    /// Decrease the Number of maximum Players in the Host start Option
    /// </summary>
    public void DecreaseMaxPlayer()
    {
        if (maxPlayer > 0)
        {
            maxPlayer--;
            GameObject.Find("ServerManager").GetComponent<NetworkServerMessageHandler>().slots--;
        }
    }

    public void IncreaseVictoryPointsToWin()
    {
        if (victoryPoints <= 9)
        {
            victoryPoints++;
            GameObject.Find("TextManager").GetComponent<HostTextManager>().VictoryPointText.text = victoryPoints.ToString();
        }
        
    }
    public void DecreaseVictoryPointsToWin()
    {
        if (victoryPoints >= 4)
        {
            victoryPoints--;
            GameObject.Find("TextManager").GetComponent<HostTextManager>().VictoryPointText.text = victoryPoints.ToString();
        }
        
    }

    bool secondRoundStarted = false;
    bool realGameStarted = false;
    /// <summary>
    /// changes the current Player to the next number
    /// </summary>
    public void NextPlayer()
    {
        Debug.Log("Called!");
        if (firstRoundFinished == false && secondRoundFinished == false)
        {
            if (currentPlayer == maxPlayer)
            {
                currentPlayer = 0;
                firstRound = true;
                secondRoundStarted = true;
            }
            else
            {
                currentPlayer++;
            }
        }
        else if (firstRoundFinished && secondRoundFinished == false)
        {
            if (secondRoundStarted || specialStartingcase)
            {
                currentPlayer = tempPlayerFirstRound;
                secondRoundStarted = false;
                specialStartingcase = false;
            }
            else if (currentPlayer == 0)
            {
                currentPlayer = maxPlayer;
                secondRound = true;
                realGameStarted = true;
            }
            else
            {
                currentPlayer--;
            }
        }
        else
        {
            if (realGameStarted || secondSpecialStartingcase)
            {
                currentPlayer = tempPlayerSecondRound;
                realGameStarted = false;
                secondSpecialStartingcase = false;
                StartGame();
            }
            else if (currentPlayer == maxPlayer)
            {
                currentPlayer = 0;
            }
            else
            {
                currentPlayer++;
            }
        }
    }
    /// <summary>
    /// Return the Player who is actually Playing
    /// </summary>
    /// <returns></returns>
    public Player GetCurrentPlayer()
    {
        return players[currentPlayer];
    }

    IEnumerator WaitTimer(float time, string ressourceName, string color)
    {

        yield return new WaitForSeconds(time);

        CreateAnimatedRessource(ressourceName, color);
    }
    /// <summary>
    /// Give the players the Ressource of the Rolled Ressourcenumber
    /// </summary>
    /// <param name="number"> Dice Rolled Number </param>
    public void DistributeRolledRessources(int number)
    {
        if (gameStarted)
        {
            for (int i = 0; i < GameBoard.MainBoard.tilesGrid.Length; i++)
            {
                int counter = 0;
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
                                            StartCoroutine(WaitTimer(0.5f * l + (counter * players.Length * 0.5f), GameBoard.MainBoard.tilesGrid[i][j].resourceName, players[l].color));
                                            //CreateAnimatedRessource(GameBoard.MainBoard.tilesGrid[i][j].resourceName, players[l].color);
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
                                            StartCoroutine(WaitTimer(0.5f * l + (counter * players.Length * 0.5f), GameBoard.MainBoard.tilesGrid[i][j].resourceName, players[l].color));
                                            //CreateAnimatedRessource(GameBoard.MainBoard.tilesGrid[i][j].resourceName, players[l].color);
                                            UpdateInventory(players[l].clientID);
                                        }
                                    }
                                }
                            }
                        }
                        counter++;
                    }
                }
            }
        }
    }
    /// <summary>
    /// Give the players Ressources for their second build Village
    /// </summary>
    public void DistributeSecondVillageRessources()
    {
        
        int counter = 0;
        for (int l = 0; l < GameBoard.MainBoard.pawns[(int)ConvertColor(GetCurrentPlayer().color)][2].GetFields().Length; l++)

        { 
            GetCurrentPlayer().inventory.AddItem(GameBoard.MainBoard.pawns[(int)ConvertColor(GetCurrentPlayer().color)][2].GetFields()[l].resourceName);
            StartCoroutine(WaitTimer(counter, GameBoard.MainBoard.pawns[(int)ConvertColor(GetCurrentPlayer().color)][2].GetFields()[l].resourceName, GetCurrentPlayer().color));
            UpdateInventory(GetCurrentPlayer().clientID);

            counter++;
        }
    }
    /// <summary>
    /// Given Ressource fly to the handcard of the designated Color
    /// </summary>
    /// <param name="ressource"> The name of the Ressource </param>
    /// <param name="playerColor"> The Color of the Player </param>
    public void CreateAnimatedRessource(string ressource, string playerColor)
    {
        
        Transform createdRessource = Instantiate(Resources.Load<Transform>("Prefabs/" + ressource + "Symbol"), GameObject.Find("Window").transform);
        createdRessource.gameObject.GetComponent<RessourceAnimation>().direction = GameObject.Find("Handcards").transform.Find(playerColor).transform.position;
        createdRessource.gameObject.GetComponent<RessourceAnimation>().animatedRessource = ressource;
    }

    public void UpdateBoard(Pawn buildedPawn, int[] place)
    {
        Field[] usedFields = new Field[place.Length / 3];
        int[] posAtField = new int[place.Length / 3];
        Debug.Log("You startet building a " + buildedPawn.color + " " + buildedPawn.type);
        Debug.Log("At following positions:");
        for (int i = 0; i < usedFields.Length * 3; i += 3)
        {
            usedFields[i / 3] = GameBoard.MainBoard.tilesGrid[place[i]][place[i + 1]];

            posAtField[i / 3] = place[i + 2];
        }

        if (usedFields.Length == 0 || posAtField.Length == 0)
        {
            Debug.LogError("Irgendwas ist bei Bauen eines Pawns fehlgeschlagen...");
            return;
        }

        Vector3 pos = GetPosInWorld(usedFields[0], posAtField[0], buildedPawn.type);

        if (buildedPawn.type != "Street")
        {
            GetCurrentPlayer().AddVictoryPoints();
        }

        //Pawn kreieren (erst nur mesh, dann Farbe, dann position)
        Transform createdPawn = Instantiate(Resources.Load<Transform>("Prefabs/" + buildedPawn.type + "_HQ"), GameObject.Find("Board").transform);
        createdPawn.gameObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/" + buildedPawn.color);
        createdPawn.position = new Vector3(pos.x, createdPawn.position.y, pos.z);

        if (buildedPawn.type == "Street")
        {
            GameObject.Find("SoundManager").GetComponent<HostSoundManager>().PlaySound("buildingStreet");
        }
        if (buildedPawn.type == "Village")
        {
            GameObject.Find("SoundManager").GetComponent<HostSoundManager>().PlaySound("buildingVillage");
        }
        if (buildedPawn.type == "Town")
        {
            GameObject.Find("SoundManager").GetComponent<HostSoundManager>().PlaySound("buildingCity");
        }

        //Variablen Updaten
        GameBoard.MainBoard.pawns[(int)ConvertColor(buildedPawn.color)].Add(buildedPawn);
        for (int i = 0; i < usedFields.Length; i++)
        {
            if (usedFields[i] != null)
            {
                Pawn oldPawn = GameBoard.MainBoard.tilesGrid[usedFields[i].row][usedFields[i].column].pawns[posAtField[i]];
                GameBoard.MainBoard.tilesGrid[usedFields[i].row][usedFields[i].column].pawns[posAtField[i]] = buildedPawn;
                if(oldPawn != null)
                {
                    GameBoard.MainBoard.pawns[(int)ConvertColor(oldPawn.color)].Remove(oldPawn);
                }
            }
        }

        if (buildedPawn.type == "Town")
        {
            for (int i = 0; i < GameObject.Find("Board").transform.childCount; i++)
            {
                GameObject currentPawn = GameObject.Find("Board").transform.GetChild(i).gameObject;

                if (currentPawn.name == "Village_HQ(Clone)")
                {
                    if (Vector3.Distance(pos, currentPawn.transform.position) < 1.0f)
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
        if(gameStarted)
        {
            GetCurrentPlayer().inventory.RemoveItem(buildedPawn.type);
        }
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

        for (int i = 0; i < maxPlayer + 1; i++)
        {
            if(color == players[i].color)
            {
                result = (PlayerColor)i;
            }
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
    /// <summary>
    /// Updates the Inventory of the given Player
    /// </summary>
    /// <param name="clientID"> The Player which inventory should be updated </param>
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

                GameObject.Find("ServerManager").GetComponent<NetworkServerUI>().UpdateClientInventoryGUI(clientID, "Street", players[i].inventory.inven["Street"]);
                GameObject.Find("ServerManager").GetComponent<NetworkServerUI>().UpdateClientInventoryGUI(clientID, "Village", players[i].inventory.inven["Village"]);
                GameObject.Find("ServerManager").GetComponent<NetworkServerUI>().UpdateClientInventoryGUI(clientID, "Town", players[i].inventory.inven["Town"]);

                players[i].hand = players[i].inventory.inven["Brick"] + players[i].inventory.inven["Wheat"] + players[i].inventory.inven["Ore"] + players[i].inventory.inven["Wood"] + players[i].inventory.inven["Wool"];
                GameObject.Find("ServerManager").GetComponent<NetworkServerGUI>().UpdateHand(players[i].clientID);
                UpdateHand(players[i]);
                break;
            }
        }        
       //ToDo: Straßen, Siedlungen, Städte updaten
    }

    //Handkarten Anzeige ändern
    private void UpdateHand(Player player)
    {
        Transform parent = GameObject.Find("Handcards").transform.Find(player.color).transform;

        //first, delete all old Cards
        for (int i = 0; i < parent.childCount; i++)
        {
            GameObject.Destroy(parent.GetChild(i).gameObject);
        }

        for (int i = 0; i < player.hand; i++)
        {
            RectTransform newHandCard = GameObject.Instantiate(Resources.Load<RectTransform>("Prefabs/CardPrefab"), parent);

            newHandCard.Rotate(0, 0, 150 / player.hand * i - 75);
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
    
    /// <summary>
    /// What happens when Victory conditions are met, called in Player atm
    /// </summary>
    /// <param name="color"> The color of the Player who Won </param>
    public void GameWon(string color)
    {
        string pName = "";
        for (int i = 0; i < players.Length; i++)
        {
           if(players[i].color == color)
           {
                pName = players[i].name;
           }
        }
        GameObject.Find("SoundManager").GetComponent<HostSoundManager>().PlaySound("victory");
        VictoryWindow.SetActive(true);
        GameObject.Find("TextManager").GetComponent<HostTextManager>().WinText.text = "Player " + pName + " Won!";
        GameObject.Find("ServerManager").GetComponent<NetworkServerMessageHandler>().SendToAllClients("ServerFull");
        GameObject.Find("ServerManager").GetComponent<NetworkServerUI>().KillServer();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("main");
    }

    private void Update()
    {
        if (buildingStarted)
        {
            if (firstRoundFinished == false && secondRoundFinished == false)
            {
                if (tempPlayerFirstRoundInitialised == false)
                {
                    if (currentPlayer != 0)
                    {
                        tempPlayerFirstRound = currentPlayer - 1;
                    }
                    else
                    {
                        tempPlayerFirstRound = maxPlayer;
                        firstRound = true;
                        specialStartingcase = true;
                    }
                    tempPlayerFirstRoundInitialised = true;
                }

                if (firstRound && tempPlayerFirstRound == currentPlayer)
                {
                    firstRoundFinished = true;
                }
            }

            if (firstRoundFinished && secondRoundFinished == false)
            {
                if (tempPlayerSecondRoundInitialised == false)
                {
                    currentPlayer = tempPlayerFirstRound;
                    if (currentPlayer != maxPlayer)
                    {
                        tempPlayerSecondRound = currentPlayer + 1;
                    }
                    else
                    {
                        tempPlayerSecondRound = 0;
                        secondRound = true;
                        secondSpecialStartingcase = true;
                    }
                    tempPlayerSecondRoundInitialised = true;
                }

                if (secondRound && tempPlayerSecondRound == currentPlayer)
                {
                    secondRoundFinished = true;
                }
            }

            if (secondRoundFinished)
            {
                buildingStarted = false;
            }
        }




        if (GameObject.Find("Debugging") != null)
        {
            for (int i = 0; i < GameBoard.MainBoard.tilesGrid.Length; i++)
            {
                for (int j = 0; j < GameBoard.MainBoard.tilesGrid[i].Length; j++)
                {
                    int pawns = 0;
                    if (GameBoard.MainBoard.tilesGrid[i][j] == null)
                    {
                        GameObject.Find("Debugging").transform.GetChild(i).GetChild(j).gameObject.GetComponent<Text>().text = "";
                        continue;
                    }
                    for (int k = 0; k < GameBoard.MainBoard.tilesGrid[i][j].pawns.Length; k++)
                    {
                        if (GameBoard.MainBoard.tilesGrid[i][j].pawns[k] != null)
                        {
                            pawns++;
                        }
                    }
                    GameObject.Find("Debugging").transform.GetChild(i).GetChild(j).gameObject.GetComponent<Text>().text = pawns.ToString();
                }
            }
        }

        if (GameObject.Find("ServerManager").GetComponent<NetworkServerMessageHandler>().blockedSlots == maxPlayer + 1)
        {
            StartGameButton.SetActive(true);
        }
        else
        {
            StartGameButton.SetActive(false);
        }
    }

    public void switchSound(bool soundStatus)
    {
        if (soundStatus)
        {
            sound = true;
            GameObject.Find("TextManager").GetComponent<HostTextManager>().SoundText.text = "ON";
        }
        else
        {
            sound = false;
            GameObject.Find("TextManager").GetComponent<HostTextManager>().SoundText.text = "OFF";
        }
    }
}
