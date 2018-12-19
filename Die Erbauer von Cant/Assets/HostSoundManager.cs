using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostSoundManager : MonoBehaviour
{
    public AudioClip buttonClick;
    public AudioClip buildingStreet;
    public AudioClip buildingVillage;
    public AudioClip buildingCity;
    public AudioClip ressourceGain;
    public AudioClip diceRoll;
    public AudioClip popUp;
    public AudioClip victorySound;
    public AudioClip fireworkScream;
    public AudioClip fireworkExplosion;
    public AudioClip fireworkCluster;


    public AudioSource audioSource;
    public AudioSource ambienteAudio;
    public AudioSource ambienteAudioWater;
    

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    
    public void PlaySound(string clip, float delay = 0f)
    {
        switch (clip)
        {
            case "buttonClick":
                StartCoroutine(playSoundWithDelay(buttonClick, delay));
                break;
            case "ressourceGain":
                StartCoroutine(playSoundWithDelay(ressourceGain, delay));
                break;
            case "buildingStreet":
                StartCoroutine(playSoundWithDelay(buildingStreet, delay));
                break;
            case "buildingVillage":
                StartCoroutine(playSoundWithDelay(buildingVillage, delay));
                break;
            case "buildingCity":
                StartCoroutine(playSoundWithDelay(buildingCity, delay));
                break;
            case "diceRoll":
                StartCoroutine(playSoundWithDelay(diceRoll, delay));
                break;
            case "popUp":
                StartCoroutine(playSoundWithDelay(popUp, delay));
                break;
            case "victory":
                StartCoroutine(playSoundWithDelay(victorySound, delay));
                break;
            case "fireworkScream":
                StartCoroutine(playSoundWithDelay(fireworkScream, delay, 0.5f));
                break;
            case "fireworkExplosion":
                StartCoroutine(playSoundWithDelay(fireworkExplosion, delay, 0.5f));
                break;
            case "fireworkCluster":
                StartCoroutine(playSoundWithDelay(fireworkCluster, delay, 0.5f));
                break;
            default:
                break;
        }
        
    }

    IEnumerator playSoundWithDelay(AudioClip clip, float delay, float volume = 1)
    {
        yield return new WaitForSeconds(delay);
        audioSource.PlayOneShot(clip);
        audioSource.volume = volume;
    }

    public void MuteAudio(bool soundMuted)
    {
        if (soundMuted)
        {
            audioSource.mute = true;
            ambienteAudio.mute = true;
            ambienteAudioWater.mute = true;
        }
        else
        {
            audioSource.mute = false;
            ambienteAudio.mute = false;
            ambienteAudioWater.mute = false;
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}
}
