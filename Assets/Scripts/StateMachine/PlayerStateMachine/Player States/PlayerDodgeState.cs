using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerState
{
    private float dodgeTimer; 
    private float lerper;   
    public override void Enter(PlayerStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        stateInput.playerController.dodgeDirection = stateInput.playerController.clampTo8Directions(stateInput.player.GetAxis2D("MoveHorizontal", "MoveVertical"));
        if (stateInput.playerController.dodgeDirection.x == 0 && stateInput.playerController.dodgeDirection.y == 0) {
            if (stateInput.spriteRenderer.flipX == true) {
                stateInput.playerController.dodgeDirection = new Vector2(-1, 0);
            } else {
                stateInput.playerController.dodgeDirection = new Vector2(1, 0);
            }
        }
        stateInput.rb.velocity = stateInput.playerController.dodgeDirection * stateInput.playerController.dodgeSpeed;
        dodgeTimer = stateInput.playerController.dodgeTime;
        lerper = 0;
    }

    public override void ForceCleanUp(PlayerStateInput stateInput)
    {
        //stateInput.rb.velocity = Vector2.zero;
        stateInput.rb.drag = 0;
        stateInput.playerController.startDodgeCooldown();
    }

    public override void Update(PlayerStateInput stateInput)
    {
        stateInput.rb.velocity = stateInput.playerController.dodgeDirection.normalized * stateInput.playerController.dodgeSpeed;
        if (stateInput.playerController.tookDamage()) {
            stateInput.playerController.setDamaged(false);
            LaunchStateTransitionInfo transitionInfo = new LaunchStateTransitionInfo(stateInput.playerController.launchVelocity, stateInput.playerController.moveAfterLaunchTime, true);
            character.ChangeState<PlayerLaunchState>(transitionInfo);
            return;
        }

        if (dodgeTimer > 0)
        { 
            dodgeTimer -= Time.deltaTime;
            lerper += Time.deltaTime;
            stateInput.rb.drag = Mathf.Lerp(18, 0, lerper / stateInput.playerController.dodgeTime);
        } else
        {
            character.ChangeState<PlayerIdleState>();
        }
    }
}
