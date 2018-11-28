using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field
{

    public Pawn[] pawns = new Pawn[12];
    public string resourceName;
    public int chipNumber;
    public int row;
    public int column;

    public Field() {
        //t
    }
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
	
    public Field[] GetConnectedFields(int corner)
    {
        List<Field> result = new List<Field>();

        //Path
        if (corner % 2 != 0)
        {
            //erlangt das gespiegelte feld, wenn es nciht außerhalb des Spielfeldes ist
            if (corner == 1)
            {
                if (this.row - 1 >= 0 && this.row - 1 < GameBoard.MainBoard.tilesGrid.Length)
                {
                    if (this.column + 1 >= 0 && this.column + 1 < GameBoard.MainBoard.tilesGrid[this.row - 1].Length)
                    {
                        result.Add(GameBoard.MainBoard.tilesGrid[this.row - 1][this.column + 1]);
                    }
                }
            }
            else if (corner == 3)
            {
                if (this.row >= 0 && this.row < GameBoard.MainBoard.tilesGrid.Length)
                {
                    if (this.column + 2 >= 0 && this.column + 2 < GameBoard.MainBoard.tilesGrid[this.row].Length)
                    {
                        result.Add(GameBoard.MainBoard.tilesGrid[this.row][this.column + 2]);
                    }
                }
            }
            else if (corner == 5)
            {
                if (this.row + 1 >= 0 && this.row + 1 < GameBoard.MainBoard.tilesGrid.Length)
                {
                    if (this.column + 1 >= 0 && this.column + 1 < GameBoard.MainBoard.tilesGrid[this.row + 1].Length)
                    {
                        result.Add(GameBoard.MainBoard.tilesGrid[this.row + 1][this.column + 1]);
                    }
                }
            }
            else if (corner == 7)
            {
                if (this.row + 1 >= 0 && this.row + 1 < GameBoard.MainBoard.tilesGrid.Length)
                {
                    if (this.column - 1 >= 0 && this.column - 1 < GameBoard.MainBoard.tilesGrid[this.row + 1].Length)
                    {
                        result.Add(GameBoard.MainBoard.tilesGrid[this.row + 1][this.column - 1]);
                    }
                }
            }
            else if (corner == 9)
            {
                if (this.row >= 0 && this.row < GameBoard.MainBoard.tilesGrid.Length)
                {
                    if (this.column - 2 >= 0 && this.column - 2 < GameBoard.MainBoard.tilesGrid[this.row].Length)
                    {
                        result.Add(GameBoard.MainBoard.tilesGrid[this.row][this.column - 2]);
                    }
                }
            }
            else if (corner == 11)
            {
                if (this.row - 1 >= 0 && this.row - 1 < GameBoard.MainBoard.tilesGrid.Length)
                {
                    if (this.column - 1 >= 0 && this.column - 1 < GameBoard.MainBoard.tilesGrid[this.row - 1].Length)
                    {
                        result.Add(GameBoard.MainBoard.tilesGrid[this.row - 1][this.column - 1]);
                    }
                }
            }
        }
        //Corner
        else
        {
            if (corner == 0)
            {
                if (this.row - 1 >= 0 && this.row - 1 < GameBoard.MainBoard.tilesGrid.Length)
                {
                    if (this.column - 1 >= 0 && this.column - 1 < GameBoard.MainBoard.tilesGrid[this.row - 1].Length)
                    {
                        result.Add(GameBoard.MainBoard.tilesGrid[this.row - 1][this.column - 1]);
                    }
                }
                if (this.row - 1 >= 0 && this.row - 1 < GameBoard.MainBoard.tilesGrid.Length)
                {
                    if (this.column + 1 >= 0 && this.column + 1 < GameBoard.MainBoard.tilesGrid[this.row - 1].Length)
                    {
                        result.Add(GameBoard.MainBoard.tilesGrid[this.row - 1][this.column + 1]);
                    }
                }
            }
            else if (corner == 2)
            {
                if (this.row - 1 >= 0 && this.row - 1 < GameBoard.MainBoard.tilesGrid.Length)
                {
                    if (this.column + 1 >= 0 && this.column + 1 < GameBoard.MainBoard.tilesGrid[this.row - 1].Length)
                    {
                        result.Add(GameBoard.MainBoard.tilesGrid[this.row - 1][this.column + 1]);
                    }
                }
                if (this.row >= 0 && this.row < GameBoard.MainBoard.tilesGrid.Length)
                {
                    if (this.column + 2 >= 0 && this.column + 2 < GameBoard.MainBoard.tilesGrid[this.row].Length)
                    {
                        result.Add(GameBoard.MainBoard.tilesGrid[this.row][this.column + 2]);
                    }
                }
            }
            else if (corner == 4)
            {
                if (this.row >= 0 && this.row < GameBoard.MainBoard.tilesGrid.Length)
                {
                    if (this.column + 2 >= 0 && this.column + 2 < GameBoard.MainBoard.tilesGrid[this.row].Length)
                    {
                        result.Add(GameBoard.MainBoard.tilesGrid[this.row][this.column + 2]);
                    }
                }
                if (this.row + 1 >= 0 && this.row + 1 < GameBoard.MainBoard.tilesGrid.Length)
                {
                    if (this.column + 1 >= 0 && this.column + 1 < GameBoard.MainBoard.tilesGrid[this.row + 1].Length)
                    {
                        result.Add(GameBoard.MainBoard.tilesGrid[this.row + 1][this.column + 1]);
                    }
                }
            }
            else if (corner == 6)
            {
                if (this.row + 1 >= 0 && this.row + 1 < GameBoard.MainBoard.tilesGrid.Length)
                {
                    if (this.column + 1 >= 0 && this.column + 1 < GameBoard.MainBoard.tilesGrid[this.row + 1].Length)
                    {
                        result.Add(GameBoard.MainBoard.tilesGrid[this.row + 1][this.column + 1]);
                    }
                }
                if (this.row + 1 >= 0 && this.row + 1 < GameBoard.MainBoard.tilesGrid.Length)
                {
                    if (this.column - 1 >= 0 && this.column - 1 < GameBoard.MainBoard.tilesGrid[this.row + 1].Length)
                    {
                        result.Add(GameBoard.MainBoard.tilesGrid[this.row + 1][this.column - 1]);
                    }
                }
            }
            else if (corner == 8)
            {
                if (this.row + 1 >= 0 && this.row + 1 < GameBoard.MainBoard.tilesGrid.Length)
                {
                    if (this.column - 1 >= 0 && this.column - 1 < GameBoard.MainBoard.tilesGrid[this.row + 1].Length)
                    {
                        result.Add(GameBoard.MainBoard.tilesGrid[this.row + 1][this.column - 1]);
                    }
                }
                if (this.row >= 0 && this.row < GameBoard.MainBoard.tilesGrid.Length)
                {
                    if (this.column - 2 >= 0 && this.column - 2 < GameBoard.MainBoard.tilesGrid[this.row].Length)
                    {
                        result.Add(GameBoard.MainBoard.tilesGrid[this.row][this.column - 2]);
                    }
                }
            }
            else if (corner == 10)
            {
                if (this.row >= 0 && this.row < GameBoard.MainBoard.tilesGrid.Length)
                {
                    if (this.column - 2 >= 0 && this.column - 2 < GameBoard.MainBoard.tilesGrid[this.row].Length)
                    {
                        result.Add(GameBoard.MainBoard.tilesGrid[this.row][this.column - 2]);
                    }
                }
                if (this.row - 1 >= 0 && this.row - 1 < GameBoard.MainBoard.tilesGrid.Length)
                {
                    if (this.column - 1 >= 0 && this.column - 1 < GameBoard.MainBoard.tilesGrid[this.row - 1].Length)
                    {
                        result.Add(GameBoard.MainBoard.tilesGrid[this.row - 1][this.column - 1]);
                    }
                }
            }
        }

        return result.ToArray();
    }
}
