using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSoundManager : MonoBehaviour {

    public AudioClip buttonClick;
    public AudioClip popUp;


    private AudioSource audioSource;


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }



    public void PlaySound(string clip, float delay = 0.0f)
    {
        switch (clip)
        {
            case "buttonClick":
                StartCoroutine(playSoundWithDelay(buttonClick, delay));
                break;
            case "popUp":
                StartCoroutine(playSoundWithDelay(popUp, delay));
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

    //public void MuteAudio(bool soundMuted)
    //{
    //    if (soundMuted)
    //    {
    //        audioSource.mute = true;
    //    }
    //    else
    //    {
    //        audioSource.mute = false;
    //    }
    //}
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
