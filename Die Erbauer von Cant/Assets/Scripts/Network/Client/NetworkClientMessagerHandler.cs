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
    public void SendFieldUpdateToServer(Pawn _pawn_, Place _place_) {
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
                break;
            default:
                break;
        }
    }
    private void ReciveTradeMessage(NetworkMessage _message_) {
        Debug.Log("RECIVED A TRADEMESSAGE!");
        TradeMessage tradeMSG = new TradeMessage();
        _message_.reader.SeekZero();
        tradeMSG.trade = _message_.ReadMessage<TradeMessage>().trade;
    }
    private void ReciveAcceptMessage(NetworkMessage _message_) {
        Debug.Log("RECIVED A ACCEPTMESSAGE!");
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
    private void ReciveInventoryMessage(NetworkMessage _message_) {
        NetMessage netMSG = new NetMessage();
        _message_.reader.SeekZero();
        netMSG.command = _message_.ReadMessage<NetMessage>().command;
        string[] deltas = netMSG.command.Split('|');
        string name = deltas[0];
        GameObject.Find("Window").transform.Find("Hand").Find(name).Find("AmountBG").Find("Amount").GetComponent<Text>().text = deltas[1];
    }
    private void ReciveFieldUpdateMessage(NetworkMessage _message_) {
        FieldMessage fieldMSG = new FieldMessage();
        _message_.reader.SeekZero();
        fieldMSG.pawn = _message_.ReadMessage<FieldMessage>().pawn;
        fieldMSG.place = _message_.ReadMessage<FieldMessage>().place;
    }
}
