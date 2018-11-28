using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameBoard
{
    //Singleton
    private static GameBoard mainBoard;
    public static GameBoard MainBoard
    {
        get
        {
            if (mainBoard == null)
            {
                mainBoard = new GameBoard();
            }
            return mainBoard;
        }
    }

    public Field[] tiles = new Field[16];
    public Field[][] tilesGrid = new Field[][] { new Field[9], new Field[9], new Field[9], new Field[9], new Field[9]};

    public List<Pawn>[] pawns = new List<Pawn>[]
    {
        new List<Pawn>(),
        new List<Pawn>(),
        new List<Pawn>(),
        new List<Pawn>()
    };

    public void Init()
    {
        //Create Tiles
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                tilesGrid[i][j] = new Field();
                tilesGrid[i][j].row = i;
                tilesGrid[i][j].column = j;

                #region tilesInit
                // First Row
                if (i == 0 && j == 2)
                {
                    tilesGrid[i][j].chipNumber = 10;
                    tilesGrid[i][j].resourceName = "Ore";
                }
                if (i == 0 && j == 4)
                {
                    tilesGrid[i][j].chipNumber = 2;
                    tilesGrid[i][j].resourceName = "Wool";
                }
                if (i == 0 && j == 6)
                {
                    tilesGrid[i][j].chipNumber = 9;
                    tilesGrid[i][j].resourceName = "Wood";
                }
                // Second Row
                if (i == 1 && j == 1)
                {
                    tilesGrid[i][j].chipNumber = 12;
                    tilesGrid[i][j].resourceName = "Wheat";
                }
                if (i == 1 && j == 3)
                {
                    tilesGrid[i][j].chipNumber = 6;
                    tilesGrid[i][j].resourceName = "Brick";
                }
                if (i == 1 && j == 5)
                {
                    tilesGrid[i][j].chipNumber = 4;
                    tilesGrid[i][j].resourceName = "Wool";
                }
                if (i == 1 && j == 7)
                {
                    tilesGrid[i][j].chipNumber = 10;
                    tilesGrid[i][j].resourceName = "Brick";
                }
                // Third Row
                if (i == 2 && j == 0)
                {
                    tilesGrid[i][j].chipNumber = 9;
                    tilesGrid[i][j].resourceName = "Wheat";
                }
                if (i == 2 && j == 2)
                {
                    tilesGrid[i][j].chipNumber = 11;
                    tilesGrid[i][j].resourceName = "Wood";
                }
                if (i == 2 && j == 4)
                {
                    tilesGrid[i][j].chipNumber = 7;
                    tilesGrid[i][j].resourceName = "Brick";
                }
                if (i == 2 && j == 6)
                {
                    tilesGrid[i][j].chipNumber = 3;
                    tilesGrid[i][j].resourceName = "Wood";
                }
                if (i == 2 && j == 8)
                {
                    tilesGrid[i][j].chipNumber = 8;
                    tilesGrid[i][j].resourceName = "Ore";
                }
                // Fourth Row
                if (i == 3 && j == 1)
                {
                    tilesGrid[i][j].chipNumber = 8;
                    tilesGrid[i][j].resourceName = "Wood";
                }
                if (i == 3 && j == 3)
                {
                    tilesGrid[i][j].chipNumber = 3;
                    tilesGrid[i][j].resourceName = "Ore";
                }
                if (i == 3 && j == 5)
                {
                    tilesGrid[i][j].chipNumber = 4;
                    tilesGrid[i][j].resourceName = "Wheat";
                }
                if (i == 3 && j == 7)
                {
                    tilesGrid[i][j].chipNumber = 5;
                    tilesGrid[i][j].resourceName = "Wool";
                }
                // Fifth Row
                if (i == 4 && j == 2)
                {
                    tilesGrid[i][j].chipNumber = 5;
                    tilesGrid[i][j].resourceName = "Brick";
                }
                if (i == 4 && j == 4)
                {
                    tilesGrid[i][j].chipNumber = 6;
                    tilesGrid[i][j].resourceName = "Wheat";
                }
                if (i == 4 && j == 6)
                {
                    tilesGrid[i][j].chipNumber = 11;
                    tilesGrid[i][j].resourceName = "Wool";
                }
                #endregion
                
            }
        }
    }

    
    public void SpreadResources(int rolledNumber)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (rolledNumber == tiles[i].chipNumber)
            {
                foreach (var element in tiles[i].pawns)
                {
                    if (element.type == "village")
                    {
                        GamePlay.Main.GetCurrentPlayer().inventory.AddItem(tiles[i].resourceName, 1);
                    }
                    if (element.type == "Town")
                    {
                        GamePlay.Main.GetCurrentPlayer().inventory.AddItem(tiles[i].resourceName, 2);
                    }
                    
                }
            }
        }
    }

    public Place[] GetAllPositions(Pawn buildedPawn, bool godmode = false)
    {

        //Spielfiguren selbst speichern und dann die möglichen bauplätze aus den eigenen spielfiguren ermitteln
        // Die gebaute Spielfigur wird dann auf das zugehörige Tile gelegt

        PlayerColor pawnColor = PlayerColor.NONE;
        switch (buildedPawn.color)
        {
            case "Blue":
                pawnColor = PlayerColor.BLUE;
                break;
            case "Red":
                pawnColor = PlayerColor.RED;
                break;
            case "Orange":
                pawnColor = PlayerColor.ORANGE;
                break;
            case "White":
                pawnColor = PlayerColor.WHITE;
                break;
            default:
                Debug.LogError("Die zu bauende Spielfigur hat die undefinierte Farbe: " + buildedPawn.color);
                return null;
        }

        List<Place> possiblePositions = new List<Place>();

        if (buildedPawn.type == "Town")
        {
            for (int i = 0; i < pawns[(int) pawnColor].Count; i++)
            {
                Pawn currentPawn = pawns[(int)pawnColor][i];

                if (currentPawn.type == "Village")
                {
                    Place newPlace = GameObject.Instantiate(Resources.Load<Place>("Prefabs/PossiblePlaceTown"), GameObject.Find("Places").transform);
                    newPlace.usedFields = currentPawn.GetFields();
                    newPlace.posAtField = currentPawn.GetPosAtField();
                    possiblePositions.Add(newPlace);
                }
            }
            return possiblePositions.ToArray();
        }
        else if (buildedPawn.type == "Street")
        {
            if (godmode)
            {
                //for (int i = 0; i < pawns[(int)pawnColor].Count; i++)
                {
                    Pawn currentPawn = pawns[(int)pawnColor][pawns[(int)pawnColor].Count - 1];

                    if (currentPawn.type == "Village")
                    {
                        Field[] currentFields = currentPawn.GetFields();
                        int[] currentPosition = currentPawn.GetPosAtField();
                        for (int j = 0; j < currentFields.Length; j++)
                        {
                            Place newPlace = GameObject.Instantiate(Resources.Load<Place>("Prefabs/PossiblePlaceStreet"), GameObject.Find("Places").transform);
                            List<Field> newPlaceFields = new List<Field>();
                            List<int> newPlacePos = new List<int>();

                            //Abfrage, ob an der benachbarten position eine Straße ist
                            //Wenn nicht, dann wird es dem possible Places hinzugefügt
                            int newPos = currentPosition[j] + 1;
                            if (newPos >= 12)
                            {
                                newPos = newPos - 12;
                            }

                            if (currentFields[j].pawns[newPos] == null)
                            {
                                //erstes Feld
                                newPlaceFields.Add(currentFields[j]);
                                newPlacePos.Add(newPos);

                                //zweites Feld
                                if (currentFields[j].GetConnectedFields(newPos).Length > 0)
                                {
                                    newPlaceFields.Add(currentFields[j].GetConnectedFields(newPos)[0]);
                                }

                                //position des zweiten feldes ermitteln
                                int posAtSecondField = newPos + 6;
                                if (posAtSecondField >= 12)
                                {
                                    posAtSecondField = posAtSecondField - 12;
                                }
                                newPlacePos.Add(posAtSecondField);

                                //werte zuweisen
                                newPlace.usedFields = newPlaceFields.ToArray();
                                newPlace.posAtField = newPlacePos.ToArray();
                                possiblePositions.Add(newPlace);
                            }

                            //zurücksetzen der werte
                            newPlace = GameObject.Instantiate(Resources.Load<Place>("Prefabs/PossiblePlaceStreet"), GameObject.Find("Places").transform);
                            newPlaceFields = new List<Field>();
                            newPlacePos = new List<int>();

                            //erstellen einer weiteren neuen platzes
                            newPos = currentPosition[j] - 1;
                            if (newPos < 0)
                            {
                                newPos = newPos + 12;
                            }

                            if (currentFields[j].pawns[newPos] == null)
                            {
                                //erstes Feld
                                newPlaceFields.Add(currentFields[j]);
                                newPlacePos.Add(newPos);

                                //zweites Feld
                                if (currentFields[j].GetConnectedFields(newPos).Length > 0)
                                {
                                    newPlaceFields.Add(currentFields[j].GetConnectedFields(newPos)[0]);
                                }

                                //position des zweiten feldes ermitteln
                                int posAtSecondField = newPos + 6;
                                if (posAtSecondField >= 12)
                                {
                                    posAtSecondField = posAtSecondField - 12;
                                }
                                newPlacePos.Add(posAtSecondField);

                                //werte zuweisen
                                newPlace.usedFields = newPlaceFields.ToArray();
                                newPlace.posAtField = newPlacePos.ToArray();
                                possiblePositions.Add(newPlace);
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < pawns[(int)pawnColor].Count; i++)
                {
                    Pawn currentPawn = pawns[(int)pawnColor][i];

                    if (currentPawn.type == "Street")
                    {
                        Field[] currentFields = currentPawn.GetFields();
                        int[] currentPosition = currentPawn.GetPosAtField();
                        for (int j = 0; j < currentFields.Length; j++)
                        {
                            Place newPlace = GameObject.Instantiate(Resources.Load<Place>("Prefabs/PossiblePlaceStreet"), GameObject.Find("Places").transform);
                            List<Field> newPlaceFields = new List<Field>();
                            List<int> newPlacePos = new List<int>();

                            //Abfrage, ob an der benachbarten position eine Straße ist
                            //Wenn nicht, dann wird es dem possible Places hinzugefügt
                            int newPos = currentPosition[j] + 2;
                            if (newPos >= 12)
                            {
                                newPos = newPos - 12;
                            }

                            if (currentFields[j].pawns[newPos] == null)
                            {
                                //erstes Feld
                                newPlaceFields.Add(currentFields[j]);
                                newPlacePos.Add(newPos);

                                //zweites Feld
                                if (currentFields[j].GetConnectedFields(newPos).Length > 0)
                                {
                                    newPlaceFields.Add(currentFields[j].GetConnectedFields(newPos)[0]);
                                }

                                //position des zweiten feldes ermitteln
                                int posAtSecondField = newPos + 6;
                                if (posAtSecondField >= 12)
                                {
                                    posAtSecondField = posAtSecondField - 12;
                                }
                                newPlacePos.Add(posAtSecondField);

                                //werte zuweisen
                                newPlace.usedFields = newPlaceFields.ToArray();
                                newPlace.posAtField = newPlacePos.ToArray();
                                possiblePositions.Add(newPlace);
                            }

                            //zurücksetzen der werte
                            newPlace = GameObject.Instantiate(Resources.Load<Place>("Prefabs/PossiblePlaceStreet"), GameObject.Find("Places").transform);
                            newPlaceFields = new List<Field>();
                            newPlacePos = new List<int>();

                            //erstellen einer weiteren neuen platzes
                            newPos = currentPosition[j] - 2;
                            if (newPos < 0)
                            {
                                newPos = newPos + 12;
                            }

                            if (currentFields[j].pawns[newPos] == null)
                            {
                                //erstes Feld
                                newPlaceFields.Add(currentFields[j]);
                                newPlacePos.Add(newPos);

                                //zweites Feld
                                if (currentFields[j].GetConnectedFields(newPos).Length > 0)
                                {
                                    newPlaceFields.Add(currentFields[j].GetConnectedFields(newPos)[0]);
                                }

                                //position des zweiten feldes ermitteln
                                int posAtSecondField = newPos + 6;
                                if (posAtSecondField >= 12)
                                {
                                    posAtSecondField = posAtSecondField - 12;
                                }
                                newPlacePos.Add(posAtSecondField);

                                //werte zuweisen
                                newPlace.usedFields = newPlaceFields.ToArray();
                                newPlace.posAtField = newPlacePos.ToArray();
                                possiblePositions.Add(newPlace);
                            }
                        }
                    }
                }
            }
        }
        else if (buildedPawn.type == "Village")
        {
            if (!godmode)
            {
                for (int i = 0; i < pawns[(int)pawnColor].Count; i++)
                {
                    Pawn currentPawn = pawns[(int)pawnColor][i];
                    Field[] currentFields = currentPawn.GetFields();
                    int[] currentPos = currentPawn.GetPosAtField();

                    if (currentPawn.type == "Street")
                    {
                        /////////////////////////////////
                        //Check first side of street

                        //check if the aimed field is alredy taken
                        int newPos = currentPos[0] - 1;
                        if (newPos < 0)
                        {
                            newPos = newPos + 12;
                        }
                        if (currentFields[0].pawns[newPos] != null)
                        {
                            continue;
                        }

                        //check if first placeinreach is taken
                        newPos = currentPos[0] + 1;
                        if (newPos >= 12)
                        {
                            newPos = newPos - 12;
                        }
                        if (currentFields[0].pawns[newPos] == null)
                        {
                            //check if second placeinreach is taken
                            newPos = currentPos[0] - 3;
                            if (newPos < 0)
                            {
                                newPos = newPos + 12;
                            }
                            if (currentFields[0].pawns[newPos] == null)
                            {
                                //check if third placeinreach is taken
                                newPos = currentPos[1] + 3;
                                if (newPos >= 12)
                                {
                                    newPos = newPos - 12;
                                }
                                if (currentFields[1].pawns[newPos] == null)
                                {
                                    Place newPlace = GameObject.Instantiate(Resources.Load<Place>("Prefabs/PossiblePlaceVillage"), GameObject.Find("Places").transform);
                                    List<Field> newPlaceFields = new List<Field>();
                                    List<int> newPlacePos = new List<int>();

                                    //Get new Fields
                                    //first field and Position
                                    int newPos1 = currentPos[0] - 1;
                                    if (newPos1 < 0)
                                    {
                                        newPos1 = newPos1 + 12;
                                    }
                                    newPlacePos.Add(newPos1);
                                    newPlaceFields.Add(currentFields[0]);

                                    //second field and Position
                                    int newPos2 = currentPos[1] + 1;
                                    if (newPos2 >= 12)
                                    {
                                        newPos2 = newPos2 - 12;
                                    }
                                    newPlacePos.Add(newPos2);
                                    newPlaceFields.Add(currentFields[1]);

                                    //third field and Position
                                    Field[] thirdFields = currentFields[0].GetConnectedFields(newPos1);

                                    //only add third field if there is another field in board
                                    if (thirdFields.Length == 2)
                                    {
                                        newPlaceFields.Add((thirdFields[0].Equals(currentFields[1])) ? thirdFields[1] : thirdFields[0]);

                                        int newPos3 = 0;

                                        if (newPos1 == 0)
                                        {
                                            if (newPos2 == 4)
                                            {
                                                newPos3 = 8;
                                            }
                                            else if (newPos2 == 8)
                                            {
                                                newPos3 = 4;
                                            }
                                        }
                                        if (newPos1 == 4)
                                        {
                                            if (newPos2 == 0)
                                            {
                                                newPos3 = 8;
                                            }
                                            else if (newPos2 == 8)
                                            {
                                                newPos3 = 0;
                                            }
                                        }
                                        if (newPos1 == 8)
                                        {
                                            if (newPos2 == 4)
                                            {
                                                newPos3 = 0;
                                            }
                                            else if (newPos2 == 0)
                                            {
                                                newPos3 = 4;
                                            }
                                        }
                                        newPlacePos.Add(newPos3);
                                    }

                                    //finish Place
                                    newPlace.usedFields = newPlaceFields.ToArray();
                                    newPlace.posAtField = newPlacePos.ToArray();
                                    possiblePositions.Add(newPlace);
                                }
                            }
                        }

                        ////////////////////////////////
                        //check second side of street

                        //check if the aimed field is alredy taken
                        newPos = currentPos[0] + 1;
                        if (newPos >= 12)
                        {
                            newPos = newPos - 12;
                        }
                        if (currentFields[0].pawns[newPos] != null)
                        {
                            continue;
                        }

                        //check if first placeinreach is taken
                        newPos = currentPos[0] - 1;
                        if (newPos < 0)
                        {
                            newPos = newPos + 12;
                        }
                        if (currentFields[0].pawns[newPos] == null)
                        {
                            //check if second placeinreach is taken
                            newPos = currentPos[0] + 3;
                            if (newPos >= 12)
                            {
                                newPos = newPos - 12;
                            }
                            if (currentFields[0].pawns[newPos] == null)
                            {
                                //check if third placeinreach is taken
                                newPos = currentPos[1] - 3;
                                if (newPos < 0)
                                {
                                    newPos = newPos + 12;
                                }
                                if (currentFields[1].pawns[newPos] == null)
                                {
                                    Place newPlace = GameObject.Instantiate(Resources.Load<Place>("Prefabs/PossiblePlaceVillage"), GameObject.Find("Places").transform);
                                    List<Field> newPlaceFields = new List<Field>();
                                    List<int> newPlacePos = new List<int>();

                                    //Get new Fields
                                    //first field and Position
                                    int newPos1 = currentPos[0] + 1;
                                    if (newPos1 >= 12)
                                    {
                                        newPos1 = newPos1 - 12;
                                    }
                                    newPlacePos.Add(newPos1);
                                    newPlaceFields.Add(currentFields[0]);

                                    //second field and Position
                                    int newPos2 = currentPos[1] - 1;
                                    if (newPos2 < 0)
                                    {
                                        newPos2 = newPos2 + 12;
                                    }
                                    newPlacePos.Add(newPos2);
                                    newPlaceFields.Add(currentFields[1]);

                                    //third field and Position
                                    Field[] thirdFields = currentFields[0].GetConnectedFields(newPos1);

                                    //only add third field if there is another field in board
                                    if (thirdFields.Length == 2)
                                    {
                                        newPlaceFields.Add((thirdFields[0].Equals(currentFields[1])) ? thirdFields[1] : thirdFields[0]);

                                        int newPos3 = 0;

                                        if (newPos1 == 0)
                                        {
                                            if (newPos2 == 4)
                                            {
                                                newPos3 = 8;
                                            }
                                            else if (newPos2 == 8)
                                            {
                                                newPos3 = 4;
                                            }
                                        }
                                        if (newPos1 == 4)
                                        {
                                            if (newPos2 == 0)
                                            {
                                                newPos3 = 8;
                                            }
                                            else if (newPos2 == 8)
                                            {
                                                newPos3 = 0;
                                            }
                                        }
                                        if (newPos1 == 8)
                                        {
                                            if (newPos2 == 4)
                                            {
                                                newPos3 = 0;
                                            }
                                            else if (newPos2 == 0)
                                            {
                                                newPos3 = 4;
                                            }
                                        }
                                        newPlacePos.Add(newPos3);
                                    }

                                    //finish Place
                                    newPlace.usedFields = newPlaceFields.ToArray();
                                    newPlace.posAtField = newPlacePos.ToArray();
                                    possiblePositions.Add(newPlace);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < tilesGrid.Length; i++)
                {
                    for (int j = 0; j < tilesGrid[i].Length; j++)
                    {
                        if (tilesGrid[i][j] == null || tilesGrid[i][j].chipNumber == 0)
                        {
                            continue;
                        }
                        for (int k = 0; k < tilesGrid[i][j].pawns.Length; k++)
                        {
                            Pawn currentPawn = tilesGrid[i][j].pawns[k];
                            if (currentPawn == null && k % 2 == 0)
                            {
                                Field[] currentFields;
                                {
                                    List<Field> tmp = new List<Field>();
                                    tmp.Add(tilesGrid[i][j]);
                                    Field[] tmp2 = tilesGrid[i][j].GetConnectedFields(k);
                                    for (int l = 0; l < tmp2.Length; l++)
                                    {
                                        tmp.Add(tmp2[l]);
                                    }
                                    currentFields = tmp.ToArray();
                                }
                                int[] currentPos = new int[currentFields.Length];

                                currentPos[0] = k;
                                if (currentPos.Length > 1)
                                {
                                    currentPos[1] = k + 4;
                                    if (currentPos[1] >= 12)
                                    {
                                        currentPos[1] -= 12;
                                    }
                                    if (currentPos.Length > 2)
                                    {
                                        currentPos[2] = k + 8;
                                        if (currentPos[2] >= 12)
                                        {
                                            currentPos[2] -= 12;
                                        }
                                    }
                                }
                                bool savespot = true;
                                for (int l = 0; l < currentFields.Length; l++)
                                {
                                    int tmp = currentPos[l] + 2;
                                    if (tmp >= 12)
                                    {
                                        tmp -= 12;
                                    }
                                    if (currentFields[l].pawns[tmp] != null)
                                    {
                                        savespot = false;
                                        break;
                                    }
                                    tmp = currentPos[l] - 2;
                                    if (tmp < 0)
                                    {
                                        tmp += 12;
                                    }
                                    if (currentFields[l].pawns[tmp] != null)
                                    {
                                        savespot = false;
                                        break;
                                    }
                                }
                                if (savespot)
                                {
                                    //Place newPlace = new Place();
                                    Place newPlace = GameObject.Instantiate(Resources.Load<Place>("Prefabs/PossiblePlaceVillage"), GameObject.Find("Places").transform);
                                    newPlace.buildedPawn = buildedPawn;
                                    newPlace.usedFields = currentFields;
                                    newPlace.posAtField = currentPos;
                                    possiblePositions.Add(newPlace);
                                }
                            }
                        }
                    }
                }
            }
        }
        else
        {
            Debug.LogError("Du kannst keine " + buildedPawn.type + " bauen.");
        }
        return possiblePositions.ToArray();
    }

    public void deleteAllPlaces()
    {
        //Places löschen
        for (int i = 0; i < GameObject.Find("Places").transform.childCount; i++)
        {
            GameObject.Destroy(GameObject.Find("Places").transform.GetChild(i).gameObject);
        }
    }
}
