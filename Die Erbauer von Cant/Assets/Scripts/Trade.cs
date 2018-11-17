using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trade
{
    public string giver;
    public string taker;


    // 0. Brick, 1. Wheat, 2. Ore, 3. Wood, 4. Wool
    public int[] givenRessources = new int[5];
    public int[] askedRessources = new int[5];

    public void AddGivenRessource(string addedRessource)
    {
        switch (addedRessource)
        {
            case "Brick":
                {
                    if (GamePlay.Main.GetCurrentPlayer().inventory.inven[addedRessource] > givenRessources[0])
                        givenRessources[0]++;
                }
                break;
            case "Wheat":
                {
                    if (GamePlay.Main.GetCurrentPlayer().inventory.inven[addedRessource] > givenRessources[1])
                        givenRessources[1]++;
                }
                break;
            case "Ore":
                {
                    if (GamePlay.Main.GetCurrentPlayer().inventory.inven[addedRessource] > givenRessources[2])
                        givenRessources[2]++;
                }
                break;
            case "Wood":
                {
                    if (GamePlay.Main.GetCurrentPlayer().inventory.inven[addedRessource] > givenRessources[3])
                        givenRessources[3]++;
                }
                break;
            case "Wool":
                {
                    if (GamePlay.Main.GetCurrentPlayer().inventory.inven[addedRessource] > givenRessources[4])
                        givenRessources[4]++;
                }
                break;
            default:
                break;
        }
    }
    public void RemoveGivenRessource(string removedRessource)
    {
        switch (removedRessource)
        {
            case "Brick":
                {
                    if (givenRessources[0] > 0)
                        givenRessources[0]--;
                }
                break;
            case "Wheat":
                {
                    if (givenRessources[1] > 0)
                        givenRessources[1]--;
                }
                break;
            case "Ore":
                {
                    if (givenRessources[2] > 0)
                        givenRessources[2]--;
                }
                break;
            case "Wood":
                {
                    if (givenRessources[3] > 0)
                        givenRessources[3]--;
                }
                break;
            case "Wool":
                {
                    if (givenRessources[4] > 0)
                        givenRessources[4]--;
                }
                break;
            default:
                break;
        }
    }

    public void AddAskedRessource(string addedRessource)
    {
        switch (addedRessource)
        {
            case "Brick":
                {
                    
                    askedRessources[0]++;
                }
                break;
            case "Wheat":
                {
                    askedRessources[1]++;
                }
                break;
            case "Ore":
                {
                    askedRessources[2]++;
                }
                break;
            case "Wood":
                {
                    askedRessources[3]++;
                }
                break;
            case "Wool":
                {
                    askedRessources[4]++;
                }
                break;
            default:
                break;
        }
    }
    public void RemoveAskedRessource(string removedRessource)
    {
        switch (removedRessource)
        {
            case "Brick":
                {
                    if (askedRessources[0] > 0)
                        askedRessources[0]--;
                }
                break;
            case "Wheat":
                {
                    if (askedRessources[1] > 0)
                        askedRessources[1]--;
                }
                break;
            case "Ore":
                {
                    if (askedRessources[2] > 0)
                        askedRessources[2]--;
                }
                break;
            case "Wood":
                {
                    if (askedRessources[3] > 0)
                        askedRessources[3]--;
                }
                break;
            case "Wool":
                {
                    if (askedRessources[4] > 0)
                        askedRessources[4]--;
                }
                break;
            default:
                break;
        }
    }
}



