using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateTrade : MonoBehaviour {

    public Trade createdTrade = new Trade();


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


    public void createAskedPlayer(string name)
    {
        createdTrade.taker = name;
    }
    public void inkrementGivenRessource(string name)
    {
        switch (name)
        {
            case "Brick":
                {
                    createdTrade.givenRessources[givenBrick] = name;
                    givenBrick++;
                    
                }
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
