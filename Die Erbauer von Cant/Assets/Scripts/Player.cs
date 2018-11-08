using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public Inventory inventory = new Inventory();
    string name;
    string color;
    int clientID;

    public Player(int _clientID, string _name, string _color)
    {
        clientID = _clientID;
        //name = _name;
        color = _color;

    }
	
	
	// Update is called once per frame
	void Update () {
		
	}
}
