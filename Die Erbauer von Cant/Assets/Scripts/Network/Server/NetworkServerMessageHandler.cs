﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class NetworkServerMessageHandler : MonoBehaviour {

    //bool init = false;
    //int slots = 4;
    //public int blockedSlots = 0;

    private void Update() {
        //if(init == true) {
        //    if(blockedSlots == slots) {
        //        NetworkServer.dontListen = true;
        //    }else if(blockedSlots < slots && NetworkServer.dontListen == true) {
        //        NetworkServer.dontListen = false;
        //    }
        //}
    }
    public void InitRecivingMessages() {
        NetworkServer.RegisterHandler(888, ServerReciveMessage);
        NetworkServer.RegisterHandler(889, ReciveTradeMessage);
        NetworkServer.RegisterHandler(MsgType.Connect, ServerOnClientConnect);
        NetworkServer.RegisterHandler(MsgType.Disconnect, ServerOnClientDisconnect);
        //init = true;
    }
    //Connect from Client
    private void ServerOnClientConnect(NetworkMessage _message_) {

        Debug.Log("[Client ID: " + _message_.conn.connectionId + "] Client connected!");

        RectTransform output = Instantiate(GameObject.Find("Window").transform.GetChild(0).gameObject, GameObject.Find("Window").transform).GetComponent<RectTransform>();
        output.GetChild(0).GetComponent<Text>().text = _message_.conn.connectionId.ToString();
        output.Translate(Vector2.down * 30);
        
        //blockedSlots++;
        //Create Player here
    }
    private void ServerOnClientDisconnect(NetworkMessage _message_) {
        Debug.Log("[Client ID: " + _message_.conn.connectionId + "] Client disconnected!");

        GameObject tmp = null;
        for (int i = 1; i < GameObject.Find("Window").transform.childCount; i++)
        {
            if(int.Parse(GameObject.Find("Window").transform.GetChild(i).GetChild(0).gameObject.GetComponent<Text>().text) == _message_.conn.connectionId)
            {
                tmp = GameObject.Find("Window").transform.GetChild(i).gameObject;
            }
        }

        Destroy(tmp);
        //blockedSlots--;
        //Destroying Player here
    }
    //Recive message from Client
    private void ServerReciveMessage(NetworkMessage _message_) {
        int clientID = _message_.conn.connectionId;
        Debug.Log("RECIVED A MESSAGE FROM CLIENT WITH THE ID: " + clientID);
        StringMessage msg = new StringMessage();
        msg.value = _message_.ReadMessage<StringMessage>().value;
        Debug.Log("[Message:] " + msg.value);

        switch (msg.value){
            case "Roll Dice":
                DiceGenerator.Main.DiceRoll();
                break;
        }
    }
    //Recieve trade message from Client
    private void ReciveTradeMessage(NetworkMessage _message_) {
        Debug.Log("RECIVED A TRADEMESSAGE!");
        TradeMessage tradeMSG = new TradeMessage();
        tradeMSG.trade = _message_.ReadMessage<TradeMessage>().trade;
        //Tradestuff here
    }
    //Send to Client
    public void ServerSendToClient(int _ClientID_, string _NetMsg_) {
        StringMessage msg = new StringMessage();
        msg.value = _NetMsg_;
        NetworkServer.SendToClient(_ClientID_, 888, msg);
    }
    //Send trade to Client
    public void SendTradeToClient(int _ClientID_, Trade _trade_) {
        TradeMessage tradeMSG = new TradeMessage();
        tradeMSG.trade = _trade_;
        NetworkServer.SendToClient(_ClientID_, 889, tradeMSG);
    }
}
