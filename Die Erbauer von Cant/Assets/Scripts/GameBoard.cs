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
                mainBoard.Init();
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

    private void Init()
    {
        //Create Tiles
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                tilesGrid[i][j] = new Field();
                tilesGrid[i][j].row = i;
                tilesGrid[i][j].column = j;
            }
        }

        //Set startPawns
        tilesGrid[2][4].pawns[3] = new Pawn("Street", "Orange");
        tilesGrid[2][6].pawns[9] = tilesGrid[2][4].pawns[3];
        pawns[(int)PlayerColor.ORANGE].Add(tilesGrid[2][4].pawns[3]);

        tilesGrid[2][4].pawns[5] = new Pawn("Street", "Orange");
        MainBoard.tilesGrid[3][5].pawns[11] = tilesGrid[2][4].pawns[5];
        pawns[(int)PlayerColor.ORANGE].Add(tilesGrid[2][4].pawns[5]);
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

    public Place[] GetAllPositions(Pawn buildedPawn)
    {

        //Spielfiguren selbst speichern und dann die möglichen bauplätze aus den eigenen spielfiguren ermitteln
        // Die gebaute Spielfigur wird dann auf das zugehörige Tile gelegt

        PlayerColor pawnColor;
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
                Debug.Log("Die zu bauende Spielfigur hat die undefinierte Farbe: " + buildedPawn.color);
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
                    Place newPlace = new Place();
                    newPlace.usedFields = currentPawn.GetFields();
                    newPlace.posAtField = currentPawn.GetPosAtField();
                }
            }
            return possiblePositions.ToArray();
        }

        if (buildedPawn.type == "Street")
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
                        Place newPlace = new Place();
                        List<Field> newPlaceFields = new List<Field>();
                        List<int> newPlacePos = new List<int>();

                        //Abfrage, ob an der benachbarten position eine Straße ist
                        //Wenn nicht, dann wird es dem possible Places hinzugefügt
                        int newPos = currentPosition[i] + 2;
                        if (newPos >= 12)
                        {
                            newPos = newPos - 12;
                        }

                        if (currentFields[i].pawns[newPos] == null)
                        {
                            //erstes Feld
                            newPlaceFields.Add(currentFields[i]);
                            newPlacePos.Add(newPos);

                            //zweites Feld
                            newPlaceFields.Add(currentFields[i].GetConnectedFields(newPos)[0]);

                            //position des zweiten feldes ermitteln
                            int posAtSecondField = newPos + 6;
                            if(posAtSecondField >= 12)
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
                        newPlace = new Place();
                        newPlaceFields = new List<Field>();
                        newPlacePos = new List<int>();

                        //erstellen einer weiteren neuen platzes
                        newPos = currentPosition[i] - 2;
                        if (newPos < 0)
                        {
                            newPos = newPos + 12;
                        }

                        if (currentFields[i].pawns[newPos] == null)
                        {
                            //erstes Feld
                            newPlaceFields.Add(currentFields[i]);
                            newPlacePos.Add(newPos);

                            //zweites Feld
                            //Feld ermitteln
                            Field secondField = null;

                            //check if the place is on the water and may not have a second field
                            bool fieldOnTop = (currentFields[i].row == 0 && (newPos == 1 || newPos == 11));
                            bool fieldOnRight = ((currentFields[i].column == 7 || currentFields[i].column == 8) && newPos == 3);
                            bool fieldOnBottom = (currentFields[i].row == 4 && (newPos == 5 || newPos == 7));
                            bool fieldOnLeft = ((currentFields[i].column == 0 || currentFields[i].column == 1) && newPos == 9);

                            //erlangen des zweiten feldes
                            if (!(fieldOnTop && fieldOnRight && fieldOnBottom && fieldOnLeft))
                            {
                                if (newPos == 1)
                                {
                                    secondField = tilesGrid[currentFields[i].row - 1][currentFields[i].column + 1];
                                }
                                else if (newPos == 3)
                                {
                                    secondField = tilesGrid[currentFields[i].row][currentFields[i].column + 2];
                                }
                                else if (newPos == 5)
                                {
                                    secondField = tilesGrid[currentFields[i].row + 1][currentFields[i].column + 1];
                                }
                                else if (newPos == 7)
                                {
                                    secondField = tilesGrid[currentFields[i].row + 1][currentFields[i].column - 1];
                                }
                                else if (newPos == 9)
                                {
                                    secondField = tilesGrid[currentFields[i].row][currentFields[i].column - 2];
                                }
                                else if (newPos == 11)
                                {
                                    secondField = tilesGrid[currentFields[i].row - 1][currentFields[i].column - 1];
                                }
                            }
                            newPlaceFields.Add(secondField);

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

        if (buildedPawn.type == "Village")
        {
            Debug.Log("Looking for Places...");
            for (int i = 0; i < pawns[(int)pawnColor].Count; i++)
            {
                Pawn currentPawn = pawns[(int)pawnColor][i];
                Field[] currentFields = currentPawn.GetFields();
                int[] currentPos = currentPawn.GetPosAtField();

                if (currentPawn.type == "Street")
                {
                    Debug.Log("Found Street!");
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
                                Debug.Log("No other Village");
                                Place newPlace = new Place();
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
                                Field[] thirdFields = currentFields[0].GetConnectedFields(currentPos[0]);

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
                                Debug.Log("No other Village");
                                Place newPlace = new Place();
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
                                Field[] thirdFields = currentFields[0].GetConnectedFields(currentPos[0]);

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

        return possiblePositions.ToArray();
    }
}
