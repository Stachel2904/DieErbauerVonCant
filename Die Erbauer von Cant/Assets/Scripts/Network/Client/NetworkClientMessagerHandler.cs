using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class NetworkClientMessagerHandler : MonoBehaviour {

    private NetworkClient client;

    public void InitClientMessages(NetworkClient _client_) {
        client = _client_;
        client.RegisterHandler(888, ReciveMessageFromServer);
        client.RegisterHandler(889, ReciveTradeMessage);
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
    public void SendToServer(string _message_) {
        if (client.isConnected) {
            StringMessage msg = new StringMessage();
            msg.value = _message_;
            bool success = client.Send(888, msg);
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
    //Recive from Server
    private void ReciveMessageFromServer(NetworkMessage _message_) {
        Debug.Log("RECIVED A MESSAGE!");
        StringMessage msg = new StringMessage();
        msg.value = _message_.ReadMessage<StringMessage>().value;
    }
    private void ReciveTradeMessage(NetworkMessage _message_) {
        Debug.Log("RECIVED A TRADEMESSAGE!");
        TradeMessage tradeMSG = new TradeMessage();
        tradeMSG.trade = _message_.ReadMessage<TradeMessage>().trade;
    }
}
