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
                
            }
        }
    }
}
