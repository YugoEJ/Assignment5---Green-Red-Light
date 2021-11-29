using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameFlow : MonoBehaviour
{
    Scene scene;

    public GameObject playerCam;
    public GameObject youLoseText;
    public GameObject youWinText;

    public AudioSource gunshotSound;
    public AudioSource greenLightSound;
    public AudioSource redLightSound;
    public AudioSource defeatSound;
    public AudioSource backgroundMusic;

    private AudioSource[] allAudioSources;

    public Collision finishLine;

    public Text countdownText;

    float currentTime = 0f;
    float startingTime = 32f;

    public Vector3 rotationAngle = new Vector3(0f, 1f, 0f);

    private bool isScanning = true;
    private bool skipFirstSounds = true;

    CursorLockMode lockMode;

    void Awake()
    {
        lockMode = CursorLockMode.Locked;
        Cursor.lockState = lockMode;
    }

    void Start()
    {
        scene = SceneManager.GetActiveScene(); 

        currentTime = startingTime;
        youLoseText.SetActive(false);
        youWinText.SetActive(false);

        float scanningTime = (float)Random.Range(4, 6);
        float nonScanningTime = (float)Random.Range(1, 4);

        StartCoroutine(RotateObject(180, rotationAngle, 0.25f));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            stopAudio();
            SceneManager.LoadScene(scene.name);
        }

        if (skipFirstSounds)
        {
            stopAudio();
            skipFirstSounds = false;
        }

        currentTime -= 1f * Time.deltaTime;

        if (currentTime > 9)
        {
            countdownText.text = "00:" + currentTime.ToString("0");
        }
        else
        {
            countdownText.text = "00:0" + currentTime.ToString("0");
        }

        if (currentTime <= 0 && CollisionWithPlayer.playerWins == false)
        {
            currentTime = 0;
            stopAudio();
            playerDied();
        }

        if (CollisionWithPlayer.playerWins == true)
        {
            redLightSound.Stop();
            greenLightSound.Stop();
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
                playRedLightSound();
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

            if (isScanning)
            {

                if (movementDetected())
                {
                    playerDied();
                    yield break;
                }

                yield return new WaitForSeconds((float)Random.Range(1, 2));
                playGreenLightSound();
                isScanning = !isScanning;
            }
            else
            {
                yield return new WaitForSeconds((float)Random.Range(2, 5));
                isScanning = !isScanning;
            }
        }
    }

    public void stopAudio()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            if (audioS == backgroundMusic)
            {

            } else
            {
                audioS.Stop();
            }
        }
    }

    public void playGunshotSound()
    {
        gunshotSound.Play();
    }

    public void playGreenLightSound()
    {
        greenLightSound.Play();
    }

    public void playRedLightSound()
    {
        redLightSound.Play();
    }

    public void playDefeatSound()
    {
        defeatSound.Play();
    }

    public void playBGM()
    {
        backgroundMusic.Play();
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
        backgroundMusic.Stop();
        playDefeatSound();
        playGunshotSound();
        playerCam.SetActive(false);
        youLoseText.SetActive(true);
    }
}
