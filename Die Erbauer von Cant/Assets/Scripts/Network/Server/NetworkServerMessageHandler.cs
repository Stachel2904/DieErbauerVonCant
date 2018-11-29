using UnityEngine;
using UnityEngine.Networking;

public class NetworkServerMessageHandler : MonoBehaviour {

    public int slots = 4;
    public int blockedSlots = 0;

    public void InitRecivingMessages() {
        NetworkServer.RegisterHandler(888, ServerReciveMessage);
        NetworkServer.RegisterHandler(889, ReciveTradeMessage);
        NetworkServer.RegisterHandler(892, ReciveFieldUpdateMessage);
        NetworkServer.RegisterHandler(894, ReciveFieldUpdateMessage2);
        NetworkServer.RegisterHandler(893, ReciveCreateTradeMessage);
        NetworkServer.RegisterHandler(895, ReciveNameMessage);
        NetworkServer.RegisterHandler(MsgType.Connect, ServerOnClientConnect);
        NetworkServer.RegisterHandler(MsgType.Disconnect, ServerOnClientDisconnect);
    }
    //Connect from Client
    private void ServerOnClientConnect(NetworkMessage _message_) {
        Debug.Log("[Client ID: " + _message_.conn.connectionId + "] Client connected!");
        if (blockedSlots == slots) {
            SendToClient(_message_.conn.connectionId, "ServerFull");
        }
        else {
            GetComponent<NetworkServerUI>().AddConnectedPlayer(_message_.conn.connectionId);
            GetComponent<NetworkServerGUI>().AddConnectedPlayerAvatar(_message_.conn.connectionId);
            blockedSlots++;
            if (GamePlay.Main.running == true) {
                SendToClient(_message_.conn.connectionId, "Start");
                for (int i = 0; i < GameBoard.MainBoard.pawns.Length; i++) {
                    for (int j = 0; j < GameBoard.MainBoard.pawns[i].Count; j++) {
                        Pawn tempPawn = GameBoard.MainBoard.pawns[i][j];
                        int[] place = new int[tempPawn.GetFields().Length * 3];
                        for (int y = 0; y < place.Length; y += 3) {
                            place[y] = tempPawn.GetFields()[y / 3].row;
                            place[y + 1] = tempPawn.GetFields()[y / 3].column;
                            place[y + 2] = tempPawn.GetPosAtField()[y / 3];
                        }
                        SendFieldUpdateToClient(tempPawn.type, tempPawn.color, place);
                    }
                }
                GamePlay.Main.UpdateInventory(_message_.conn.connectionId);
                if (_message_.conn.connectionId == GamePlay.Main.GetCurrentPlayer().clientID) {
                    SendToClient(_message_.conn.connectionId, "Go");
                }

            }
        }
    }
    private void ServerOnClientDisconnect(NetworkMessage _message_) {
        Debug.Log("[Client ID: " + _message_.conn.connectionId + "] Client disconnected!");
        if (_message_.conn.connectionId == GamePlay.Main.GetCurrentPlayer().clientID) {
            GamePlay.Main.NextPlayer();
            SendToClient(GamePlay.Main.GetCurrentPlayer().clientID, "Go");
        }
        GetComponent<NetworkServerGUI>().RemoveConnectedPlayerAvatar(_message_.conn.connectionId);
        GetComponent<NetworkServerUI>().RemoveConnectedPlayer(_message_.conn.connectionId);
        blockedSlots--;
    }
    //Recive message from Client
    private void ServerReciveMessage(NetworkMessage _message_) {
        _message_.reader.SeekZero();
        int clientID = _message_.conn.connectionId;
        Debug.Log("RECIVED A MESSAGE FROM CLIENT WITH THE ID: " + clientID);
        switch (_message_.ReadMessage<NetMessage>().command){
            case "Roll Dice":
                DiceGenerator.Main.DiceRoll();
                break;
            case "Next Player":
                GamePlay.Main.NextPlayer();
                if (GamePlay.Main.gameStarted)
                {
                    SendToClient(GamePlay.Main.GetCurrentPlayer().clientID, "Go");
                }
                else if (GamePlay.Main.firstRoundFinished == false)
                {
                    SendToClient(GamePlay.Main.GetCurrentPlayer().clientID, "FirstRoundGo");
                }
                else
                {
                    SendToClient(GamePlay.Main.GetCurrentPlayer().clientID, "SecondRoundGo");
                }
                break;
            case "Player started Trading":
                GameObject.Find("Window").transform.Find("TradingMessage").gameObject.SetActive(true);
                break;
            case "Player stopped Trading":
                GameObject.Find("Window").transform.Find("TradingMessage").gameObject.SetActive(false);
                break;
            case "Player declined Trading":
                SendToClient(GamePlay.Main.GetCurrentPlayer().clientID, "Player declined Trading");
                break;
            case "Player accepted Trading":
                SendToClient(GamePlay.Main.GetCurrentPlayer().clientID, "Player accepted Trading");
                break;
            case "GetOrderRoll":
                DiceGenerator.Main.GetOrderRoll(_message_.conn.connectionId);
                break;
            case "Get Second Village Ressources":
                GamePlay.Main.DistributeSecondVillageRessources();
                break;
            default:
                Debug.LogError("Can not read message from Client: " + _message_.conn.connectionId);
                break;
        }
    }
    private void ReciveNameMessage(NetworkMessage _message_) {
        _message_.reader.SeekZero();
        for (int i = 0; i < 4; i++) {
            if (_message_.conn.connectionId == GamePlay.Main.players[i].clientID) {
                GamePlay.Main.players[i].name = _message_.ReadMessage<NetMessage>().command;
                GetComponent<NetworkServerGUI>().AddConnectedPlayerAvatar(_message_.conn.connectionId);
            }
        }
    }
    //Recieve trade message from Client
    private void ReciveTradeMessage(NetworkMessage _message_) {
        Debug.Log("RECIVED A TRADEMESSAGE!");
        TradeMessage tradeMSG = new TradeMessage();
        _message_.reader.SeekZero();
        tradeMSG.trade = _message_.ReadMessage<TradeMessage>().trade;
        switch (tradeMSG.trade.timesSend)
        {
            case 0:
                for (int i = 0; i < 4; i++)
                {

                    if (GamePlay.Main.players[i].color == tradeMSG.trade.taker)
                    {
                        Debug.Log("color ist " + tradeMSG.trade.taker);
                        SendTradeToClient(GamePlay.Main.players[i].clientID, tradeMSG.trade);
                    }
                }
                break;
            case 1:
                GamePlay.Main.Trading(tradeMSG.trade);
                break;
            default:
                Debug.LogError("Reciving trade not possible! No avaible case between 0 and 1!");
                break;
        }
       
    }
    private void ReciveCreateTradeMessage(NetworkMessage _message_) {
        Debug.Log("RECIVED A CREATETRADEMESSAGE!");
        CreateTradeMessage tradeMSG = new CreateTradeMessage();
        _message_.reader.SeekZero();
        tradeMSG.ressource = _message_.ReadMessage<CreateTradeMessage>().ressource;
        string[] deltas = tradeMSG.ressource.Split('|');
        GamePlay.Main.tradeSystem4to1(deltas[0], deltas[1]);
    }
    //FIELD UPDATE
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
        GamePlay.Main.UpdateBoard(new Pawn(tempPawn, tempColor), fieldMSG2.place);
        SendFieldUpdateToClient(tempPawn, tempColor, fieldMSG2.place);
        GameObject.Find("GamePlay").GetComponent<GamePlay>().UpdateInventory(_message_.conn.connectionId);
    }
    //Send to Client
    public void SendToClient(int _ClientID_, string _command_) {
        NetMessage netMSG = new NetMessage();
        netMSG.command = _command_;
        NetworkServer.SendToClient(_ClientID_, 888, netMSG);
    }
    //Send trade to Client
    public void SendTradeToClient(int _ClientID_, Trade _trade_) {
        TradeMessage tradeMSG = new TradeMessage();
        tradeMSG.trade = _trade_;
        NetworkServer.SendToClient(_ClientID_, 889, tradeMSG);
    }
    //Send Inventoryinformation to Client
    /// <param name="_ClientID_">ID</param>
    /// <param name="_message_">Brick|8 Der Wert muss aus dem Inventar ausgelesen werden, dies ist die Zahl, die angezeigt wird</param> Der Wert muss aus dem Inventar ausgelesen werden, dies ist die Zahl, die angezeigt wird
    public void SendInventoryToClient(int _ClientID_, string _message_) {
        NetMessage netMSG = new NetMessage();
        netMSG.command = _message_;
        NetworkServer.SendToClient(_ClientID_, 891, netMSG);
    }
    //UPDATE FIELD
    public void SendFieldUpdateToClient(string _pawn_, string _color_, int[] _place_) {
        FieldMessage fieldMSG = new FieldMessage();
        FieldMessage2 fieldMSG2 = new FieldMessage2();
        fieldMSG.pawn = _pawn_ + "|" + _color_;
        fieldMSG2.place = _place_;
        Debug.Log("[SEND] PAWN: " + fieldMSG.pawn);
        Debug.Log("[SEND] PLACE: " + fieldMSG2.place);
        bool succsess = NetworkServer.SendToAll(892, fieldMSG);
        if (!succsess) {
            Debug.LogError("Failed to send fieldupdateinformation [PAWN]!");
        }
        succsess = NetworkServer.SendToAll(894, fieldMSG2);
        if (!succsess) {
            Debug.LogError("Failed to send fieldupdateinformation [PLACE]!");
        }
    }
    public void SendToAllClients(string _command_) {
        NetMessage netMSG = new NetMessage();
        netMSG.command = _command_;
        bool succsess = NetworkServer.SendToAll(888, netMSG);
        if (!succsess) {
            Debug.LogError("Failed to send command: " + _command_ + "to all clients!");
        }
    }
    
}
