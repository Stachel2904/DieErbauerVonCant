using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class NetworkClientMessagerHandler : MonoBehaviour {

    private NetworkClient client;

    public void InitClientMessages(NetworkClient _client_) {
        client = _client_;
        client.RegisterHandler(888, ReciveMessageFromServer);
        client.RegisterHandler(889, ReciveTradeMessage);
        client.RegisterHandler(891, ReciveInventoryMessage);
        client.RegisterHandler(892, ReciveFieldUpdateMessage);
        client.RegisterHandler(894, ReciveFieldUpdateMessage2);
        client.RegisterHandler(MsgType.Connect, OnConnect);
        client.RegisterHandler(MsgType.Disconnect, OnDisconnect);
    }
    private void OnConnect(NetworkMessage _message_) {
        Debug.Log("Client is connected: " + client.isConnected);
        Debug.Log("Server: " + client.serverIp);
        GetComponent<NetworkClientGUI>().DeactivateSearchServerPanel();
        GameObject.Find("Window").transform.Find("ClientDefault").gameObject.SetActive(true);
    }
    private void OnDisconnect(NetworkMessage _message_) {
        Debug.Log("Client is connected: " + client.isConnected);
        Debug.Log("Client is disconnected!");
        SceneManager.LoadScene("main");
    }
    //Send to Server
    public void SendToServer(string _command_) {
        if (client.isConnected) {
            NetMessage netMSG = new NetMessage();
            netMSG.command = _command_;
            bool success = client.Send(888, netMSG);
            if (!success) {
                Debug.LogError("Failed to send message!");
            }
        }
        else {
            Debug.LogError("Client is not connected! Failed to send message!");
        }
    }
    //TRADE
    public void SendTradeToServer(Trade _trade_) {
        if (client.isConnected) {
            TradeMessage tradeMSG = new TradeMessage();
            tradeMSG.trade = _trade_;
            bool success = client.Send(889, tradeMSG);
            if (!success) {
                Debug.LogError("Failed to send trademessage!");
            }
        }
        else {
            Debug.LogError("Client is not connected! Failed to send trademessage!");
        }
    }
    public void SendCreateTradeToServer(string _resource1_, string _resource2_) {
        if (client.isConnected) {
            CreateTradeMessage tradeMSG = new CreateTradeMessage();
            tradeMSG.ressource = _resource1_ + "|" + _resource2_;
            bool success = client.Send(893, tradeMSG);
            if (!success) {
                Debug.LogError("Failed to send trademessage!");
            }
        }
        else {
            Debug.LogError("Client is not connected! Failed to send trademessage!");
        }
    }

    //FIELD
    public void SendFieldUpdateToServer(string _pawn_, string _color_, int[] _place_) {
        if (client.isConnected) {
            FieldMessage fieldMSG = new FieldMessage();
            FieldMessage2 fieldMSG2 = new FieldMessage2();
            fieldMSG.pawn = _pawn_ + "|" + _color_;
            fieldMSG2.place = _place_;
            bool success = client.Send(892, fieldMSG);
            if (!success) {
                Debug.LogError("Failed to send fieldupdatemessage!");
            }
            success = client.Send(894, fieldMSG2);
            if (!success) {
                Debug.LogError("Failed to send fieldupdatemessage!");
            }
        }
        else {
            Debug.LogError("Client is not connected! Failed to send fieldupdatemessage!");
        }
    }
    public void SendNameToServer(string _name_) {
        if (client.isConnected) {
            NetMessage netMSG = new NetMessage();
            netMSG.command = _name_;
            bool success = client.Send(895, netMSG);
            if (!success) {
                Debug.LogError("Failed to send namemessage!");
            }
        }
        else {
            Debug.LogError("Client is not connected! Failed to send namemessage!");
        }
    }
    //Recive from Server
    private void ReciveMessageFromServer(NetworkMessage _message_) {
        Debug.Log("RECIVED A MESSAGE!");
        _message_.reader.SeekZero();
        switch (_message_.ReadMessage<NetMessage>().command) {
            case "Go":
                GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().ClientDefault.transform.Find("TradeButton").GetComponent<Button>().interactable = true;
                GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().ClientDefault.transform.Find("BuildButton").GetComponent<Button>().interactable = true;
                GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().ClientDefault.transform.Find("Next Player").GetComponent<Button>().interactable = true;
                GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().DiceRoll.SetActive(true);
                break;
            case "FirstRoundGo":
                GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().FirstRoundBuild.SetActive(true);
                break;
            case "SecondRoundGo":
                GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().SecondRoundBuild.SetActive(true);
                break;
            case "FirstGo":
                GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().ClientDefault.transform.Find("Next Player").GetComponent<Button>().interactable = true;
                break;
            case "Orange":
                GameObject.Find("GamePlay").GetComponent<GamePlayClient>().InitClient("Orange");
                break;
            case "White":
                GameObject.Find("GamePlay").GetComponent<GamePlayClient>().InitClient("White");
                break;
            case "Blue":
                GameObject.Find("GamePlay").GetComponent<GamePlayClient>().InitClient("Blue");
                break;
            case "Red":
                GameObject.Find("GamePlay").GetComponent<GamePlayClient>().InitClient("Red");
                break;
            case "Start":
                GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().ClientDefault.SetActive(true);
                GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().WaitScreen.SetActive(false);
                GameBoard.MainBoard.Init();
                break;
            case "Player declined Trading":
                GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().TradeDecline.SetActive(true);
                //GameObject.Find("TradeWasDeclined").SetActive(true);
                break;
            case "Player accepted Trading":
                GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().TradeAccept.SetActive(true);
                //GameObject.Find("TradeWasAccepted").SetActive(true);
                break;
            case "PlayerOrder":
                GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().OrderDiceRoll.SetActive(true);
                break;
            case "Roll Again":
                GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().NewOrderDiceRoll.SetActive(true);
                GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().WaitRollScreen.SetActive(false);
                break;
            case "Roll Waiting":
                GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().WaitRollScreen.SetActive(true);
                break;
            case "FinishOrderRoll":
                GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().WaitRollScreen.SetActive(false);
                break;
            case "ServerFull":
                SceneManager.LoadScene("main");
                break;
            default:
                Debug.LogError("Can not read message from Server!");
                break;
        }
    }
    //TRADE
    private void ReciveTradeMessage(NetworkMessage _message_) {
        Debug.Log("RECIVED A TRADEMESSAGE!");
        TradeMessage tradeMSG = new TradeMessage();
        _message_.reader.SeekZero();
        tradeMSG.trade = _message_.ReadMessage<TradeMessage>().trade;
        GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().Trade.GetComponent<CreateTrade>().ShowTrade(tradeMSG.trade);
        //GameObject.Find("Trade").GetComponent<CreateTrade>().ShowTrade(tradeMSG.trade);
    }
    //INVENTORY
    private void ReciveInventoryMessage(NetworkMessage _message_) {
        NetMessage netMSG = new NetMessage();
        _message_.reader.SeekZero();
        netMSG.command = _message_.ReadMessage<NetMessage>().command;
        string[] deltas = netMSG.command.Split('|');
        string name = deltas[0];
        GameObject.Find("Window").transform.Find("Hand").Find(name).Find("AmountBG").Find("Amount").GetComponent<Text>().text = deltas[1];
        GameObject.Find("GamePlay").GetComponent<GamePlayClient>().ownPlayer.inventory.inven[name] = Int32.Parse(deltas[1]);
    }
    //UPDATE FIELD
    string tempPawn;
    string tempColor;
    private void ReciveFieldUpdateMessage(NetworkMessage _message_) {
        FieldMessage fieldMSG = new FieldMessage();
        _message_.reader.SeekZero();
        fieldMSG.pawn = _message_.ReadMessage<FieldMessage>().pawn;
        string[] deltas = fieldMSG.pawn.Split('|');
        tempPawn = deltas[0];
        tempColor = deltas[1];
    }
    private void ReciveFieldUpdateMessage2(NetworkMessage _message_) {
        FieldMessage2 fieldMSG2 = new FieldMessage2();
        _message_.reader.SeekZero();
        fieldMSG2.place = _message_.ReadMessage<FieldMessage2>().place;
        GameObject.Find("GamePlay").GetComponent<GamePlayClient>().UpdateBoard(new Pawn(tempPawn, tempColor), fieldMSG2.place);
    }
}
