using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animatePlusOne : MonoBehaviour {

    public Vector3 direction = Vector3.zero;

    int counter = 0;

    // Use this for initialization
    void Start () {
        transform.position = direction;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, 5, 0);

        if (counter == 20)
        {
            Destroy(this.gameObject);
        }
        counter++;
	}
}
