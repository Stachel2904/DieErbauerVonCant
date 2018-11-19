using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayClient : MonoBehaviour {
    
    public Player ownPlayer;

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
        GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().BuildSelection.transform.Find("Street").GetChild(0).gameObject.GetComponent<Image>().color = ownColor;
        GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().BuildSelection.transform.Find("Village").GetChild(0).gameObject.GetComponent<Image>().color = ownColor;
        GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().BuildSelection.transform.Find("Town").GetChild(0).gameObject.GetComponent<Image>().color = ownColor;
        
        GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().ClientDefault.transform.Find("TradeButton").GetComponent<Button>().interactable = false;
        GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().ClientDefault.transform.Find("BuildButton").GetComponent<Button>().interactable = false;
        GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().ClientDefault.transform.Find("Next Player").GetComponent<Button>().interactable = false;
        GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().ChooseTradePlayer.transform.Find("Player4Button").GetComponent<Button>().interactable = false;
    }

    public void NextPlayer()
    {
        GameObject.Find("ClientManager").GetComponent<NetworkClientMessagerHandler>().SendToServer("Next Player");
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
            return;
        }

        //Ressourcen überprüfen
        if (!ownPlayer.inventory.CheckInventory(type))
        {
            Debug.Log("You have not enough Ressources...");
            GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().ClientDefault.SetActive(true);
            return;
        }

        Pawn buildedPawn = new Pawn(type, ownPlayer.color);

        //Alle möglichen Positionen ausgeben
        Place[] possiblePlaces = GameBoard.MainBoard.GetAllPositions(buildedPawn);

        if (possiblePlaces.Length == 0)
        {
            Debug.Log("Du kannst nirgendwo ein" + ((buildedPawn.type == "Village") ? " Dorf" : ((buildedPawn.type == "Street") ? "e Straße" : "e Stadt")) + " bauen...");
            GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().ClientDefault.SetActive(true);
        }

        for (int i = 0; i < possiblePlaces.Length; i++)
        {
            Vector3 placePosition = GetPosInWorld(possiblePlaces[i].usedFields[0], possiblePlaces[i].posAtField[0], buildedPawn.type);

            possiblePlaces[i].gameObject.transform.position = placePosition;
            possiblePlaces[i].buildedPawn = buildedPawn;

            for (int j = 0; j < GameObject.Find("Places").transform.childCount; j++)
            {
                if (Vector3.Distance(GameObject.Find("Places").transform.GetChild(j).position, placePosition) < 0.5f && !GameObject.Find("Places").transform.GetChild(j).gameObject.Equals(possiblePlaces[i].gameObject))
                {
                    GameObject.Destroy(possiblePlaces[i].gameObject);
                }
            }
        }
    }

    private Vector3 GetPosInWorld(Field usedField, int posAtField, string type)
    {
        Vector3 result = new Vector3();

        //Get pos of Field
        result.x = usedField.column * -6 - 6;
        result.y = 0;
        result.z = usedField.row * 10.5f + 8.5f;

        //get Pos from PosAtField
        result += Quaternion.Euler(0, 30 * posAtField, 0) * Vector3.back * ((type == "Street") ? 6.0f : 14.0f / 2.0f);

        return result;
    }

    public void buildPawn(Place destination)
    {
        Debug.Log("Huch, die funktion wird trotzdem aufgerufen...");
        /*
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

        GameBoard.MainBoard.pawns[(int) ConvertColor(buildedPawn.color)].Add(buildedPawn);

        //Pawn kreieren (erst nur mesh, dann Farbe, dann position)
        Transform createdPawn = Instantiate(Resources.Load<Transform>("Prefabs/" + buildedPawn.type), GameObject.Find("Board").transform);
        createdPawn.gameObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/" + buildedPawn.color);
        createdPawn.position = destination.gameObject.transform.position;

        //Wenn eine Stadt platziert wird, wird die Siedlung gelöscht
        if (buildedPawn.type == "Town")
        {
            for (int i = 0; i < GameObject.Find("Board").transform.childCount; i++)
            {
                GameObject currentPawn = GameObject.Find("Board").transform.GetChild(i).gameObject;

                if (currentPawn.name == "Village(Clone)")
                {
                    if (Vector3.Distance(destination.gameObject.transform.position, currentPawn.transform.position) < 0.5f)
                    {
                        GameObject.Destroy(currentPawn);
                    }
                }
            }
        }
        else if (buildedPawn.type == "Street")
        {
            createdPawn.Rotate(0.0f, 30.0f * destination.posAtField[0], 0.0f);
        }

        //Places löschen
        for (int i = 0; i < GameObject.Find("Places").transform.childCount; i++)
        {
            GameObject.Destroy(GameObject.Find("Places").transform.GetChild(i).gameObject);
        }

        //Sag Server, dass du etwas gebaut hast
        //GameObject.Find("ClientManager").GetComponent<NetworkClientMessagerHandler>().SendFieldUpdateToServer(buildedPawn.type, destination);
        */
    }

    public void UpdateBoard(Pawn buildedPawn, int[] place)
    {
        Field[] usedFields = new Field[place.Length / 3];
        int[] posAtField = new int[place.Length / 3];
        for (int i = 0; i < usedFields.Length * 3; i += 3)
        {
            usedFields[i / 3] = GameBoard.MainBoard.tilesGrid[place[i]][place[i + 1]];
            posAtField[i / 3] = place[i + 2];
        }


        Vector3 pos = GetPosInWorld(usedFields[0], posAtField[0], buildedPawn.type);
        //Pawn kreieren (erst nur mesh, dann Farbe, dann position)
        Transform createdPawn = Instantiate(Resources.Load<Transform>("Prefabs/" + buildedPawn.type), GameObject.Find("Board").transform);
        createdPawn.gameObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/" + buildedPawn.color);
        createdPawn.position = pos;

        GameBoard.MainBoard.pawns[(int)ConvertColor(buildedPawn.color)].Add(buildedPawn);
        for (int i = 0; i < usedFields.Length; i++)
        {
            if (usedFields[i] != null) {
                GameBoard.MainBoard.tilesGrid[usedFields[i].row][usedFields[i].column].pawns[posAtField[i]] = buildedPawn;
            }
        }

        if (buildedPawn.type == "Town")
        {
            for (int i = 0; i < GameObject.Find("Board").transform.childCount; i++)
            {
                GameObject currentPawn = GameObject.Find("Board").transform.GetChild(i).gameObject;

                if (currentPawn.name == "Village(Clone)")
                {
                    if (Vector3.Distance(pos, currentPawn.transform.position) < 0.5f)
                    {
                        GameObject.Destroy(currentPawn);
                    }
                }
            }
        }
        else if (buildedPawn.type == "Street")
        {
            createdPawn.Rotate(0.0f, 30.0f * posAtField[0], 0.0f);
        }
    }
    #endregion

    public PlayerColor ConvertColor(string color)
    {
        PlayerColor result = PlayerColor.NONE;
        switch (color)
        {
            case "Blue":
                result = PlayerColor.BLUE;
                break;
            case "Red":
                result = PlayerColor.RED;
                break;
            case "Orange":
                result = PlayerColor.ORANGE;
                break;
            case "White":
                result = PlayerColor.WHITE;
                break;
            default:
                Debug.Log("Die zu bauende Spielfigur hat die undefinierte Farbe: " + color);
                break;
        }
        return result;
    }
}
