using UnityEngine;
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
        NetworkServer.RegisterHandler(890, ReciveAcceptMessage);
        NetworkServer.RegisterHandler(MsgType.Connect, ServerOnClientConnect);
        NetworkServer.RegisterHandler(MsgType.Disconnect, ServerOnClientDisconnect);
        //init = true;
    }
    //Connect from Client
    private void ServerOnClientConnect(NetworkMessage _message_) {

        Debug.Log("[Client ID: " + _message_.conn.connectionId + "] Client connected!");
        GetComponent<NetworkServerUI>().AddConnectedPlayer(_message_.conn.connectionId);
        GetComponent<NetworkServerGUI>().AddConnectedPlayerAvatar(_message_.conn.connectionId);
        //blockedSlots++;
        //Create Player here
    }
    private void ServerOnClientDisconnect(NetworkMessage _message_) {
        Debug.Log("[Client ID: " + _message_.conn.connectionId + "] Client disconnected!");
        GetComponent<NetworkServerGUI>().RemoveConnectedPlayerAvatar(_message_.conn.connectionId);
        GetComponent<NetworkServerUI>().RemoveConnectedPlayer(_message_.conn.connectionId);
        //blockedSlots--;
        //Destroying Player here
    }
    //Recive message from Client
    private void ServerReciveMessage(NetworkMessage _message_) {
        int clientID = _message_.conn.connectionId;
        Debug.Log("RECIVED A MESSAGE FROM CLIENT WITH THE ID: " + clientID);
        NetMessage netMSG = new NetMessage();
        switch (_message_.ReadMessage<NetMessage>().command){
            case "Roll Dice":
                DiceGenerator.Main.DiceRoll();
                break;
            case "Next Player":
                //NextPlayerStuffHere
                SendToClient(GamePlay.Main.GetCurrentPlayer().clientID, "Go");
                break;
            default:
                Debug.LogError("Can not read message from Client: " + _message_.conn.connectionId);
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
    //Recive AcceptMessage
    private void ReciveAcceptMessage(NetworkMessage _message_) {
        Debug.Log("RECIVED A ACCEPTMESSAGE!");
        AcceptMessage acceptMSG = new AcceptMessage();
        acceptMSG.isAccepted = _message_.ReadMessage<AcceptMessage>().isAccepted;
        switch (_message_.ReadMessage<AcceptMessage>().acceptType) {
            case "Trade":
                //Tradeaccept stuff
                break;
            case "Go":
                //NextPlayerStuff
                break;
            case "Ready":
                //Readyaccept stuff
            default:
                break;
        }
    }
    //Send to Client
    public void SendToClient(int _ClientID_, string _command_) {
        NetMessage netMSG = new NetMessage();
        netMSG.command = _command_;
        //netMSG.value = _value_;
        NetworkServer.SendToClient(_ClientID_, 888, netMSG);
    }
    //Send trade to Client
    public void SendTradeToClient(int _ClientID_, Trade _trade_) {
        TradeMessage tradeMSG = new TradeMessage();
        tradeMSG.trade = _trade_;
        NetworkServer.SendToClient(_ClientID_, 889, tradeMSG);
    }
    //Send Accept To Server
    public void SendAcceptToClient(int _ClientID_, string _AcceptType_, bool _isAccepted_) {
        AcceptMessage acceptMSG = new AcceptMessage();
        acceptMSG.acceptType = _AcceptType_;
        acceptMSG.isAccepted = _isAccepted_;
        NetworkServer.SendToClient(_ClientID_, 890, acceptMSG);
    }
    public void SendInventoryToClient(int _ClientID_, string _message_) {
        NetMessage netMSG = new NetMessage();
        netMSG.command = _message_;
        NetworkServer.SendToClient(_ClientID_, 891, netMSG);
    }
}
