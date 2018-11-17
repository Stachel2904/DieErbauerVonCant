using UnityEngine.Networking;
/// <summary>
/// For Client and Server
/// Example: 
/// On Client:
/// public void SendToServer(string _command_, int _value_){
///     if (client.isConnected) {
///      NetMessage netMSG = new NetMessage();
///      netMSG.command = _command_;
///      netMSG.value = _value_;
///      client.Send(888,netMSG);
///     }
/// }
/// On Server:
/// private void ServerReciveMessage(NetworkMessage _message_) {
///      NetMessage netMSG = new NetMessage();
///      netMSG.command = _message_.ReadMessage<NetMessage>().command;
///      netMSG.value = _message_.ReadMessage<NetMessage>().value;
///        switch (NetMSG.command) {
///            case "Roll Dice":
///                DiceGenerator.Main.DiceRoll();
///                break;
///            case "Trade":
///                AskForTrade(_message_.conn.connectionId, netMSG.value);
///                break;
///        }
///    }
/// </summary>
public class NetMessage : MessageBase { //ToDo: Anpassen, fehlende Daten ergänzen
    public string command;
}
public class TradeMessage : MessageBase {
    public Trade trade;
}
public class AcceptMessage : MessageBase {
    public string acceptType;
    public bool isAccepted;
}
