using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollHeadMovement : MonoBehaviour
{
    public Vector3 rotationAngle = new Vector3(0f, 1f, 0f);
    //public Rigidbody rb;

    private bool isScanning = true;

    public AudioSource Scan;
    public AudioSource Gunshot;


    void Start()
    {
        StartCoroutine(RotateObject(180, rotationAngle, 0.25f));
    }

    IEnumerator RotateObject(float angle, Vector3 axis, float inTime)
    {
        // calculate rotation speed
        float rotationSpeed = angle / inTime;

        while (true)
        {
            // save starting rotation position
            Quaternion startRotation = transform.rotation;

            float deltaAngle = 0;

            // rotate until reaching angle
            while (deltaAngle < angle)
            {
                deltaAngle += rotationSpeed * Time.deltaTime;
                deltaAngle = Mathf.Min(deltaAngle, angle);

                transform.rotation = startRotation * Quaternion.AngleAxis(deltaAngle, axis);

                yield return null;
            }

            // delay here
            if (isScanning)
            {
                playScanSound();
            }

            if (isScanning && GameObject.FindGameObjectWithTag("Player").transform.hasChanged == true)
            {
                playGunshotSound();
            }

            if (isScanning)
            {
                // greenLightSound.Play();
                yield return new WaitForSeconds((float)Random.Range(4, 6));
            } else
            {
                // redLightSound.Play();
                yield return new WaitForSeconds((float)Random.Range(1, 4));
            }

            isScanning = !isScanning;
        }
    }

    public void playScanSound()
    {
        Scan.Play();
    }

    public void playGunshotSound()
    {
        Gunshot.Play();
    }
}
