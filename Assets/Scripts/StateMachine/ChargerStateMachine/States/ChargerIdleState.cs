using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerIdleState : ChargerState {
    private float timer;
    public override void Enter(ChargerStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        stateInput.rb.velocity = Vector2.zero;
        timer = stateInput.chargerController.attackCooldownTime;
    }

    public override void Update(ChargerStateInput stateInput)
    {
        timer -= Time.deltaTime;

        if (timer <= 0) {
            if (stateInput.chargerController.isPlayerInRange()) {
                character.ChangeState<ChargerAttackState>();
            }
        }
    }


    public override void FixedUpdate(ChargerStateInput stateInput) {
        
    }

}
