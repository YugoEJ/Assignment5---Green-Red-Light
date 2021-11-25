using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
public class CountdownTimer : MonoBehaviour
{
    public GameObject playerCam;
    public GameObject youLoseText;

    public AudioSource gunshotSound;

    public Text countdownText;

    float currentTime = 0f;
    float startingTime = 15f;

    void Start()
    {
        currentTime = startingTime;
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
            gunshotSound.Stop();
            gunshotSound.Play();
            playerCam.SetActive(false);
            youLoseText.SetActive(true);
        }
    }
}
*/