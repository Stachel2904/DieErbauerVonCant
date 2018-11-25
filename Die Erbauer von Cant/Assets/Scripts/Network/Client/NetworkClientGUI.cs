using UnityEngine;
using UnityEngine.UI;

public class NetworkClientGUI : MonoBehaviour {
    public void OutputAdresses() {
        RectTransform parent = GameObject.Find("Window").transform.Find("Addresses").GetComponent<RectTransform>();

        int childs = parent.childCount - 2;

        for (int i = 0; i < childs; i++) {
            Destroy(parent.GetChild(2 + i).gameObject);
        }

        RectTransform newAddress = Instantiate(GameObject.Find("Window").transform.Find("Addresses").Find("BG").gameObject, parent).GetComponent<RectTransform>();

        int cachedPort = this.gameObject.GetComponent<NetworkClientDiscovery>().port;
        string cachedIpAddress = this.gameObject.GetComponent<NetworkClientDiscovery>().ipAddress;

        newAddress.transform.Find("Name").gameObject.GetComponent<Text>().text = GetComponent<NetworkClientDiscovery>().serverName;
        newAddress.transform.Find("Port").gameObject.GetComponent<Text>().text = cachedPort.ToString();
        newAddress.Translate(Vector2.down * 100);
        newAddress.gameObject.AddComponent<Button>().onClick.AddListener(delegate { this.gameObject.GetComponent<NetworkClientUI>().ConnectToServer(cachedIpAddress, cachedPort); });
    }
    public void DeactivateSearchServerPanel() {
        GameObject.Find("Window").transform.Find("Addresses").gameObject.SetActive(false);
        GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().WaitScreen.SetActive(true);
    }
}
