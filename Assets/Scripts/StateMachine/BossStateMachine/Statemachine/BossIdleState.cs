using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : BossState {
    private float timer;
    public override void Enter(BossStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        stateInput.rb.velocity = Vector2.zero;
        timer = stateInput.bossController.attackCooldownTime;
    }

    public override void Update(BossStateInput stateInput)
    {
        if (stateInput.bossController.canAct == false) {
            return;
        }
        
        timer -= Time.deltaTime;

        if (timer <= 0) {

            if (stateInput.bossController.resolve > 50.0f) {
                character.ChangeState<BossShootState>();
            }

            if (stateInput.bossController.isPlayerInRange() && stateInput.bossController.resolve <= 50.0f) {
                character.ChangeState<BossChargeState>();
            } 
        }
    }


    public override void FixedUpdate(BossStateInput stateInput) {
        
    }

}
