using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard
{
    public Field[] tiles = new Field[16];
    
    public void SpreadResources(int rolledNumber)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (rolledNumber == tiles[i].chipNumber)
            {
                foreach (var element in tiles[i].pawns)
                {
                    if (element.type == "village")
                    {
                        GamePlay.Main.GetCurrentPlayer().inventory.AddItem(tiles[i].resourceName, 1);
                    }
                    if (element.type == "Town")
                    {
                        GamePlay.Main.GetCurrentPlayer().inventory.AddItem(tiles[i].resourceName, 2);
                    }
                    
                }
            }
        }
    }

    public Place[] GetAllPositions(Pawn buildedPawn)
    {
        //TODO: check here all of the possible positions
        return null;
    }
}
