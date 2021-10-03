using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTriggerController : MonoBehaviour
{
    public GameManager gameManager;

    void Start() {
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.layer == 8) {
            if (gameManager.GetState() is GameManagerWaitState) {
                ((GameManagerWaitState) gameManager.GetState()).triggerHit = true; 
            }
        }

    }
}
