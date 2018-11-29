using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayClient : MonoBehaviour {

    //Singleton
    private static GamePlayClient main;
    public static GamePlayClient Main
    {
        get
        {
            if (main == null)
            {
                main = GameObject.Find("GamePlay").GetComponent<GamePlayClient>();
            }
            return main;
        }
    }

    public Player ownPlayer;

    public void InitClient(string color)
    {
        main = GameObject.Find("GamePlay").GetComponent<GamePlayClient>();

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
        for (int i = 0; i < GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().PawnSprites.Length; i++)
        {
            GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().PawnSprites[i].color = ownColor;
        }

        GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().BuildSelection.transform.Find("Street").gameObject.GetComponent<Button>().onClick.AddListener(delegate { TryBuild("Street"); });
        GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().BuildSelection.transform.Find("Village").gameObject.GetComponent<Button>().onClick.AddListener(delegate { TryBuild("Village"); });
        GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().BuildSelection.transform.Find("Town").gameObject.GetComponent<Button>().onClick.AddListener(delegate { TryBuild("Town"); });

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

    public void RequestSecondVillageRessources()
    {
        GameObject.Find("ClientManager").GetComponent<NetworkClientMessagerHandler>().SendToServer("Get Second Village Ressources");
    }

    #region Build
    /// <summary>
    /// Check if you have enough Ressources and print possible Positions
    /// </summary>
    /// <param name="tryBuildedPawn">The pawn you wish to build</param>
    public void TryBuild(string type, bool startRound = false)
    {
        //set buildedPawn back to null
        if (type == "")
        {
            return;
        }

        //Ressourcen überprüfen
        if (!ownPlayer.inventory.CheckInventory(type) && !startRound)
        {
            Debug.Log("You have not enough Ressources...");
            GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().ClientDefault.SetActive(true);
            return;
        }

        Pawn buildedPawn = new Pawn(type, ownPlayer.color);

        //Alle möglichen Positionen ausgeben
        Place[] possiblePlaces = GameBoard.MainBoard.GetAllPositions(buildedPawn, startRound);

        if (possiblePlaces.Length == 0)
        {
            Debug.Log("Du kannst nirgendwo ein" + ((buildedPawn.type == "Village") ? " Dorf" : ((buildedPawn.type == "Street") ? "e Straße" : "e Stadt")) + " bauen...");
            GameObject.Find("ClientButtonManager").GetComponent<ClientButtonManager>().ClientDefault.SetActive(true);
            return;
        }

        for (int i = 0; i < possiblePlaces.Length; i++)
        {
            if (possiblePlaces[i].usedFields.Length == 0 || possiblePlaces[i].posAtField.Length == 0)
            {
                GameObject.Destroy(possiblePlaces[i].gameObject);
                continue;
            }

            Vector3 placePosition = GetPosInWorld(possiblePlaces[i].usedFields[0], possiblePlaces[i].posAtField[0], buildedPawn.type);

            possiblePlaces[i].gameObject.transform.position = new Vector3(placePosition.x, possiblePlaces[i].gameObject.transform.position.y, placePosition.z);
            possiblePlaces[i].buildedPawn = buildedPawn;

            for (int j = 0; j < GameObject.Find("Places").transform.childCount; j++)
            {
                if (Vector3.Distance(GameObject.Find("Places").transform.GetChild(j).position, placePosition) < 0.5f && !GameObject.Find("Places").transform.GetChild(j).gameObject.Equals(possiblePlaces[i].gameObject))
                {
                    GameObject.Destroy(possiblePlaces[i].gameObject);
                }
            }

            if (Vector3.Distance(placePosition, Vector3.zero) < 2.5f)
            {
                GameObject.Destroy(possiblePlaces[i].gameObject);
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

    public void UpdateBoard(Pawn buildedPawn, int[] place)
    {
        Field[] usedFields = new Field[place.Length / 3];
        int[] posAtField = new int[place.Length / 3];
        for (int i = 0; i < usedFields.Length * 3; i += 3)
        {
            usedFields[i / 3] = GameBoard.MainBoard.tilesGrid[place[i]][place[i + 1]];
            posAtField[i / 3] = place[i + 2];
        }

        if(usedFields.Length == 0 || posAtField.Length == 0)
        {
            Debug.LogError("Irgendwas ist bei Bauen eines Pawns fehlgeschlagen...");
            return;
        }

        Vector3 pos = GetPosInWorld(usedFields[0], posAtField[0], buildedPawn.type);
        //Pawn kreieren (erst nur mesh, dann Farbe, dann position)
        Transform createdPawn = Instantiate(Resources.Load<Transform>("Prefabs/" + buildedPawn.type), GameObject.Find("Board").transform);
        createdPawn.gameObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/" + buildedPawn.color);
        createdPawn.position = new Vector3(pos.x, createdPawn.position.y, pos.z);

        GameBoard.MainBoard.pawns[(int)ConvertColor(buildedPawn.color)].Add(buildedPawn);
        for (int i = 0; i < usedFields.Length; i++)
        {
            if (usedFields[i] != null)
            {
                Pawn oldPawn = GameBoard.MainBoard.tilesGrid[usedFields[i].row][usedFields[i].column].pawns[posAtField[i]];
                GameBoard.MainBoard.tilesGrid[usedFields[i].row][usedFields[i].column].pawns[posAtField[i]] = buildedPawn;
                if (oldPawn != null)
                {
                    GameBoard.MainBoard.pawns[(int)ConvertColor(oldPawn.color)].Remove(oldPawn);
                }
            }
        }

        if (buildedPawn.type == "Town")
        {
            for (int i = 0; i < GameObject.Find("Board").transform.childCount; i++)
            {
                GameObject currentPawn = GameObject.Find("Board").transform.GetChild(i).gameObject;

                if (currentPawn.name == "Village(Clone)")
                {
                    if (Vector3.Distance(pos, currentPawn.transform.position) < 2.5f)
                    {
                        GameObject.Destroy(currentPawn.gameObject);
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
