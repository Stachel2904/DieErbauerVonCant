using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    public float speed;
    public float zoom;
    private Transform camera;

    private void Start()
    {
        camera = this.transform.GetChild(0).transform;
    }

    void Update ()
    {
        if(Input.touchCount == 1)
        {
            transform.Rotate(0, Input.GetTouch(0).deltaPosition.x * 0.25f, 0);

            if(camera.localRotation.eulerAngles.x - Input.GetTouch(0).deltaPosition.y * speed * 12 * 0.5f < 90 && camera.localRotation.eulerAngles.x - Input.GetTouch(0).deltaPosition.y * speed * 12 * 0.5f > 0)
            {
                camera.Translate(0, -Input.GetTouch(0).deltaPosition.y * (90 - camera.localRotation.x) * speed * 0.35f * 1 / zoom * 0.5f, -Input.GetTouch(0).deltaPosition.y * camera.localRotation.x * speed * 0.35f * 1 / zoom * 0.5f);
                camera.Rotate(-Input.GetTouch(0).deltaPosition.y * speed * 12 * 0.5f, 0, 0);
            }
            else if (camera.localRotation.eulerAngles.x - Input.GetTouch(0).deltaPosition.y * speed * 12 * 0.5f >= 90)
            {
                camera.localPosition = new Vector3(0, 150 * 1 / zoom, 0);
                camera.localRotation = Quaternion.Euler(90, 0, 0);
            }
            else if (camera.localRotation.eulerAngles.x - Input.GetTouch(0).deltaPosition.y * speed * 12 * 0.5f <= 0)
            {
                camera.localPosition = new Vector3(0, 0, -150 * 1 / zoom);
                camera.localRotation = Quaternion.Euler(0, 0, 0);
            }

        }
        else if (Input.touchCount == 2)
        {
            float distanceOfFingersLastFrame = Vector2.Distance(Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition, Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition);
            float distanceOfFingersThisFrame = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            if(zoom * 1 + ((float)distanceOfFingersThisFrame - (float)distanceOfFingersLastFrame) / 1000f < 2.0f && zoom * 1 + ((float)distanceOfFingersThisFrame - (float)distanceOfFingersLastFrame) / 1000f > 0.5f)
            {
                zoom *= 1 + ((float)distanceOfFingersThisFrame - (float)distanceOfFingersLastFrame) / 1000f;
            }
        }


        if (Vector3.Distance(camera.position, this.gameObject.transform.position) != 150 * 1 / zoom)
        {
            camera.Translate(0, 0, Vector3.Distance(camera.position, this.gameObject.transform.position) - 150 * 1 / zoom);
        }
    }
}
