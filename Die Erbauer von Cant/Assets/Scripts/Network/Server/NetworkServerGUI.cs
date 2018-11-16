using UnityEngine;
using UnityEngine.UI;

public class NetworkServerGUI : MonoBehaviour {

    public Image player1;
    public Image player2;
    public Image player3;
    public Image player4;
    Color ctemp;

    private void Start() {
        //ToDo: Avatare aufploppen lassen (Hier also deaktivieren bei Start)
    }
    public void AddConnectedPlayerAvatar(int _clientID_) {
        for (int i = 0; i < 4; i++) {
            if (GamePlay.Main.players[i].clientID == _clientID_) {
                switch (GamePlay.Main.players[i].color) {
                    case "yellow":
                        ctemp = Color.yellow;
                        break;
                    case "white":
                        ctemp = Color.white;
                        break;
                    case "red":
                        ctemp = Color.red;
                        break;
                    case "blue":
                        ctemp = Color.blue;
                        break;
                }
                switch (_clientID_) {
                    case 1:
                        AddAvatar(i, player1);
                        break;
                    case 2:
                        AddAvatar(i, player2);
                        break;
                    case 3:
                        AddAvatar(i, player3);
                        break;
                    case 4:
                        AddAvatar(i, player4);
                        break;
                    default:
                        Debug.LogError("No Playeravatar found which can be added!");
                        break;
                }
                break;
            }
        }
    }
    public void RemoveConnectedPlayerAvatar(int _clientID_) {
        for (int i = 0; i < 4; i++) {
            if (GamePlay.Main.players[i].clientID == _clientID_) {
                switch (GamePlay.Main.players[i].avatar) {
                    case "Player1":
                        RemoveAvatar(i, player1);
                        break;
                    case "Player2":
                        RemoveAvatar(i, player2);
                        break;
                    case "Player3":
                        RemoveAvatar(i, player3);
                        break;
                    case "Player4":
                        RemoveAvatar(i, player4);
                        break;
                    default:
                        Debug.LogError("No Playeravatar found which can be removed!");
                        break;
                }
                break;
            }
        }
    }
    public void UpdateVictoryPoints(string _color_) { //Update Victorypoints
        for (int i = 0; i < 4; i++) {
            if (GamePlay.Main.players[i].color == _color_) {
                switch (GamePlay.Main.players[i].avatar) {
                    case "Player1":
                        player1.transform.Find("Victorypoints").GetComponent<Text>().text = GamePlay.Main.players[i].victoryPoints.ToString();
                        break;
                    case "Player2":
                        player2.transform.Find("Victorypoints").GetComponent<Text>().text = GamePlay.Main.players[i].victoryPoints.ToString();
                        break;
                    case "Player3":
                        player3.transform.Find("Victorypoints").GetComponent<Text>().text = GamePlay.Main.players[i].victoryPoints.ToString();
                        break;
                    case "Player4":
                        player4.transform.Find("Victorypoints").GetComponent<Text>().text = GamePlay.Main.players[i].victoryPoints.ToString();
                        break;
                    default:
                        Debug.LogError("No Playeravatar found to add victorypoints!");
                        break;
                }
            }
        }
    }
    public void UpdateHand(string _color_) { //Update count of handcards
        for (int i = 0; i < 4; i++) {
            if (GamePlay.Main.players[i].color == _color_) {
                switch (GamePlay.Main.players[i].avatar) {
                    case "Player1":
                        player1.transform.Find("Hand").GetComponent<Text>().text = GamePlay.Main.players[i].hand.ToString();
                        break;
                    case "Player2":
                        player2.transform.Find("Hand").GetComponent<Text>().text = GamePlay.Main.players[i].hand.ToString();
                        break;
                    case "Player3":
                        player3.transform.Find("Hand").GetComponent<Text>().text = GamePlay.Main.players[i].hand.ToString();
                        break;
                    case "Player4":
                        player4.transform.Find("Hand").GetComponent<Text>().text = GamePlay.Main.players[i].hand.ToString();
                        break;
                    default:
                        Debug.LogError("No Playeravatar found to add hand!");
                        break;
                }
            }
        }
    }
    private void AddAvatar(int _player_, Image _playerAvatar_) {
        _playerAvatar_.transform.Find("Playername").GetComponent<Text>().text = GamePlay.Main.players[_player_].name;
        GamePlay.Main.players[_player_].avatar = "Player1";
        _playerAvatar_.GetComponent<Image>().color = ctemp;
    }
    private void RemoveAvatar(int _player_, Image _playerAvatar_) {
        _playerAvatar_.transform.Find("Playername").GetComponent<Text>().text = "Player";
        _playerAvatar_.GetComponent<Image>().color = Color.gray;
        GamePlay.Main.players[_player_].avatar = "DEFAULT";
        GamePlay.Main.players[_player_].victoryPoints = 0;
        _playerAvatar_.transform.Find("Victorypoints").GetComponent<Text>().text = "0";
        GamePlay.Main.players[_player_].hand = 0;
        _playerAvatar_.transform.Find("Hand").GetComponent<Text>().text = "0";
    }
}
