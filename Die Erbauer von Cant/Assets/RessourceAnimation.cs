using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourceAnimation : MonoBehaviour {

    public Vector3 direction = Vector3.zero;
    public string animatedRessource;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, direction, 10);

        if (transform.position == direction)
        {
            Transform createdPlusOne = Instantiate(Resources.Load<Transform>("Prefabs/" + animatedRessource + "PlusOne"), GameObject.Find("Window").transform);
            createdPlusOne.gameObject.GetComponent<animatePlusOne>().direction = direction;
            GameObject.Find("SoundManager").GetComponent<HostSoundManager>().PlaySound("ressourceGain");
            Destroy(gameObject);
        }
        
	}
}
