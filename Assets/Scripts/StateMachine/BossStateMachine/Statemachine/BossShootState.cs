using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShootState : BossState {
    private Vector2 attackDir;

    private Vector2 moveDir;
    private float timer;

    public bool parried;
    public override void Enter(BossStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        GameObject player = stateInput.playerController.gameObject;
        Vector2 chargerPos = new Vector2(stateInput.bossController.gameObject.transform.position.x, stateInput.bossController.gameObject.transform.position.y);
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        moveDir = new Vector2(Random.Range(0,16) / 16f, Random.Range(0,16) / 16f);
        moveDir.x = (Random.Range(0,2) == 0 ? 1 : -1) * moveDir.x;
        moveDir.y = (Random.Range(0,2) == 0 ? 1 : -1) * moveDir.y;
        attackDir = playerPos - chargerPos;
        timer = stateInput.bossController.attackTime;
        parried = false;
        stateInput.bossController.shoot(attackDir.normalized);
    }

    public override void Update(BossStateInput stateInput)
    {
        if (stateInput.bossController.canAct == false) {
            return;
        }
        
        timer -= Time.deltaTime;
        
        if (timer <= 0) {
            character.ChangeState<BossIdleState>();
        }

        if (parried) {
            character.ChangeState<BossIdleState>();
        }

    }

    public override void ForceCleanUp(BossStateInput stateInput)
    {
        if (parried)
        {
            stateInput.bossController.pushBack();
            parried = false;
        }

    }


    public override void FixedUpdate(BossStateInput stateInput) {
        stateInput.bossController.HandleMovement(moveDir); 
    }

}