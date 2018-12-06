using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClientButtonManager : MonoBehaviour {

    [SerializeField]
    Button rollDice;
    [SerializeField]
    Button backMenu;

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
    public GameObject OrderDiceRoll;
    public GameObject NewOrderDiceRoll;
    public GameObject WaitRollScreen;
    public GameObject FirstRoundBuild;
    public GameObject SecondRoundBuild;
    public Image[] PawnSprites;
    public Button FirstRoundVillage;
    public Button SecondRoundVillage;
    public Button FirstRoundStreet;
    public Button SecondRoundStreet;
    #endregion

    // Use this for initialization
    void Start ()
    {
        Button rDice = rollDice.GetComponent<Button>();
        rDice.onClick.AddListener(RollTheDice);
        Button bMenu = backMenu.GetComponent<Button>();
        bMenu.onClick.AddListener(BackToMenu);

        FirstRoundVillage.onClick.AddListener(delegate { GamePlayClient.Main.TryBuild("Village", true); });
        SecondRoundVillage.onClick.AddListener(delegate { GamePlayClient.Main.TryBuild("Village", true); });
        FirstRoundStreet.onClick.AddListener(delegate { GamePlayClient.Main.TryBuild("Street", true); });
        SecondRoundStreet.onClick.AddListener(delegate { GamePlayClient.Main.TryBuild("Street", true); });
    }
    private void RollTheDice()
    {
        GameObject.Find("ClientManager").GetComponent<NetworkClientMessagerHandler>().SendToServer("Roll Dice");
        DiceRoll.SetActive(false);
        ClientDefault.transform.Find("TradeButton").GetComponent<Button>().interactable = true;
        ClientDefault.transform.Find("BuildButton").GetComponent<Button>().interactable = true;
        ClientDefault.transform.Find("Next Player").GetComponent<Button>().interactable = true;
    }

    public void RollOrderDice()
    {
        GameObject.Find("ClientManager").GetComponent<NetworkClientMessagerHandler>().SendToServer("GetOrderRoll");
    }
    public void BackToMenu() {
        SceneManager.LoadScene("main");
    }
}
