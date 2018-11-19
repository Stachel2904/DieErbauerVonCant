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

    [SerializeField]
    GameObject text11;

    [SerializeField]
    GameObject text12;

    [SerializeField]
    GameObject text13;

    [SerializeField]
    GameObject text14;

    [SerializeField]
    GameObject text15;

    [SerializeField]
    GameObject text16;

    [SerializeField]
    GameObject text17;

    [SerializeField]
    GameObject text18;

    [SerializeField]
    GameObject text19;

    [SerializeField]
    GameObject text20;

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

    Text showngivenBrickText;
    Text showngivenWheatText;
    Text showngivenOreText;
    Text showngivenWoodText;
    Text showngivenWoolText;

    Text shownaskedBrickText;
    Text shownaskedWheatText;
    Text shownaskedOreText;
    Text shownaskedWoodText;
    Text shownaskedWoolText;

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

        showngivenBrickText = text11.GetComponent<Text>();
        showngivenWheatText = text12.GetComponent<Text>();
        showngivenOreText = text13.GetComponent<Text>();
        showngivenWoodText = text14.GetComponent<Text>();
        showngivenWoolText = text15.GetComponent<Text>();

        shownaskedBrickText = text16.GetComponent<Text>();
        shownaskedWheatText = text17.GetComponent<Text>();
        shownaskedOreText = text18.GetComponent<Text>();
        shownaskedWoodText = text19.GetComponent<Text>();
        shownaskedWoolText = text20.GetComponent<Text>();
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

        showngivenBrickText.text = createdTrade.givenRessources[0].ToString();
        showngivenWheatText.text = createdTrade.givenRessources[1].ToString();
        showngivenOreText.text = createdTrade.givenRessources[2].ToString();
        showngivenWoodText.text = createdTrade.givenRessources[3].ToString();
        showngivenWoolText.text = createdTrade.givenRessources[4].ToString();

        shownaskedBrickText.text = createdTrade.askedRessources[0].ToString();
        shownaskedWheatText.text = createdTrade.askedRessources[1].ToString();
        shownaskedOreText.text = createdTrade.askedRessources[2].ToString();
        shownaskedWoodText.text = createdTrade.askedRessources[3].ToString();
        shownaskedWoolText.text = createdTrade.askedRessources[4].ToString();
    }

    public void StartedTrading()
    {
        createdTrade.giver = GameObject.Find("GamePlay").GetComponent<GamePlayClient>().ownPlayer.name;

        ResetTrade();

        GameObject.Find("ClientManager").GetComponent<NetworkClientMessagerHandler>().SendToServer("Player started Trading");
    }

    public void StoppedTrading()
    {
        GameObject.Find("ClientManager").GetComponent<NetworkClientMessagerHandler>().SendToServer("Player stopped Trading");
        GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().ClientDefault.SetActive(true);
    }

    public void createAskedPlayer(string name)
    {
        createdTrade.taker = name;
    }


    public void inkrementGivenRessource(string name)
    {
        createdTrade.AddGivenRessource(name);
    }

    public void dekrementGivenRessource(string name)
    {
        createdTrade.RemoveGivenRessource(name);
    }

    public void inkrementAskedRessource(string name)
    {
        createdTrade.AddAskedRessource(name);
    }

    public void dekrementAskedRessource(string name)
    {
        createdTrade.RemoveAskedRessource(name);
    }

    public void FinishPlayerTrade()
    {
        GameObject.Find("ClientManager").GetComponent<NetworkClientMessagerHandler>().SendTradeToServer(createdTrade);
        GameObject.Find("ClientManager").GetComponent<NetworkClientMessagerHandler>().SendToServer("Player accepted Trading");
        GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().ClientDefault.SetActive(true);
    }

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

        GameObject.Find("TradeAcception").SetActive(true);
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
    }    
}
