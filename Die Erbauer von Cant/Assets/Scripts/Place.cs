using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place : MonoBehaviour{

    public Field[] usedFields;
    public int[] posAtField;

    private void OnMouseDown()
    {
        GamePlay.Main.buildPawn(this);
    }
}
