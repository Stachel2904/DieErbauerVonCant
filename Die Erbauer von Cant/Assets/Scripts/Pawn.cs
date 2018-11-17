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
        for (int i = 0; i < GameBoard.MainBoard.tiles.Length; i++)
        {
            for (int j = 0; j < GameBoard.MainBoard.tiles[i].pawns.Length; j++)
            {
                if (GameBoard.MainBoard.tiles[i].pawns[j].Equals(this))
                {
                    result.Add(GameBoard.MainBoard.tiles[i]);
                }
            }
        }

        return result.ToArray();
    }

    public int[] GetPosAtField()
    {
        List<int> result = new List<int>();

        Field[] fields = GetFields();

        for (int i = 0; i < fields.Length; i++)
        {
            for (int j = 0; j < fields[i].pawns.Length; j++)
            {
                if (fields[i].pawns[j].Equals(this))
                {
                    result.Add(j);
                }
            }
        }

        return result.ToArray();
    }
}
