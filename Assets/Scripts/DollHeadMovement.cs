using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DollHeadMovement : MonoBehaviour
{
    public GameObject playerCam;
    public GameObject youLoseText;
    public GameObject youWinText;

    public AudioSource scanSound;
    public AudioSource gunshotSound;

    public Collision finishLine;

    public Text countdownText;

    float currentTime = 0f;
    float startingTime = 2f;

    public Vector3 rotationAngle = new Vector3(0f, 1f, 0f);

    private bool isScanning = true;

    void Start()
    {
        currentTime = startingTime;
        youLoseText.SetActive(false);
        youWinText.SetActive(false);
        float scanningTime = (float)Random.Range(4, 6);
        float nonScanningTime = (float)Random.Range(1, 4);
        StartCoroutine(RotateObject(180, rotationAngle, 0.25f));
    }

    void Update()
    {
        currentTime -= 1f * Time.deltaTime;

        if (currentTime > 9)
        {
            countdownText.text = "00:" + currentTime.ToString("0");
        }
        else
        {
            countdownText.text = "00:0" + currentTime.ToString("0");
        }

        if (currentTime <= 0)
        {
            currentTime = 0;
            playerDied();
        }
    }

    IEnumerator RotateObject(float angle, Vector3 axis, float inTime)
    {
        // calculate rotation speed
        float rotationSpeed = angle / inTime;

        while (true)
        {
            if (isScanning)
            {
                playScanSound();
            }

            // save starting rotation position
            Quaternion startRotation = transform.rotation;

            float deltaAngle = 0;

            // rotate until reaching angle
            while (deltaAngle < angle)
            {

                if (!isScanning && movementDetected())
                {
                    playerDied();
                    yield break;
                }

                deltaAngle += rotationSpeed * Time.deltaTime;
                deltaAngle = Mathf.Min(deltaAngle, angle);

                transform.rotation = startRotation * Quaternion.AngleAxis(deltaAngle, axis);

                yield return null;
            }

            // delay here
            if (isScanning)
            {

                if (movementDetected())
                {
                    playerDied();
                    yield break;
                }

                yield return new WaitForSeconds((float)Random.Range(1, 2));
                isScanning = !isScanning;

                // greenLightSound.Play();
            }
            else
            {
                // redLightSound.Play();

                yield return new WaitForSeconds((float)Random.Range(2, 5));
                isScanning = !isScanning;
            }
        }
    }

    public void playScanSound()
    {
        scanSound.Play();
    }

    public void playGunshotSound()
    {
        gunshotSound.Play();
    }

    public bool movementDetected()
    {
        bool playerMoved = false;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            playerMoved = true;

        }

        return playerMoved;
    }

    public void playerDied()
    {
        playGunshotSound();
        scanSound.Stop();
        playerCam.SetActive(false);
        youLoseText.SetActive(true);
    }
}
