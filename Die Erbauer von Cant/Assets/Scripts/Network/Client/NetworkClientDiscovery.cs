using UnityEngine;
using UnityEngine.Networking;

public class NetworkClientDiscovery : NetworkDiscovery {

    public string ipAddress;
    public string serverMessage;
    public int port;

    public void StartClientDiscovery() {
        Initialize();
        StartAsClient();
    }
    public override void OnReceivedBroadcast(string _fromAddress_, string _data_)
    {
        ipAddress = _fromAddress_.Substring(_fromAddress_.LastIndexOf(":") + 1, _fromAddress_.Length - (_fromAddress_.LastIndexOf(":") + 1));
        serverMessage = _data_.Substring(_data_.LastIndexOf(":") + 1, _data_.Length - (_data_.LastIndexOf(":") + 1));
        Debug.Log(serverMessage);
        string s_port = _data_.Substring(_data_.LastIndexOf("|") + 1, _data_.Length - (_data_.LastIndexOf("|") + 1));
        int.TryParse(s_port, out port);
        Debug.Log(ipAddress + " / " + port);
        this.gameObject.GetComponent<NetworkClientUI>().OutputAdresses();
    }
}
