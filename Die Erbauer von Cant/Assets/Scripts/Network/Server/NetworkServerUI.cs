using UnityEngine;
using UnityEngine.Networking;

public class NetworkServerUI : MonoBehaviour {

    int minPort = 5555;
    int maxPort = 5565;
    int defaultPort = 5555;
    public int serverPort = -1;
    int maxPlayer = 4;
    public bool serverOn = false;

    //private void Start(){
    //    StartServer();
    //}
	public void StartServer(){ 
        serverPort = InitServer();
        if (serverPort != -1) {
            Debug.Log("Server successfully created on port: " + serverPort);
            GetComponent<NetworkServerMessageHandler>().InitRecivingMessages();
            GetComponent<NetworkServerDiscovery>().StartServerDiscovery(serverPort);
            Debug.Log("Server initialisation complete!");
            serverOn = true;
        }
	}
    public void KillServer() {
        NetworkServer.Shutdown();
        Debug.Log("Server successfully killed... :(");
        GetComponent<NetworkServerDiscovery>().StopServerDiscovery();
        serverOn = false;
    }
    //Init
    private int InitServer() {
        int t_serverPort = -1;
        NetworkServer.Reset();
        bool serverCreated = NetworkServer.Listen(defaultPort);
        if (serverCreated) {
            t_serverPort = defaultPort;
            Debug.Log("Server created with default port: " + defaultPort);
        }
        else {
            Debug.Log("Failed to create Server with default port");
            for(int tempPort = minPort; tempPort <= maxPort; tempPort++) {
                if(tempPort != defaultPort) {
                    if (NetworkServer.Listen(tempPort)) {
                        t_serverPort = tempPort;
                        break;
                    }
                    if(tempPort == maxPort) {
                        Debug.LogError("Failed to create Server, no free port between 5555 and 5565 found!");
                    }
                }
            }
        }
        return t_serverPort;
    }
    //Handle
    public void AddConnectedPlayer(int _clientID_)
    {
        for (int i = 0; i < maxPlayer; i++)
        {
            if (GamePlay.Main.players[i].clientID == -1)
            {
                GamePlay.Main.players[i].clientID = _clientID_;
                GamePlay.Main.players[i].name = _clientID_.ToString(); //ToDo: Später Namen einfügen
                GetComponent<NetworkServerMessageHandler>().SendToClient(_clientID_, GamePlay.Main.players[i].color);
                break;
            }
        }
    }
    public void RemoveConnectedPlayer(int _clientID_)
    {
        for (int i = 0; i < maxPlayer; i++)
        {
            if (GamePlay.Main.players[i].clientID == _clientID_)
            {
                GamePlay.Main.players[i].clientID = -1;
                break;
            }
        }
    }
    public void UpdateClientInventoryGUI(int _ClientID_, string _Type_, int _Amount_)
    { //Nach Add !!!!! im Inventar für Type = Name des Rohstoffs und für Amount = Anzahl das Inventar für diesen Typ (Rohstoff) auslesen. Dies ist keine Addfunktion. 
        string temp = _Type_ + "|" + _Amount_.ToString();
        GetComponent<NetworkServerMessageHandler>().SendInventoryToClient(_ClientID_, temp);
    }
}
