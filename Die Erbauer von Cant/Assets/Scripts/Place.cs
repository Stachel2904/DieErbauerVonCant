using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Place : MonoBehaviour{

    public Field[] usedFields;
    public int[] posAtField;

    public Pawn buildedPawn;

    private void OnMouseDown()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        for (int i = 0; i < GameObject.Find("Places").transform.childCount; i++)
        {
            GameObject.Find("Places").transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
        }
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        GameObject buildAcceptionInterface = GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().BuildAcception;
        buildAcceptionInterface.SetActive(true);
        buildAcceptionInterface.transform.Find("Accept").gameObject.GetComponent<Button>().onClick.RemoveAllListeners();

        //make int[] from Place
        int[] place = new int[usedFields.Length * 3];
        for (int i = 0; i < place.Length; i += 3)
        {
            place[i] = usedFields[i / 3].row;
            place[i + 1] = usedFields[i / 3].column;
            place[i + 2] = posAtField[i / 3];
        }
        buildAcceptionInterface.transform.Find("Accept").gameObject.GetComponent<Button>().onClick.AddListener(delegate { GameObject.Find("ClientManager").GetComponent<NetworkClientMessagerHandler>().SendFieldUpdateToServer(buildedPawn.type, buildedPawn.color, place); });
        buildAcceptionInterface.transform.Find("Accept").gameObject.GetComponent<Button>().onClick.AddListener(GameBoard.MainBoard.deleteAllPlaces);
    }
}
