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
            if (charger != null) {
                if (charger.GetState() is ChargerAttackState) {
                    playerController.cancelParry(false);
                    other.gameObject.SetActive(false);
                    return;
                }
            }

            BossManager boss = other.GetComponent<BossManager>();
            if (boss != null) {
                BossState currentState = boss.GetState();
                if (currentState is BossChargeState) {
                    playerController.cancelParry(false);
                    ((BossChargeState)currentState).parried = true;
                    boss.GetStateInput().bossController.takeDamage();
                    return;
                }
            }

            ShotController shot = other.GetComponent<ShotController>();
            if (shot != null) {
                    playerController.cancelParry(false);
                    shot.boss.GetStateInput().bossController.takeDamage();
                    shot.killShot();
                    return;
            }
            
            
            //lower the enemy's resolve
        }
    }
}
