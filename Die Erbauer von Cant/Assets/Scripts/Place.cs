using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Place : MonoBehaviour{

    public Field[] usedFields;
    public int[] posAtField;

    private void OnMouseDown()
    {
        for (int i = 0; i < GameObject.Find("Places").transform.childCount; i++)
        {
            GameObject.Find("Places").transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
        }
        this.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
        GameObject buildAcceptionInterface = GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().BuildAcceptionInterface;
        buildAcceptionInterface.SetActive(true);
        buildAcceptionInterface.transform.Find("Accept").gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        buildAcceptionInterface.transform.Find("Accept").gameObject.GetComponent<Button>().onClick.AddListener(delegate { GamePlay.Main.buildPawn(this); });
    }
}
