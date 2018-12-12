using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSound : MonoBehaviour {

    private ParticleSystem parSystem;
    private int numberOfParticles = 0;

    private void Start()
    {
        parSystem = this.gameObject.GetComponent<ParticleSystem>();
    }

    private void Update ()
    {
        int count = parSystem.particleCount;
        if(count < numberOfParticles)
        {
            GameObject.Find("SoundManager").GetComponent<HostSoundManager>().PlaySound("fireworkExplosion");
            GameObject.Find("SoundManager").GetComponent<HostSoundManager>().PlaySound("fireworkCluster", 0.8f);
        }
        else if(count > numberOfParticles)
        {
            GameObject.Find("SoundManager").GetComponent<HostSoundManager>().PlaySound("fireworkScream");
        }
        numberOfParticles = count;
	}
}
