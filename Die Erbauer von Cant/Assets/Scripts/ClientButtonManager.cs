using UnityEngine;
using UnityEngine.UI;

public class ClientButtonManager : MonoBehaviour {

    [SerializeField]
    Button rollDice;
    [SerializeField]
    Button nextPlayer;
    
    public GameObject BuildAcceptionInterface;

    // Use this for initialization
    void Start () {
        Button rDice = rollDice.GetComponent<Button>();
        rDice.onClick.AddListener(RollTheDice);
        Button nPlayer = nextPlayer.GetComponent<Button>();
        nPlayer.onClick.AddListener(GoNextPlayer);
	}
    private void RollTheDice() {
        GameObject.Find("ClientManager").GetComponent<NetworkClientMessagerHandler>().SendToServer("Roll Dice");
    }
    private void GoNextPlayer() {
        GameObject.Find("ClientManager").GetComponent<NetworkClientMessagerHandler>().SendToServer("Next Player");
    }
}
