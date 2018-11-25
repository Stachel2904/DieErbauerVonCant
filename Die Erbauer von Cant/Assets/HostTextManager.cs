using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HostTextManager : MonoBehaviour {

    [SerializeField]
    GameObject PlayerNumberText;

    
    Text tPlayerNumber;

	// Use this for initialization
	void Start () {
        tPlayerNumber = PlayerNumberText.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        tPlayerNumber.text = (GamePlay.Main.maxPlayer +1).ToString();
    }
}
