using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateTrade : MonoBehaviour {

    public Trade createdTrade = new Trade();

    [SerializeField]
    GameObject text1;

    [SerializeField]
    GameObject text2;

    [SerializeField]
    GameObject text3;

    [SerializeField]
    GameObject text4;

    [SerializeField]
    GameObject text5;

    [SerializeField]
    GameObject text6;

    [SerializeField]
    GameObject text7;

    [SerializeField]
    GameObject text8;

    [SerializeField]
    GameObject text9;

    [SerializeField]
    GameObject text10;

    public GameObject playerOneButton;
    public GameObject playerTwoButton;
    public GameObject playerThreeButton;
    public GameObject playerFourButton;

    Text givenBrickText;
    Text givenWheatText;
    Text givenOreText;
    Text givenWoodText;
    Text givenWoolText;

    Text askedBrickText;
    Text askedWheatText;
    Text askedOreText;
    Text askedWoodText;
    Text askedWoolText;

    string temp4to1Ressource;

    public void Start()
    {
        givenBrickText = text1.GetComponent<Text>();
        givenWheatText = text2.GetComponent<Text>();
        givenOreText = text3.GetComponent<Text>();
        givenWoodText = text4.GetComponent<Text>();
        givenWoolText = text5.GetComponent<Text>();

        askedBrickText = text6.GetComponent<Text>();
        askedWheatText = text7.GetComponent<Text>();
        askedOreText = text8.GetComponent<Text>();
        askedWoodText = text9.GetComponent<Text>();
        askedWoolText = text10.GetComponent<Text>();
    }
    private void Update()
    {
        givenBrickText.text = createdTrade.givenRessources[0].ToString();
        givenWheatText.text = createdTrade.givenRessources[1].ToString();
        givenOreText.text = createdTrade.givenRessources[2].ToString();
        givenWoodText.text = createdTrade.givenRessources[3].ToString();
        givenWoolText.text = createdTrade.givenRessources[4].ToString();

        askedBrickText.text = createdTrade.askedRessources[0].ToString();
        askedWheatText.text = createdTrade.askedRessources[1].ToString();
        askedOreText.text = createdTrade.askedRessources[2].ToString();
        askedWoodText.text = createdTrade.askedRessources[3].ToString();
        askedWoolText.text = createdTrade.askedRessources[4].ToString();

        
    }
    /// <summary>
    /// Updates the Text of the Playertrading Ressources texts
    /// </summary>
    public void UpdateTradingRessources()
    {
        GameObject.Find("ClientTextManager").GetComponent<TextManager>().text11.text = createdTrade.givenRessources[0].ToString();
        GameObject.Find("ClientTextManager").GetComponent<TextManager>().text12.text = createdTrade.givenRessources[1].ToString();
        GameObject.Find("ClientTextManager").GetComponent<TextManager>().text13.text = createdTrade.givenRessources[2].ToString();
        GameObject.Find("ClientTextManager").GetComponent<TextManager>().text14.text = createdTrade.givenRessources[3].ToString();
        GameObject.Find("ClientTextManager").GetComponent<TextManager>().text15.text = createdTrade.givenRessources[4].ToString();

        GameObject.Find("ClientTextManager").GetComponent<TextManager>().text16.text = createdTrade.askedRessources[0].ToString();
        GameObject.Find("ClientTextManager").GetComponent<TextManager>().text17.text = createdTrade.askedRessources[1].ToString();
        GameObject.Find("ClientTextManager").GetComponent<TextManager>().text18.text = createdTrade.askedRessources[2].ToString();
        GameObject.Find("ClientTextManager").GetComponent<TextManager>().text19.text = createdTrade.askedRessources[3].ToString();
        GameObject.Find("ClientTextManager").GetComponent<TextManager>().text20.text = createdTrade.askedRessources[4].ToString();
    }
    /// <summary>
    /// Determines what happens when the Player starts Trading
    /// </summary>
    public void StartedTrading()
    {
        createdTrade.giver = GameObject.Find("GamePlay").GetComponent<GamePlayClient>().ownPlayer.color;

        ResetTrade();

        GameObject.Find("ClientManager").GetComponent<NetworkClientMessagerHandler>().SendToServer("Player started Trading");
    }
    /// <summary>
    /// Determines what happens when the Player stops Trading
    /// </summary>
    public void StoppedTrading()
    {
        GameObject.Find("ClientManager").GetComponent<NetworkClientMessagerHandler>().SendToServer("Player stopped Trading");
        GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().ClientDefault.SetActive(true);
    }
    /// <summary>
    /// Saves the Color of the Player that gets the offer
    /// </summary>
    /// <param name="color"> Color of the Player that gets the offer </param>
    public void createAskedPlayer(string color)
    {
        createdTrade.taker = color;
    }

    public void enableActivePlayerButtons()
    {
        playerOneButton.SetActive(true);
        playerTwoButton.SetActive(true);
        playerThreeButton.SetActive(true);
        playerFourButton.SetActive(true);

        if (GameObject.Find("GamePlay").GetComponent<GamePlayClient>().ownPlayer.color == "Orange")
        {
            playerOneButton.SetActive(false);
        }
        if (GameObject.Find("GamePlay").GetComponent<GamePlayClient>().ownPlayer.color == "Blue" || GamePlayClient.Main.maxPlayer == 0)
        {
            playerTwoButton.SetActive(false);
        }
        if (GameObject.Find("GamePlay").GetComponent<GamePlayClient>().ownPlayer.color == "White" || GamePlayClient.Main.maxPlayer <= 1)
        {
            playerThreeButton.SetActive(false);
        }
        if (GameObject.Find("GamePlay").GetComponent<GamePlayClient>().ownPlayer.color == "Red" || GamePlayClient.Main.maxPlayer <= 2)
        {
            playerFourButton.SetActive(false);
        }
        
    }

    /// <summary>
    /// Increase the Ressource the current Player wants to Trade
    /// </summary>
    /// <param name="name"> The name of the Ressource </param>
    public void inkrementGivenRessource(string name)
    {
        createdTrade.AddGivenRessource(name);
    }
    /// <summary>
    /// Decrease the Ressource the current Player wants to Trade
    /// </summary>
    /// <param name="name"> The name of the Ressource </param>
    public void dekrementGivenRessource(string name)
    {
        createdTrade.RemoveGivenRessource(name);
    }
    /// <summary>
    /// Increase the Ressource the current Player wants to get
    /// </summary>
    /// <param name="name"> the name of the Ressource </param>
    public void inkrementAskedRessource(string name)
    {
        createdTrade.AddAskedRessource(name);
    }
    /// <summary>
    /// Decrease the Ressource the current Player wants to get
    /// </summary>
    /// <param name="name"> the name of the Ressource </param>
    public void dekrementAskedRessource(string name)
    {
        createdTrade.RemoveAskedRessource(name);
    }
    /// <summary>
    /// Determines what happens if the Trade ends
    /// </summary>
    public void FinishPlayerTrade()
    {
        GameObject.Find("ClientManager").GetComponent<NetworkClientMessagerHandler>().SendTradeToServer(createdTrade);
        GameObject.Find("ClientManager").GetComponent<NetworkClientMessagerHandler>().SendToServer("Player accepted Trading");
        GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().ClientDefault.SetActive(true);
    }
    /// <summary>
    /// sends the saved Trade to the asked Player
    /// </summary>
    public void tradeAsking()
    {
        GameObject.Find("ClientManager").GetComponent<NetworkClientMessagerHandler>().SendTradeToServer(createdTrade);
    }

    public void declineTrade()
    {
        GameObject.Find("ClientManager").GetComponent<NetworkClientMessagerHandler>().SendToServer("Player declined Trading");
        GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().ClientDefault.SetActive(true);
    }

    public void ResetTrade()
    {
        createdTrade.timesSend = 0;

        createdTrade.givenRessources[0] = 0;
        createdTrade.givenRessources[1] = 0;
        createdTrade.givenRessources[2] = 0;
        createdTrade.givenRessources[3] = 0;
        createdTrade.givenRessources[4] = 0;

        createdTrade.askedRessources[0] = 0;
        createdTrade.askedRessources[1] = 0;
        createdTrade.askedRessources[2] = 0;
        createdTrade.askedRessources[3] = 0;
        createdTrade.askedRessources[4] = 0;


    }



    // Second Player Trading

    public void ShowTrade(Trade tradeoffer)
    {
        createdTrade = tradeoffer;
        createdTrade.timesSend++;

        bool checkBrick = GameObject.Find("GamePlay").GetComponent<GamePlayClient>().ownPlayer.inventory.CheckInventory("Brick", createdTrade.askedRessources[0]);
        bool checkWheat = GameObject.Find("GamePlay").GetComponent<GamePlayClient>().ownPlayer.inventory.CheckInventory("Wheat", createdTrade.askedRessources[1]);
        bool checkOre = GameObject.Find("GamePlay").GetComponent<GamePlayClient>().ownPlayer.inventory.CheckInventory("Ore", createdTrade.askedRessources[2]);
        bool checkWood = GameObject.Find("GamePlay").GetComponent<GamePlayClient>().ownPlayer.inventory.CheckInventory("Wood", createdTrade.askedRessources[3]);
        bool checkWool = GameObject.Find("GamePlay").GetComponent<GamePlayClient>().ownPlayer.inventory.CheckInventory("Wool", createdTrade.askedRessources[4]);

        

        GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().TradeAcception.SetActive(true);
        UpdateTradingRessources();
        //GameObject.Find("TradeAcception").SetActive(true);
        GameObject.Find("TradeAcception").transform.Find("Accept").gameObject.SetActive(false);

        if (checkBrick && checkWheat && checkOre && checkWood && checkWool)
        {
            GameObject.Find("TradeAcception").transform.Find("Accept").gameObject.SetActive(true);
        }
    }

    // Trade 4 : 1

    public void check4to1Inventory()
    {
        if (GameObject.Find("GamePlay").GetComponent<GamePlayClient>().ownPlayer.inventory.inven["Brick"] > 3)
        {
            GameObject.Find("SystemTradeWindow1").transform.Find("Brick").gameObject.SetActive(true);
        }
        if (GameObject.Find("GamePlay").GetComponent<GamePlayClient>().ownPlayer.inventory.inven["Brick"] <= 3)
        {
            GameObject.Find("SystemTradeWindow1").transform.Find("Brick").gameObject.SetActive(false);
        }

        if (GameObject.Find("GamePlay").GetComponent<GamePlayClient>().ownPlayer.inventory.inven["Wheat"] > 3)
        {
            GameObject.Find("SystemTradeWindow1").transform.Find("Wheat").gameObject.SetActive(true);
        }
        if (GameObject.Find("GamePlay").GetComponent<GamePlayClient>().ownPlayer.inventory.inven["Wheat"] <= 3)
        {
            GameObject.Find("SystemTradeWindow1").transform.Find("Wheat").gameObject.SetActive(false);
        }

        if (GameObject.Find("GamePlay").GetComponent<GamePlayClient>().ownPlayer.inventory.inven["Ore"] > 3)
        {
            GameObject.Find("SystemTradeWindow1").transform.Find("Ore").gameObject.SetActive(true);
        }
        if (GameObject.Find("GamePlay").GetComponent<GamePlayClient>().ownPlayer.inventory.inven["Ore"] <= 3)
        {
            GameObject.Find("SystemTradeWindow1").transform.Find("Ore").gameObject.SetActive(false);
        }

        if (GameObject.Find("GamePlay").GetComponent<GamePlayClient>().ownPlayer.inventory.inven["Wood"] > 3)
        {
            GameObject.Find("SystemTradeWindow1").transform.Find("Wood").gameObject.SetActive(true);
        }
        if (GameObject.Find("GamePlay").GetComponent<GamePlayClient>().ownPlayer.inventory.inven["Wood"] <= 3)
        {
            GameObject.Find("SystemTradeWindow1").transform.Find("Wood").gameObject.SetActive(false);
        }

        if (GameObject.Find("GamePlay").GetComponent<GamePlayClient>().ownPlayer.inventory.inven["Wool"] > 3)
        {
            GameObject.Find("SystemTradeWindow1").transform.Find("Wool").gameObject.SetActive(true);
        }
        if (GameObject.Find("GamePlay").GetComponent<GamePlayClient>().ownPlayer.inventory.inven["Wool"] <= 3)
        {
            GameObject.Find("SystemTradeWindow1").transform.Find("Wool").gameObject.SetActive(false);
        }
    }

    public void tradeInRessource4to1(string name)
    {
        temp4to1Ressource = name;
    }

    public void finish4to1Trade(string ressource)
    {
        
        GameObject.Find("ClientManager").GetComponent<NetworkClientMessagerHandler>().SendCreateTradeToServer(temp4to1Ressource, ressource);
        GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().ClientDefault.SetActive(true);
        GameObject.Find("ClientManager").GetComponent<NetworkClientMessagerHandler>().SendToServer("Player stopped Trading");
    }    
}
