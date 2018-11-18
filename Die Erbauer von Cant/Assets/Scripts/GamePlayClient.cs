using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayClient : MonoBehaviour {

    private Pawn buildedPawn;

    public Player ownPlayer;

    void Start ()
    {        
		//Get Right Color in Build Selection
        //look for right images and set same the same as "OwnColor".color
	}

    public void InitClient(string color)
    {
        ownPlayer = new Player("", color);

        Color ownColor = Color.white;

        switch (color)
        {
            case "Orange":
                ownColor = new Color(255, 150, 0, 255);
                break;
            case "Blue":
                ownColor = Color.blue;
                break;
            case "White":
                ownColor = Color.white;
                break;
            case "Red":
                ownColor = Color.red;
                break;
        }

        GameObject.Find("OwnColor").GetComponent<Image>().color = ownColor;
        GameObject.Find("BuildSelection").transform.Find("Street").gameObject.GetComponent<Image>().color = ownColor;
        GameObject.Find("BuildSelection").transform.Find("Village").gameObject.GetComponent<Image>().color = ownColor;
        GameObject.Find("BuildSelection").transform.Find("Town").gameObject.GetComponent<Image>().color = ownColor;
        
        GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().ClientDefault.transform.Find("TradeButton").GetComponent<Button>().interactable = false;
        GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().ClientDefault.transform.Find("BuildButton").GetComponent<Button>().interactable = false;
        GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().ClientDefault.transform.Find("Next Player").GetComponent<Button>().interactable = false;
    }

    public void NextPlayer()
    {
        GameObject.Find("ClientManager").GetComponent<NetworkClientMessagerHandler>().SendToServer("NextPlayer");
        GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().ClientDefault.transform.Find("TradeButton").GetComponent<Button>().interactable = false;
        GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().ClientDefault.transform.Find("BuildButton").GetComponent<Button>().interactable = false;
        GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().ClientDefault.transform.Find("Next Player").GetComponent<Button>().interactable = false;
    }

    #region Build
    /// <summary>
    /// Check if you have enough Ressources and print possible Positions
    /// </summary>
    /// <param name="tryBuildedPawn">The pawn you wish to build</param>
    public void TryBuild(string type)
    {
        //set buildedPawn back to null
        if (type == "")
        {
            buildedPawn = null;
            return;
        }

        //Ressourcen überprüfen
        //if (!GetCurrentPlayer().inventory.CheckInventory(type))
        //{
        //    Debug.Log("You have not enough Ressources...");
        //    return;
        //}

        buildedPawn = new Pawn(type, ownPlayer.color);
        Debug.Log("Started building a " + buildedPawn.color + " " + buildedPawn.type + ".");

        //Alle möglichen Positionen ausgeben
        Place[] possiblePlaces = GameBoard.MainBoard.GetAllPositions(buildedPawn);

        if (possiblePlaces.Length == 0)
        {
            Debug.Log("Du kannst nirgendwo ein" + ((buildedPawn.type == "Village") ? " Dorf" : ((buildedPawn.type == "Street") ? "e Straße" : "e Stadt")) + " bauen...");
        }

        for (int i = 0; i < possiblePlaces.Length; i++)
        {

            bool alreadyBuilded = false;
            Vector3 placePosition = GetPosInWorld(possiblePlaces[i].usedFields[0], possiblePlaces[i].posAtField[0]);

            for (int j = 0; j < GameObject.Find("Places").transform.childCount; j++)
            {
                if (Vector3.Distance(GameObject.Find("Places").transform.GetChild(j).position, placePosition) < 0.1f)
                {
                    alreadyBuilded = true;
                }
            }

            //create PlaceObject
            if (!alreadyBuilded)
            {
                Place createdPlace = Instantiate(Resources.Load<Place>("Prefabs/PossiblePlace"), GameObject.Find("Places").transform);
                createdPlace.usedFields = possiblePlaces[i].usedFields;
                createdPlace.posAtField = possiblePlaces[i].posAtField;
                createdPlace.gameObject.transform.position = placePosition;
            }
        }
    }

    private Vector3 GetPosInWorld(Field usedField, int posAtField)
    {
        Vector3 result = new Vector3();

        //Get pos of Field
        result.x = usedField.column * -6 + 6;
        result.y = 0;
        result.z = usedField.row * 6.93f + (3.46f + 6.93f / 2);

        //get Pos from PosAtField
        result += Quaternion.Euler(0, 30 * posAtField, 0) * Vector3.back * ((buildedPawn.type == "Street") ? 6.0f : 16.86f / 2.0f);

        return result;
    }

    public void buildPawn(Place destination)
    {
        if (buildedPawn == null)
        {
            Debug.Log("Which pawn do you want to build?");
            return;
        }

        //Ressourcenmanagement/Rohstoffe abziehen etc.
        ownPlayer.inventory.RemoveItem(buildedPawn.type);

        //An die richtige Position setzen und die angrenzenden Tiles updaten
        for (int i = 0; i < GameBoard.MainBoard.tilesGrid.Length; i++)
        {
            for (int j = 0; j < GameBoard.MainBoard.tilesGrid[i].Length; j++)
            {
                for (int k = 0; k < destination.usedFields.Length; k++)
                {
                    if (GameBoard.MainBoard.tilesGrid[i][j].Equals(destination.usedFields[k]))
                    {
                        GameBoard.MainBoard.tilesGrid[i][j].pawns[destination.posAtField[k]] = buildedPawn;
                    }
                }
            }
        }

        //Pawn kreieren (erst nur mesh, dann Farbe, dann position)
        Transform createdPawn = Instantiate(Resources.Load<Transform>("Prefabs/" + buildedPawn.type), GameObject.Find("Board").transform);
        createdPawn.gameObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/" + buildedPawn.color);
        createdPawn.position = destination.gameObject.transform.position;

        //Places löschen
        for (int i = 0; i < GameObject.Find("Places").transform.childCount; i++)
        {
            GameObject.Destroy(GameObject.Find("Places").transform.GetChild(i).gameObject);
        }

        //Sag Server, dass du etwas gebaut hast
        GameObject.Find("ClientManager").GetComponent<NetworkClientMessagerHandler>().SendFieldUpdateToServer(buildedPawn.type, destination);
    }

    public void UpdateBoard(Pawn buildedPawn, Place destination)
    {
        //Pawn kreieren (erst nur mesh, dann Farbe, dann position)
        Transform createdPawn = Instantiate(Resources.Load<Transform>("Prefabs/" + buildedPawn.type), GameObject.Find("Board").transform);
        createdPawn.gameObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/" + buildedPawn.color);
        createdPawn.position = destination.gameObject.transform.position;
    }
    #endregion
    
}
