using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChargeState : BossState {
    private Vector2 attackDir;
    private float timer;

    public bool parried;
    public override void Enter(BossStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        GameObject player = stateInput.playerController.gameObject;
        Vector2 chargerPos = new Vector2(stateInput.bossController.gameObject.transform.position.x, stateInput.bossController.gameObject.transform.position.y);
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        attackDir = playerPos - chargerPos;
        timer = stateInput.bossController.attackTime;
        parried = false;
    }

    public override void Update(BossStateInput stateInput)
    {
        if (stateInput.bossController.canAct == false) {
            return;
        }
        timer -= Time.deltaTime;
        GameObject player = stateInput.playerController.gameObject;
        Vector2 chargerPos = new Vector2(stateInput.bossController.gameObject.transform.position.x, stateInput.bossController.gameObject.transform.position.y);
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 targetDir = playerPos - chargerPos;
        attackDir = Vector2.MoveTowards(attackDir, targetDir, 0.08f);

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
        stateInput.bossController.HandleMovement(attackDir); 
    }

}