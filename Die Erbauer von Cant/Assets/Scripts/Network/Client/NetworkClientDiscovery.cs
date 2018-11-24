using UnityEngine;
using UnityEngine.Networking;

public class NetworkClientDiscovery : NetworkDiscovery {

    public string ipAddress;
    public string serverName;
    public int port;

    public void StartClientDiscovery() {
        Initialize();
        StartAsClient();
    }
    public override void OnReceivedBroadcast(string _fromAddress_, string _data_){
        ipAddress = _fromAddress_.Substring(_fromAddress_.LastIndexOf(":") + 1, _fromAddress_.Length - (_fromAddress_.LastIndexOf(":") + 1));
        string serverMessage = _data_.Substring(_data_.LastIndexOf(":") + 1, _data_.Length - (_data_.LastIndexOf(":") + 1));
        Debug.Log(serverMessage);
        string[] deltas = serverMessage.Split('|');
        serverName = deltas[0];
        string s_port = deltas[1];
        if (!int.TryParse(s_port, out port)) {
            Debug.LogError("Can not parse port out of serverMessage!");
        }
        Debug.Log(ipAddress + " / " + port);
        this.gameObject.GetComponent<NetworkClientGUI>().OutputAdresses();
    }
}
