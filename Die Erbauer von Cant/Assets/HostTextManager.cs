using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HostTextManager : MonoBehaviour {

    [SerializeField]
    GameObject PlayerNumberText;
    public Text Player1DiceText;
    public Text Player2DiceText;
    public Text Player3DiceText;
    public Text Player4DiceText;
    public Text WinText;
    public Text SoundText;
    public Text VictoryPointText;

    Text tPlayerNumber;

    Text player1DiceText;
    Text player2DiceText;
    Text player3DiceText;
    Text player4DiceText;

    Text winText;
    Text soundText;
    Text vpText;

    // Use this for initialization
    void Start ()
    {
        tPlayerNumber = PlayerNumberText.GetComponent<Text>();

        player1DiceText = Player1DiceText.GetComponent<Text>();
        player2DiceText = Player2DiceText.GetComponent<Text>();
        player3DiceText = Player3DiceText.GetComponent<Text>();
        player4DiceText = Player4DiceText.GetComponent<Text>();

        winText = WinText.GetComponent<Text>();
        soundText = SoundText.GetComponent<Text>();
        vpText = VictoryPointText.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        tPlayerNumber.text = (GamePlay.Main.maxPlayer +1).ToString();
    }
}
