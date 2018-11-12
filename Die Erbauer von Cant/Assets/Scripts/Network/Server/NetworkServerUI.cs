using UnityEngine;
using UnityEngine.Networking;

public class NetworkServerUI : MonoBehaviour {

    int minPort = 5555;
    int maxPort = 5565;
    int defaultPort = 5555;
    public int serverPort = -1;
	
    private void Start()
    {
        StartServer();
    }

	public void StartServer(){
        serverPort = InitServer();
        if (serverPort != -1) {
            Debug.Log("Server successfully created on port: " + serverPort);
            GetComponent<NetworkServerMessageHandler>().InitRecivingMessages();
            GetComponent<NetworkServerDiscovery>().StartServerDiscovery(serverPort);
        }
	}
    public void KillServer() {
        NetworkServer.Shutdown();
        Debug.Log("Server successfully killed... :(");
        GetComponent<NetworkServerDiscovery>().StopServerDiscovery();
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
            Debug.Log("Failed to create with default port");
            for(int tempPort = minPort; tempPort <= maxPort; tempPort++) {
                if(tempPort != defaultPort) {
                    if (NetworkServer.Listen(tempPort)) {
                        t_serverPort = tempPort;
                        break;
                    }
                    if(tempPort == maxPort) {
                        Debug.LogError("Failed to create Server, no port between 5555 and 5565 found!");
                    }
                }
            }
        }
        return t_serverPort;
    }
}
