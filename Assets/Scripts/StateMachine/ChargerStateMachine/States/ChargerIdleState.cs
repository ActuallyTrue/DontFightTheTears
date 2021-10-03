using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerIdleState : ChargerState {
    private float timeTillAttack = 1f;
    private float timer;
    public override void Enter(ChargerStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        stateInput.rb.velocity = Vector2.zero;
        timer = timeTillAttack;
    }

    public override void Update(ChargerStateInput stateInput)
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && stateInput.chargerController.isPlayerInRange()) {
            character.ChangeState<ChargerAttackState>();
        }

    }


    public override void FixedUpdate(ChargerStateInput stateInput) {
        stateInput.chargerController.HandleMovement(Vector2.zero); 
    }

}
