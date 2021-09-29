using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryController : MonoBehaviour
{
    public StatePlayerController playerController;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == 7)
        {
            ChargerManager charger = other.GetComponent<ChargerManager>();
            if (charger.GetState() is ChargerAttackState) {
                playerController.cancelParry(false);
                other.gameObject.SetActive(false);
            }
            
            //lower the enemy's resolve
        }
    }
}
