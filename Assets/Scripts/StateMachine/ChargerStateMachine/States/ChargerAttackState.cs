using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerAttackState : ChargerState {
    private Vector2 attackDir;
    private float timer;
    public override void Enter(ChargerStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        GameObject player = stateInput.playerController.gameObject;
        Vector2 chargerPos = new Vector2(stateInput.chargerController.gameObject.transform.position.x, stateInput.chargerController.gameObject.transform.position.y);
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        attackDir = playerPos - chargerPos;
        timer = stateInput.chargerController.attackTime;
    }

    public override void Update(ChargerStateInput stateInput)
    {
        timer -= Time.deltaTime;
        GameObject player = stateInput.playerController.gameObject;
        Vector2 chargerPos = new Vector2(stateInput.chargerController.gameObject.transform.position.x, stateInput.chargerController.gameObject.transform.position.y);
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 targetDir = playerPos - chargerPos;
        attackDir = Vector2.MoveTowards(attackDir, targetDir, 0.08f);

        if (timer <= 0) {
            character.ChangeState<ChargerIdleState>();
        }

    }


    public override void FixedUpdate(ChargerStateInput stateInput) {
        stateInput.chargerController.HandleMovement(attackDir); 
    }

}