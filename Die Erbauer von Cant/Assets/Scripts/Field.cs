using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field
{

    Pawn[] pawns = new Pawn[12];
    string resourceName;
    public int chipNumber;

    public Field(string _resourceName, int _chipNumber)
    {
        resourceName = _resourceName;
        chipNumber = _chipNumber;
    }

    public void AddPawn(int position, string color, string designation)
    {
        Pawn AddedPawn = new Pawn(designation, color);
        pawns[position] = AddedPawn;
    }
	

}
