using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState {

    public override void Enter(PlayerStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        stateInput.lastXDir = 0;
        stateInput.anim.Play("Player_idle");
    }

    public override void Update(PlayerStateInput stateInput)
    {
        if (stateInput.playerController.tookDamage()) {
            stateInput.playerController.setDamaged(false);
            LaunchStateTransitionInfo transitionInfo = new LaunchStateTransitionInfo(stateInput.playerController.launchVelocity, stateInput.playerController.moveAfterLaunchTime, true);
            character.ChangeState<PlayerLaunchState>(transitionInfo);
            return;
        }

        if (stateInput.playerController.canDodge() && stateInput.player.GetButtonDown("Dodge")) {
            character.ChangeState<PlayerDodgeState>();
            return;
        }
        
        // // Movement animations and saving previous input
            int horizontalMovement = (int)Mathf.Sign(stateInput.player.GetAxis("MoveHorizontal"));
            if (stateInput.player.GetAxis("MoveHorizontal") > -0.01f && stateInput.player.GetAxis("MoveHorizontal") < 0.01f) {
                horizontalMovement = 0;
            }

        // if (stateInput.lastXDir != horizontalMovement)
        // {
        //     if (horizontalMovement != 0)
        //     {
        //         stateInput.anim.Play("Player_run");
        //         stateInput.spriteRenderer.flipX = horizontalMovement == -1;
        //     }
        //     else
        //     {
        //         stateInput.anim.Play("Player_idle");
        //     }
        // }
        stateInput.lastXDir = horizontalMovement;
    }


    public override void FixedUpdate(PlayerStateInput stateInput) {
        stateInput.playerController.HandleMovement();
    }

}
