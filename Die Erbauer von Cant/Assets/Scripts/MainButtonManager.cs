using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainButtonManager : MonoBehaviour {

    [SerializeField]
    Button client;
    [SerializeField]
    Button server;

	// Use this for initialization
	void Start () {
        Button cButton = client.GetComponent<Button>();
        cButton.onClick.AddListener(ClickOnClient);
        Button sButton = server.GetComponent<Button>();
        sButton.onClick.AddListener(ClickOnServer);
	}
    private void ClickOnClient() {
        SceneManager.LoadScene("ClientScene");
    }
    private void ClickOnServer() {
        SceneManager.LoadScene("ServerScene");
    }
}
