using UnityEngine;
using UnityEngine.UI;

public class NetworkServerGUI : MonoBehaviour {

    [SerializeField]
    Image player1;
    [SerializeField]
    Image player2;
    [SerializeField]
    Image player3;
    [SerializeField]
    Image player4;
    Color ctemp;

    private void Start() {
        player1.gameObject.SetActive(false);
        player2.gameObject.SetActive(false);
        player3.gameObject.SetActive(false);
        player4.gameObject.SetActive(false);
    }
    public void AddConnectedPlayerAvatar(int _clientID_) {
        for (int i = 0; i < GamePlay.Main.players.Length ; i++) {
            if (GamePlay.Main.players[i].clientID == _clientID_ && GamePlay.Main.players[i].clientID != -1) {
                switch (GamePlay.Main.players[i].color)
                {
                    case "Orange":
                        ctemp = new Color(249, 166, 2);
                        break;
                    case "White":
                        ctemp = Color.white;
                        break;
                    case "Red":
                        ctemp = Color.red;
                        break;
                    case "Blue":
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
        for (int i = 0; i < GamePlay.Main.players.Length; i++) {
            if (GamePlay.Main.players[i].clientID == _clientID_ && GamePlay.Main.players[i].clientID != -1) {
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
        for (int i = 0; i < GamePlay.Main.players.Length; i++) {
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
    public void UpdateHand(int _ClientID_)
    { //Update count of handcards
        for (int i = 0; i < GamePlay.Main.players.Length; i++)
        {
            Debug.Log(" Gameplaymainplayer.length ist " + GamePlay.Main.players.Length);
            if (GamePlay.Main.players[i].clientID == _ClientID_ && GamePlay.Main.players[i].clientID != -1)
            {
                switch (GamePlay.Main.players[i].avatar)
                {
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

    //
    //
    //
    private void AddAvatar(int _player_, Image _playerAvatar_) {
        if (_playerAvatar_.gameObject.activeSelf == false) {
            _playerAvatar_.gameObject.SetActive(true);
        }
        _playerAvatar_.transform.Find("Playername").GetComponent<Text>().text = GamePlay.Main.players[_player_].name;
        GamePlay.Main.players[_player_].avatar = "Player" + (_player_ + 1).ToString();
        //_playerAvatar_.GetComponent<Image>().color = ctemp;
    }
    private void RemoveAvatar(int _player_, Image _playerAvatar_) {
        _playerAvatar_.transform.Find("Playername").GetComponent<Text>().text = "Player";
        _playerAvatar_.GetComponent<Image>().color = Color.gray;
        GamePlay.Main.players[_player_].avatar = "DEFAULT";
        _playerAvatar_.gameObject.SetActive(false);
    }
}
