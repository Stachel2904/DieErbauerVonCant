using UnityEngine.Networking;

public class NetworkServerDiscovery : NetworkDiscovery {

    private string serverName = "DEvC-Server";

    public void StartServerDiscovery(int _port_) {
        StopBroadcast();
        broadcastData = serverName + "|" + _port_.ToString(); //ToDo. Selbstupdatender Braodcaster + Belegte Slots
        Initialize();
        StartAsServer();
    }
    public void StopServerDiscovery() {
        StopBroadcast();
    }
    public void SetServerName(string _serverName_) {
        serverName = _serverName_;
    }
}
