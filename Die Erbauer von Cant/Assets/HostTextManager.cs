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

    Text tPlayerNumber;

    Text player1DiceText;
    Text player2DiceText;
    Text player3DiceText;
    Text player4DiceText;

    // Use this for initialization
    void Start () {
        tPlayerNumber = PlayerNumberText.GetComponent<Text>();

        player1DiceText = Player1DiceText.GetComponent<Text>();
        player2DiceText = Player2DiceText.GetComponent<Text>();
        player3DiceText = Player3DiceText.GetComponent<Text>();
        player4DiceText = Player4DiceText.GetComponent<Text>();

        //player1DiceText.text = "0";
        //player2DiceText.text = "0";
        //player3DiceText.text = "0";
        //player4DiceText.text = "0";
    }
	
	// Update is called once per frame
	void Update () {
        tPlayerNumber.text = (GamePlay.Main.maxPlayer +1).ToString();
    }
}
