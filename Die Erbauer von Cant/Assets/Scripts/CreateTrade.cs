using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateTrade : MonoBehaviour {

    public Trade createdTrade = new Trade();

    [SerializeField]
    GameObject text1;

    [SerializeField]
    GameObject text2;

    [SerializeField]
    GameObject text3;

    [SerializeField]
    GameObject text4;

    [SerializeField]
    GameObject text5;

    [SerializeField]
    GameObject text6;

    [SerializeField]
    GameObject text7;

    [SerializeField]
    GameObject text8;

    [SerializeField]
    GameObject text9;

    [SerializeField]
    GameObject text10;

    Text givenBrickText;
    Text givenWheatText;
    Text givenOreText;
    Text givenWoodText;
    Text givenWoolText;

    Text askedBrickText;
    Text askedWheatText;
    Text askedOreText;
    Text askedWoodText;
    Text askedWoolText;

    int givenBrick = 0;
    int givenWheat = 0;
    int givenOre = 0;
    int givenWood = 0;
    int givenWool = 0;

    int askedBrick = 0;
    int askedWheat = 0;
    int askedOre = 0;
    int askedWood = 0;
    int askedWool = 0;

    public void Start()
    {
        givenBrickText = text1.GetComponent<Text>();
        //givenWheatText = text2.GetComponent<Text>();
        //givenOreText = text3.GetComponent<Text>();
        //givenWoodText = text4.GetComponent<Text>();
        //givenWoolText = text5.GetComponent<Text>();

        //askedBrickText = text6.GetComponent<Text>();
        //askedWheatText = text7.GetComponent<Text>();
        //askedOreText = text8.GetComponent<Text>();
        //askedWoodText = text9.GetComponent<Text>();
        //askedWoolText = text10.GetComponent<Text>();

    }
    private void Update()
    {
        givenBrickText.text = givenBrick.ToString();
        //givenWheatText.text = givenWheat.ToString();
        //givenOreText.text = givenOre.ToString();
        //givenWoodText.text = givenWood.ToString();
        //givenWoolText.text = givenWool.ToString();

        //askedBrickText.text = askedBrick.ToString();
        //askedWheatText.text = askedWheat.ToString();
        //askedOreText.text = askedOre.ToString();
        //askedWoodText.text = askedWood.ToString();
        //askedWoolText.text = askedWool.ToString();
    }

    public void createAskedPlayer(string name)
    {
        createdTrade.taker = name;
    }

    public void inkrementGivenRessource(string name)
    {
        Debug.Log(name);
        switch (name)
        {
            case "Brick":
                Debug.Log(name);
                createdTrade.givenRessources[givenBrick] = name;
                givenBrick++;
                break;
            case "Wheat":
                {
                    createdTrade.givenRessources[givenWheat] = name;
                    givenWheat++;
                }
                break;
            case "Ore":
                {
                    createdTrade.givenRessources[givenOre] = name;
                    givenOre++;
                }
                break;
            case "Wood":
                {
                    createdTrade.givenRessources[givenWood] = name;
                    givenWood++;
                }
                break;
            case "Wool":
                {
                    createdTrade.givenRessources[givenWool] = name;
                    givenWool++;
                }
                break;
            default:
                break;
        }

        
    }

    public void dekrementGivenRessource(string name)
    {
        switch (name)
        {
            case "Brick":
                {
                    createdTrade.givenRessources[givenBrick] = null;
                    givenBrick--;
                }
                break;
            case "Wheat":
                {
                    createdTrade.givenRessources[givenWheat] = null;
                    givenWheat--;
                }
                break;
            case "Ore":
                {
                    createdTrade.givenRessources[givenOre] = null;
                    givenOre--;
                }
                break;
            case "Wood":
                {
                    createdTrade.givenRessources[givenWood] = null;
                    givenWood--;
                }
                break;
            case "Wool":
                {
                    createdTrade.givenRessources[givenWool] = null;
                    givenWool--;
                }
                break;
            default:
                break;
        }
    }



    public void inkrementAskedRessource(string name)
    {
        switch (name)
        {
            case "Brick":
                {
                    createdTrade.givenRessources[askedBrick] = name;
                    askedBrick++;
                }
                break;
            case "Wheat":
                {
                    createdTrade.givenRessources[askedWheat] = name;
                    askedWheat++;
                }
                break;
            case "Ore":
                {
                    createdTrade.givenRessources[askedOre] = name;
                    askedOre++;
                }
                break;
            case "Wood":
                {
                    createdTrade.givenRessources[askedWood] = name;
                    askedWood++;
                }
                break;
            case "Wool":
                {
                    createdTrade.givenRessources[askedWool] = name;
                    askedWool++;
                }
                break;
            default:
                break;
        }
    }

    public void dekrementAskedRessource(string name)
    {
        switch (name)
        {
            case "Brick":
                {
                    createdTrade.givenRessources[askedBrick] = null;
                    askedBrick--;
                }
                break;
            case "Wheat":
                {
                    createdTrade.givenRessources[askedBrick] = null;
                    askedBrick--;
                }
                break;
            case "Ore":
                {
                    createdTrade.givenRessources[askedOre] = null;
                    askedOre--;
                }
                break;
            case "Wood":
                {
                    createdTrade.givenRessources[askedWood] = null;
                    askedWood--;
                }
                break;
            case "Wool":
                {
                    createdTrade.givenRessources[askedWool] = null;
                    askedWool--;
                }
                break;
            default:
                break;
        }
    }
}
