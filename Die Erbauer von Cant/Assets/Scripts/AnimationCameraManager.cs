using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCameraManager : MonoBehaviour {

    [SerializeField]
    int CameraRotationSpeed = 0;

    bool inCutscene = false;
    Transform PawnInCutscene = null;
    Transform Camera;
    CanvasGroup CutsceneVideo;

    void Start()
    {
        Camera = this.transform.GetChild(0).GetChild(0);
        CutsceneVideo = GameObject.Find("Cutscene").GetComponent<CanvasGroup>();
    }

    public void Restart(Transform buildedPawn)
    {
        PawnInCutscene = buildedPawn;
        this.gameObject.transform.position = PawnInCutscene.position;

        inCutscene = true;

        StartCoroutine(Animation());
        StartCoroutine(Fade());
    }

    IEnumerator Fade(int direction = 1)
    {
        for (int i = 0; i < 40; i++)
        {
            CutsceneVideo.alpha += i / 20.0f * direction;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator Animation()
    {
        //cache Variables
        Vector3 cachedPosition = new Vector3(PawnInCutscene.position.x, PawnInCutscene.position.y, PawnInCutscene.position.z);
        Vector3 cachedScale = new Vector3(PawnInCutscene.localScale.x, PawnInCutscene.localScale.y, PawnInCutscene.localScale.z);

        //Reset variables
        PawnInCutscene.Translate(0, 5, 0);
        PawnInCutscene.localScale = Vector2.zero;
        Camera.localRotation = Quaternion.Euler(-5, 0, 0);

        //Scale Up
        for (int i = 0; i < 20; i++)
        {
            PawnInCutscene.localScale = i / 20.0f * cachedScale;
            yield return new WaitForSeconds(0.02f);
        }

        inCutscene = false;
        //Drop down
        for (int i = 0; i < 10; i++)
        {
            PawnInCutscene.Translate(0, -0.5f, 0);
            yield return new WaitForSeconds(0.02f);
            Camera.localRotation = Quaternion.Euler(i / 10.0f * 45 - 5, 0, 0);
        }

        //Dust
        this.gameObject.transform.Find("Dust").GetComponent<ParticleSystem>().Play();

        //Sound of Impact
        if (PawnInCutscene.name.Contains("Town"))
        {
            GameObject.Find("SoundManager").GetComponent<HostSoundManager>().PlaySound("buildingCity");
        }
        else if (PawnInCutscene.name.Contains("Village"))
        {
            GameObject.Find("SoundManager").GetComponent<HostSoundManager>().PlaySound("buildingVillage");
        }
        else
        {
            GameObject.Find("SoundManager").GetComponent<HostSoundManager>().PlaySound("buildingStreet");
        }

        yield return new WaitForSeconds(2.0f);

        //return to Default camera
        StartCoroutine(Fade(-1));

        //Fireworks
        if (!PawnInCutscene.name.Contains("Street"))
        {
            this.gameObject.transform.Find("Fireworks").GetComponent<ParticleSystem>().Play();
        }

        //Sound of Fireworks

        yield return new WaitForSeconds(10.0f);

        //Reset
        this.gameObject.transform.Find("Dust").GetComponent<ParticleSystem>().Stop();
        this.gameObject.transform.Find("Fireworks").GetComponent<ParticleSystem>().Stop();
    }

	void Update ()
    {
        if (!inCutscene)
        {
            return;
        }

        this.gameObject.transform.GetChild(0).Rotate(0, CameraRotationSpeed, 0);
	}
}
