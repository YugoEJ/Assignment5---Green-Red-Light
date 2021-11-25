using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameObject playerCam;
    public GameObject youWinText;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Third Person Player" || collision.gameObject.name == "PlayerBody")
        {
            playerCam.SetActive(false);
            youWinText.SetActive(true);
        }
    }
}
