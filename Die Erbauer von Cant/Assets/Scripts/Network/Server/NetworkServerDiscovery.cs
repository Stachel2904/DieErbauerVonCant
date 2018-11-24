using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public class NetworkServerDiscovery : NetworkDiscovery {

    public string serverName = "DEvC-Server";

    public InputField newServerNamer;

    public void StartServerDiscovery(int _port_) {
        //StopBroadcast();
        broadcastData = serverName + "|" + _port_.ToString(); //ToDo. Selbstupdatender Braodcaster + Belegte Slots
        Initialize();
        StartAsServer();
    }
    public void StopServerDiscovery() {
        StopBroadcast();
    }
    public void SetServerName() {
        serverName = newServerNamer.text;
    }
}
