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
                        player1.transform.Find("Playername").GetComponent<Text>().text = GamePlay.Main.players[i].name;
                        GamePlay.Main.players[i].avatar = "Player1";
                        player1.GetComponent<Image>().color = ctemp;
                        break;
                    case 2:
                        player2.transform.Find("Playername").GetComponent<Text>().text = GamePlay.Main.players[i].name;
                        GamePlay.Main.players[i].avatar = "Player2";
                        player2.GetComponent<Image>().color = ctemp;
                        break;
                    case 3:
                        player3.transform.Find("Playername").GetComponent<Text>().text = GamePlay.Main.players[i].name;
                        GamePlay.Main.players[i].avatar = "Player3";
                        player3.GetComponent<Image>().color = ctemp;
                        break;
                    case 4:
                        player4.transform.Find("Playername").GetComponent<Text>().text = GamePlay.Main.players[i].name;
                        GamePlay.Main.players[i].avatar = "Player4";
                        player4.GetComponent<Image>().color = ctemp;
                        break;
                }
                break;
            }
        }

        //RectTransform output = Instantiate(GameObject.Find("Window").transform.GetChild(0).gameObject, GameObject.Find("Window").transform).GetComponent<RectTransform>();
        //output.GetChild(0).GetComponent<Text>().text = _clientID_.ToString();
        //output.Translate(Vector2.down * 30);
    }
    public void RemoveConnectedPlayerAvatar(int _clientID_) {
        for (int i = 0; i < 4; i++) {
            if (GamePlay.Main.players[i].clientID == _clientID_) {
                switch (GamePlay.Main.players[i].avatar) {
                    case "Player1":
                        player1.transform.Find("Playername").GetComponent<Text>().text = "Player";
                        player1.GetComponent<Image>().color = Color.gray;
                        GamePlay.Main.players[i].avatar = "DEFAULT";
                        break;
                    case "Player2":
                        player2.transform.Find("Playername").GetComponent<Text>().text = "Player";
                        player2.GetComponent<Image>().color = Color.gray;
                        GamePlay.Main.players[i].avatar = "DEFAULT";
                        break;
                    case "Player3":
                        player3.transform.Find("Playername").GetComponent<Text>().text = "Player";
                        player3.GetComponent<Image>().color = Color.gray;
                        GamePlay.Main.players[i].avatar = "DEFAULT";
                        break;
                    case "Player4":
                        player4.transform.Find("Playername").GetComponent<Text>().text = "Player";
                        player4.GetComponent<Image>().color = Color.gray;
                        GamePlay.Main.players[i].avatar = "DEFAULT";
                        break;
                }
                break;
            }
        }
        //GameObject tmp = null;
        //for (int i = 0; i < 4; i++) {
        //    if(GamePlay.Main.players[i].clientID == _clientID_) {
        //        for (int j = 1; j < GameObject.Find("Window").transform.childCount; j++) {
        //            if (GameObject.Find("Window").transform.GetChild(j).GetChild(0).gameObject.GetComponent<Text>().text == GamePlay.Main.players[i].name) {
        //                tmp = GameObject.Find("Window").transform.GetChild(j).gameObject;
        //            }
        //        }
        //        break;
        //    }
        //}
        //Destroy(tmp);
    }
}
