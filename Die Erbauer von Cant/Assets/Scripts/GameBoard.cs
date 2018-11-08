using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard
{

    public Field[] tiles;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpreadResources(int rolledNumber)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (rolledNumber == tiles[i].chipNumber)
            {
                foreach (var element in tiles[i].pawns)
                {
                    if (element.designation == "village")
                    {
                        GamePlay.Main.players[element.color].inventory.AddItem(tiles[i].resourceName, 1);
                    }
                    if (element.designation == "city")
                    {
                        GamePlay.Main.players[element.color].inventory.AddItem(tiles[i].resourceName, 2);
                    }
                    
                }
            }
        }
    }
    public void GetAllPositions(Pawn buildedPawn)
    {

    }
}
