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
        if(corner % 2 != 0)
        {
            Field resultField = null;

            //check if the place is on the water and may not have a second field
            bool fieldOnTop = (this.row == 0 && (corner == 1 || corner == 11));
            bool fieldOnRight = ((this.column == 7 || this.column == 8) && corner == 3);
            bool fieldOnBottom = (this.row == 4 && (corner == 5 || corner == 7));
            bool fieldOnLeft = ((this.column == 0 || this.column == 1) && corner == 9);

            //erlangt das gespiegelte feld, wenn es nciht außerhalb des Spielfeldes ist
            if (!(fieldOnTop && fieldOnRight && fieldOnBottom && fieldOnLeft))
            {
                if (corner == 1)
                {
                    resultField = GameBoard.MainBoard.tilesGrid[this.row - 1][this.column + 1];
                }
                else if (corner == 3)
                {
                    resultField = GameBoard.MainBoard.tilesGrid[this.row][this.column + 2];
                }
                else if (corner == 5)
                {
                    resultField = GameBoard.MainBoard.tilesGrid[this.row + 1][this.column + 1];
                }
                else if (corner == 7)
                {
                    resultField = GameBoard.MainBoard.tilesGrid[this.row + 1][this.column - 1];
                }
                else if (corner == 9)
                {
                    resultField = GameBoard.MainBoard.tilesGrid[this.row][this.column - 2];
                }
                else if (corner == 11)
                {
                    resultField = GameBoard.MainBoard.tilesGrid[this.row - 1][this.column - 1];
                }
            }
            result.Add(resultField);
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
