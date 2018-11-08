using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    // Use this for initialization

    /// <summary>
    /// This Dictionary contains every usable Item the Character can have
    /// </summary>
    public Dictionary<string, int> inven = new Dictionary<string, int>();

    private void Start()
    {
        inven.Add("Wood", 0);
        inven.Add("Ore", 0);
        inven.Add("Brick", 0);
        inven.Add("Wool", 0);
        inven.Add("Wheat", 0);
        inven.Add("Street", 15);
        inven.Add("Village", 5);
        inven.Add("Town", 4);
    }

    public void AddItem(string name, int count)
    {
        inven[name] += count;
    }

    public bool RemoveItem(string name, int count)
    {
        if (inven[name] >= count)
        {
            inven[name] -= count;
            return true;
        }
        else
        {
            return false;
        }
        

    }
    
    // Update is called once per frame
    void Update ()
    {

        

	}
}
