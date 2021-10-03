using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerIdleState : ChargerState {

    public override void Enter(ChargerStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        stateInput.rb.velocity = Vector2.zero;
    }

    public override void Update(ChargerStateInput stateInput)
    {
        if (stateInput.chargerController.isPlayerInRange()) {
            character.ChangeState<ChargerAttackState>();
        }

    }


    public override void FixedUpdate(ChargerStateInput stateInput) {
        stateInput.playerController.HandleMovement(); 
    }

}
