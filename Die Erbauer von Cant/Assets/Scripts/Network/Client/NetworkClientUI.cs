using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetworkClientUI : MonoBehaviour {

    static NetworkClient client;

    private void Start() {
        client = new NetworkClient();
        GetComponent<NetworkClientDiscovery>().StartClientDiscovery();
        GetComponent<NetworkClientMessagerHandler>().InitClientMessages(client);
    }
    //Connect
    public void ConnectToServer(string _ipAddress_, int _port_) {
        client.Connect(_ipAddress_, _port_);
    }
}
