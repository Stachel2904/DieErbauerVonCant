using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard
{

    
    public Field[] allBuildFields = new Field[16];

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpreadResources(int rolledNumber)
    {
        for (int i = 0; i < allBuildFields.Length; i++)
        {
            if (rolledNumber == allBuildFields[i].chipNumber)
            {
                foreach (var element in allBuildFields[i].pawns)
                {
                    if (element.type == "village")
                    {
                        GamePlay.Main.GetCurrentPlayer().inventory.AddItem(allBuildFields[i].resourceName, 1);
                    }
                    if (element.type == "Town")
                    {
                        GamePlay.Main.GetCurrentPlayer().inventory.AddItem(allBuildFields[i].resourceName, 2);
                    }
                    
                }
            }
        }
    }
    public void GetAllPositions(Pawn buildedPawn)
    {
        
    }
}
