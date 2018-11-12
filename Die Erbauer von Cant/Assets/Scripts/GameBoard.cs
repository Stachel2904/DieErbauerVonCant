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
        List<Place> possiblePositions = new List<Place>();

        for (int i = 0; i < tiles.Length; i++)
        {
            for (int j = 0; j < tiles[i].pawns.Length; j++)
			{
                Pawn cachedPawn = tiles[i].pawns[j];

                //this position 
                if(cachedPawn != null)
                {
                    if(cachedPawn.type == "Village" && cachedPawn.color == buildedPawn.color)
                    {
                        Place newPlace = new Place();

                        if(j == 1)
                        {
                            newPlace.usedFields = new Field[]{};
                        }

                    }
                    continue;
                }

                if(buildedPawn.type == "Town")
                {

                }

                if(buildedPawn.type == "Street")
                {

                }

			}
        }

        return possiblePositions.ToArray();
    }
}
