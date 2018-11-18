using UnityEngine;
using UnityEngine.UI;

public class NetworkClientGUI : MonoBehaviour {
    public void OutputAdresses() {
        RectTransform parent = GameObject.Find("Window").transform.Find("Addresses").GetComponent<RectTransform>();

        int childs = parent.childCount - 1;

        for (int i = 0; i < childs; i++) {
            Destroy(parent.GetChild(childs - i).gameObject);
        }

        RectTransform newAddress = Instantiate(GameObject.Find("Window").transform.Find("Addresses").Find("BG").gameObject, parent).GetComponent<RectTransform>();

        int cachedPort = this.gameObject.GetComponent<NetworkClientDiscovery>().port;
        string cachedIpAddress = this.gameObject.GetComponent<NetworkClientDiscovery>().ipAddress;

        Debug.Log(newAddress);
        newAddress.transform.Find("Address").gameObject.GetComponent<Text>().text = cachedIpAddress;
        Debug.Log(newAddress);
        newAddress.transform.Find("Port").gameObject.GetComponent<Text>().text = cachedPort.ToString();
        newAddress.Translate(Vector2.down * 20);
        newAddress.gameObject.AddComponent<Button>().onClick.AddListener(delegate { this.gameObject.GetComponent<NetworkClientUI>().ConnectToServer(cachedIpAddress, cachedPort); });
    }
    public void DeactivateSearchServerPanel() {
        Debug.Log("Kartoffel");
        GameObject.Find("Window").transform.Find("Addresses").gameObject.SetActive(false);
        GameObject.Find("Window").transform.Find("ClientDefault").gameObject.SetActive(true);
        GameObject.Find("Window").transform.Find("Hand").gameObject.SetActive(true);
    }
}
