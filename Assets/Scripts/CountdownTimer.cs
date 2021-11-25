using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 45f;

    public Text countdownText;

    private void Start()
    {
        currentTime = startingTime;
    }

    private void Update()
    {
        countdownText.color = Color.red;

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
        }
    }
    
}
