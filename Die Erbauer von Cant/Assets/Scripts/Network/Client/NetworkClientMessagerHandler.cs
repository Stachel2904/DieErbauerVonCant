using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkClientMessagerHandler : MonoBehaviour {

    private NetworkClient client;

    public void InitClientMessages(NetworkClient _client_) {
        client = _client_;
        client.RegisterHandler(888, ReciveMessageFromServer);
        client.RegisterHandler(889, ReciveTradeMessage);
        client.RegisterHandler(890, ReciveAcceptMessage);
        client.RegisterHandler(891, ReciveInventoryMessage);
        client.RegisterHandler(892, ReciveFieldUpdateMessage);
        client.RegisterHandler(893, ReciveCreateTradeMessage);
        client.RegisterHandler(MsgType.Connect, OnConnect);
        client.RegisterHandler(MsgType.Disconnect, OnDisconnect);
    }
    private void OnConnect(NetworkMessage _message_) {
        Debug.Log("Client is connected: " + client.isConnected);
        Debug.Log("Server: " + client.serverIp);
        GetComponent<NetworkClientGUI>().DeactivateSearchServerPanel();
        GameObject.Find("Window").transform.Find("DiceRoll").gameObject.SetActive(true);
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
            tradeMSG.ressource1 = _resource1_;
            tradeMSG.ressource2 = _resource2_;
            bool success = client.Send(893, tradeMSG);
            if (!success) {
                Debug.LogError("Failed to send trademessage!");
            }
        }
        else {
            Debug.LogError("Client is not connected! Failed to send trademessage!");
        }
    }
    //ACCEPT
    public void SendAcceptToServer(string _AcceptType_, bool _isAccepted_) {
        if (client.isConnected) {
            AcceptMessage acceptMSG = new AcceptMessage();
            acceptMSG.acceptType = _AcceptType_;
            acceptMSG.isAccepted = _isAccepted_;
            bool success = client.Send(890, acceptMSG);
            if (!success) {
                Debug.LogError("Failed to send acceptmessage!");
            }
        }
        else {
            Debug.LogError("Client is not connected! Failed to send acceptmessage!");
        }
    }
    //FIELD
    public void SendFieldUpdateToServer(string _pawn_, Place _place_) {
        if (client.isConnected) {
            FieldMessage fieldMSG = new FieldMessage();
            fieldMSG.pawn = _pawn_;
            fieldMSG.place = _place_;
            bool success = client.Send(892, fieldMSG);
            if (!success) {
                Debug.LogError("Failed to send fieldupdatemessage!");
            }
        }
        else {
            Debug.LogError("Client is not connected! Failed to send fieldupdatemessage!");
        }
    }
    //Recive from Server
    private void ReciveMessageFromServer(NetworkMessage _message_) {
        Debug.Log("RECIVED A MESSAGE!");
        _message_.reader.SeekZero();
        switch (_message_.ReadMessage<NetMessage>().command) {
            case "Go":
            //AskForReadyHere (Send AcceptMessage)
            case "Orange":
                break;
            case "White":
                break;
            case "Blue":
                break;
            case "Red":
                GameObject.Find("ButtonManager").GetComponent<ClientButtonManager>().DiceRoll.SetActive(true);
                break;
            case "Start":
                GameObject.Find("Window").transform.Find("ClientDefault").gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
    //TRADE
    private void ReciveTradeMessage(NetworkMessage _message_) {
        Debug.Log("RECIVED A TRADEMESSAGE!");
        TradeMessage tradeMSG = new TradeMessage();
        _message_.reader.SeekZero();
        tradeMSG.trade = _message_.ReadMessage<TradeMessage>().trade;
    }
    private void ReciveCreateTradeMessage(NetworkMessage _message_) {
        Debug.Log("RECIVED A CREATETRADEMESSAGE!");
        CreateTradeMessage tradeMSG = new CreateTradeMessage();
        _message_.reader.SeekZero();
        tradeMSG.ressource1 = _message_.ReadMessage<CreateTradeMessage>().ressource1;
        tradeMSG.ressource2 = _message_.ReadMessage<CreateTradeMessage>().ressource2;
    }
    //ACCEPT
    private void ReciveAcceptMessage(NetworkMessage _message_) {
        Debug.Log("RECIVED AN ACCEPTMESSAGE!");
        AcceptMessage acceptMSG = new AcceptMessage();
        _message_.reader.SeekZero();
        acceptMSG.isAccepted = _message_.ReadMessage<AcceptMessage>().isAccepted;
        switch (_message_.ReadMessage<AcceptMessage>().acceptType) {
            case "Trade":
                //Tradeaccept stuff
                break;
            case "Ready":
                //Readyaccept stuff
                break;
            default:
                break;
        }
    }
    //INVENTORY
    private void ReciveInventoryMessage(NetworkMessage _message_) {
        NetMessage netMSG = new NetMessage();
        _message_.reader.SeekZero();
        netMSG.command = _message_.ReadMessage<NetMessage>().command;
        string[] deltas = netMSG.command.Split('|');
        string name = deltas[0];
        GameObject.Find("Window").transform.Find("Hand").Find(name).Find("AmountBG").Find("Amount").GetComponent<Text>().text = deltas[1];
    }
    //UPDATE FIELD
    private void ReciveFieldUpdateMessage(NetworkMessage _message_) {
        FieldMessage fieldMSG = new FieldMessage();
        _message_.reader.SeekZero();
        fieldMSG.pawn = _message_.ReadMessage<FieldMessage>().pawn;
        fieldMSG.place = _message_.ReadMessage<FieldMessage>().place;
        GameObject.Find("GamePlay").GetComponent<GamePlayClient>().UpdateBoard(new Pawn(fieldMSG.pawn, GamePlay.Main.GetCurrentPlayer().color), fieldMSG.place);
    }
}
