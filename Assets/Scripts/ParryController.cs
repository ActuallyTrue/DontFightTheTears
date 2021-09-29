using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryController : MonoBehaviour
{
    public StatePlayerController playerController;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == 7)
        {
            playerController.cancelParry(false);
            //lower the enemy's resolve
        }
    }
}
