using UnityEngine;
using UnityEngine.UI;

public class ClientButtonManager : MonoBehaviour {

    [SerializeField]
    Button rollDice;
    [SerializeField]
    Button nextPlayer;

    #region cachedInterfacers
    public GameObject ClientDefault;
    public GameObject DiceRoll;
    public GameObject Hand;
    public GameObject BuildSelection;
    public GameObject BuildAcception;
    public GameObject Trade;
    public GameObject TradeAcception;
    public GameObject TradeSystem;
    public GameObject ChooseTradePlayer;
    public GameObject SystemTrade1;
    public GameObject SystemTrade2;
    public GameObject TradeAccept;
    public GameObject TradeDecline;
    public GameObject Addresses;
    public GameObject WaitScreen;
    #endregion

    // Use this for initialization
    void Start () {
        Button rDice = rollDice.GetComponent<Button>();
        rDice.onClick.AddListener(RollTheDice);
        Button nPlayer = nextPlayer.GetComponent<Button>();
	}
    private void RollTheDice() {
        GameObject.Find("ClientManager").GetComponent<NetworkClientMessagerHandler>().SendToServer("Roll Dice");
        DiceRoll.SetActive(false);
        ClientDefault.transform.Find("TradeButton").GetComponent<Button>().interactable = true;
        ClientDefault.transform.Find("BuildButton").GetComponent<Button>().interactable = true;
        ClientDefault.transform.Find("Next Player").GetComponent<Button>().interactable = true;
    }
}
