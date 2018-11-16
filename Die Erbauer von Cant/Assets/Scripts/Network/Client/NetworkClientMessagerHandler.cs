using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkClientMessagerHandler : MonoBehaviour {

    private NetworkClient client;

    public void InitClientMessages(NetworkClient _client_) {
        client = _client_;
        client.RegisterHandler(888, ReciveMessageFromServer);
        client.RegisterHandler(889, ReciveTradeMessage);
        client.RegisterHandler(890, ReciveAcceptMessage);
        client.RegisterHandler(891, ReciveInventoryMessage);
        client.RegisterHandler(MsgType.Connect, OnConnect);
        client.RegisterHandler(MsgType.Disconnect, OnDisconnect);
    }
    private void OnConnect(NetworkMessage _message_) {
        Debug.Log("Client is connected: " + client.isConnected);
        Debug.Log("Server: " + client.serverIp);
        GameObject.Find("Window").transform.Find("Addresses").gameObject.SetActive(false);
        GameObject.Find("Window").transform.Find("DiceRoll").gameObject.SetActive(true);
    }
    private void OnDisconnect(NetworkMessage _message_) {
        Debug.Log("Client is connected: " + client.isConnected);
        Debug.Log("Client is disconnected!");
        GameObject.Find("Window").transform.Find("Addresses").gameObject.SetActive(true);
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
    }
    //Recive from Server
    private void ReciveMessageFromServer(NetworkMessage _message_) {
        Debug.Log("RECIVED A MESSAGE!");
        NetMessage netMSG = new NetMessage();
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
        tradeMSG.trade = _message_.ReadMessage<TradeMessage>().trade;
    }
    private void ReciveAcceptMessage(NetworkMessage _message_) {
        Debug.Log("RECIVED A ACCEPTMESSAGE!");
        AcceptMessage acceptMSG = new AcceptMessage();
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
        netMSG.command = _message_.ReadMessage<NetMessage>().command;
        string[] deltas = netMSG.command.Split('|');
        string name = deltas[0];
        GameObject.Find("Window").transform.Find("Hand").Find(name).Find("AmountBG").Find("Amount").GetComponent<Text>().text = deltas[1];
    }
}
