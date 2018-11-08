using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public Inventory inventory = new Inventory();
    string name;
    string color;

    public Player(string _name, string _color)
    {
        name = _name;
        color = _color;
    }
	
	
	// Update is called once per frame
	void Update () {
		
	}
}
