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

    public void OutputAdresses()
    {
        RectTransform parent = GameObject.Find("Window").transform.Find("Addresses").GetComponent<RectTransform>();

        int childs = parent.childCount - 1;

        for (int i = 0; i < childs; i++)
        {
            Destroy(parent.GetChild(childs - i).gameObject);
        }

        RectTransform newAddress = Instantiate(GameObject.Find("Window").transform.Find("Addresses").Find("BG").gameObject, parent).GetComponent<RectTransform>();

        int cachedPort = GetComponent<NetworkClientDiscovery>().port;
        string cachedIpAddress = GetComponent<NetworkClientDiscovery>().ipAddress;

        newAddress.transform.Find("Address").gameObject.GetComponent<Text>().text = cachedIpAddress;
        newAddress.transform.Find("Port").gameObject.GetComponent<Text>().text = cachedPort.ToString();
        newAddress.Translate(Vector2.down * 20);
        newAddress.gameObject.AddComponent<Button>().onClick.AddListener(delegate { ConnectToServer(cachedIpAddress, cachedPort); });


    }

    //Connect
    public void ConnectToServer(string _ipAddress_, int _port_) {
        client.Connect(_ipAddress_, _port_);
    }
}
