using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithPlayer : MonoBehaviour
{
    public GameObject playerCam;
    public GameObject youWinText;

    public AudioSource victorySound;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "PlayerFace")
        {
            playerCam.SetActive(false);
            youWinText.SetActive(true);
            victorySound.Play();
        }
    }
}
