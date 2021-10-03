using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerBeginState : GameManagerState {

    public override void Enter(GameManagerStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        stateInput.playerManager.GetStateInput().playerController.setCanAct(false);
        stateInput.bossManager.GetStateInput().bossController.setCanAct(false);
    }

    public override void Update(GameManagerStateInput stateInput)
    {
        if (stateInput.playerManager.GetStateInput().player.GetAnyButton())
        {
            character.ChangeState<GameManagerWaitState>();
        }
        
        
    }

    public override void ForceCleanUp(GameManagerStateInput stateInput)
    {
        base.ForceCleanUp(stateInput);
        stateInput.uiManager.hideAll();
        stateInput.playerManager.GetStateInput().playerController.setCanAct(true);
    }
}

