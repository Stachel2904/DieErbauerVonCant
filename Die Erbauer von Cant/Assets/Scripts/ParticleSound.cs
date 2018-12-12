using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSound : MonoBehaviour {

    private ParticleSystem parSystem;
    private int numberOfParticles = 0;

    private void Start() {
        parSystem = this.gameObject.GetComponent<ParticleSystem>();
    }

    private void Update () {
        int count = parSystem.particleCount;
        if(count < numberOfParticles) {
            //play Sound if particle has died
        }else if(count > numberOfParticles) {
            //play Sound if particle is born
        }
        numberOfParticles = count;
	}
}
