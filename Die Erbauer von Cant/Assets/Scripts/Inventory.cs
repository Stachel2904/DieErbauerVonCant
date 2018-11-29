using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory
{
    
    // Use this for initialization

    /// <summary>
    /// This Dictionary contains every usable Item the Character can have
    /// </summary>
    public Dictionary<string, int> inven = new Dictionary<string, int>();

    public Inventory()
    {
        inven.Add("Brick", 0);
        inven.Add("Wheat", 0);
        inven.Add("Ore", 0);
        inven.Add("Wood", 0);
        inven.Add("Wool", 0);
        inven.Add("Street", 13);
        inven.Add("Village", 3);
        inven.Add("Town", 4);
    }
    public void AddItem(string name, int count = 1) {
        inven[name] += count;
    }
    /// <summary>
    /// Removes Ressources from Inventory. If you remove a Pawn, the Ressources will be automatically removed.
    /// </summary>
    /// <param name="name">Ressource or Pawn</param>
    /// <param name="count">How much should be removed ?</param>
    /// <returns></returns>
    public void RemoveItem(string name, int count = 1)
    {   
        if(!CheckInventory(name, count))
        {
            Debug.LogError("Not enough Ressources! You should use CheckInventory() first!!");
            return;
        }

        if (name == "Street")
        {
            RemoveItem("Wood", count);
            RemoveItem("Brick", count);
        }
        else if (name == "Village")
        {
            RemoveItem("Wood", count);
            RemoveItem("Brick", count);
            RemoveItem("Wool", count);
            RemoveItem("Wheat", count);
        }
        else if (name == "Town")
        {
            RemoveItem("Ore", 3 * count);
            RemoveItem("Wheat", 2 * count);
            AddItem("Village");
        }

        inven[name] -= count;
    }

    /// <summary>
    /// check, if the Inventory has at least the amount of a specific type
    /// </summary>
    /// <param name="type"></param>
    /// <param name="amount"></param>
    /// <returns>returns false, if there's not enough in the inventory</returns>
    public bool CheckInventory(string type, int amount = 1)
    {   
        if(inven[type] >= amount)
        {
            if (type == "Street")
            {
                if (CheckInventory("Wood", amount) && CheckInventory("Brick", amount))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (type == "Village")
            {
                if (CheckInventory("Wood", amount) && CheckInventory("Brick", amount) && CheckInventory("Wool", amount) && CheckInventory("Wheat", amount))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (type == "Town")
            {
                if (CheckInventory("Ore", 3 * amount) && CheckInventory("Wheat", 2 * amount))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }
}
