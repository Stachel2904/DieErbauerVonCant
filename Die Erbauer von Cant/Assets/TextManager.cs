using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

    
    public Text text11;
    public Text text12;
    public Text text13;
    public Text text14;
    public Text text15;
    public Text text16;
    public Text text17;
    public Text text18;
    public Text text19;
    public Text text20;
    

    Text showngivenBrickText;
    Text showngivenWheatText;
    Text showngivenOreText;
    Text showngivenWoodText;
    Text showngivenWoolText;

    Text shownaskedBrickText;
    Text shownaskedWheatText;
    Text shownaskedOreText;
    Text shownaskedWoodText;
    Text shownaskedWoolText;

    

    // Use this for initialization
    void Start () {
        showngivenBrickText = text11.GetComponent<Text>();
        showngivenWheatText = text12.GetComponent<Text>();
        showngivenOreText = text13.GetComponent<Text>();
        showngivenWoodText = text14.GetComponent<Text>();
        showngivenWoolText = text15.GetComponent<Text>();

        shownaskedBrickText = text16.GetComponent<Text>();
        shownaskedWheatText = text17.GetComponent<Text>();
        shownaskedOreText = text18.GetComponent<Text>();
        shownaskedWoodText = text19.GetComponent<Text>();
        shownaskedWoolText = text20.GetComponent<Text>();

        showngivenBrickText.text = "0";
        showngivenWheatText.text = "0";
        showngivenOreText.text = "0";
        showngivenWoodText.text = "0";
        showngivenWoolText.text = "0";

        shownaskedBrickText.text = "0";
        shownaskedWheatText.text = "0";
        shownaskedOreText.text = "0";
        shownaskedWoodText.text = "0";
        shownaskedWoolText.text = "0";

        
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
