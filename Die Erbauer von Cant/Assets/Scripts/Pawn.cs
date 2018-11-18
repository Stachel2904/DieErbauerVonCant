using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn
{

    public string type;
    public string color;


    public Pawn(string _type, string _color)
    {
        type = _type;
        color = _color;
    }

	public Field[] GetFields()
    {
        List<Field> result = new List<Field>();

        for (int i = 0; i < GameBoard.MainBoard.tilesGrid.Length; i++)
        {
            for (int j = 0; j < GameBoard.MainBoard.tilesGrid[i].Length; j++)
            {
                for (int k = 0; k < GameBoard.MainBoard.tilesGrid[i][j].pawns.Length; k++)
                {
                    if (GameBoard.MainBoard.tilesGrid[i][j].pawns[k] != null && GameBoard.MainBoard.tilesGrid[i][j].pawns[k].Equals(this))
                    {
                        result.Add(GameBoard.MainBoard.tilesGrid[i][j]);
                    }
                }
            }
        }

        return result.ToArray();
    }

    public int[] GetPosAtField()
    {
        List<int> result = new List<int>();

        Field[] fields = GetFields();

        for (int i = 0; i < GameBoard.MainBoard.tilesGrid.Length; i++)
        {
            for (int j = 0; j < GameBoard.MainBoard.tilesGrid[i].Length; j++)
            {
                for (int k = 0; k < GameBoard.MainBoard.tilesGrid[i][j].pawns.Length; k++)
                {
                    if (GameBoard.MainBoard.tilesGrid[i][j].pawns[k] != null && GameBoard.MainBoard.tilesGrid[i][j].pawns[k].Equals(this))
                    {
                        result.Add(k);
                    }
                }
            }
        }

        return result.ToArray();
    }
}
