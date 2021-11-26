using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyCollision : MonoBehaviour
{
    public GameObject playerCam;
    public GameObject youWinText;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Finishing Line")
        {
            playerCam.SetActive(false);
            youWinText.SetActive(true);
        }
    }
}
