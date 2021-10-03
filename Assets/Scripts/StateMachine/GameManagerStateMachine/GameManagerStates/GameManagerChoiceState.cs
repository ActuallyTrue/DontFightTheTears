using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerChoiceState : GameManagerState {

    public int choiceChosen = -1;
    public override void Enter(GameManagerStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        choiceChosen = -1;
        stateInput.playerManager.GetStateInput().playerController.setCanAct(false);
        stateInput.bossManager.GetStateInput().bossController.setCanAct(false);
        if (stateInput.bossManager.GetStateInput().bossController.resolve <= 0.0f) {
            stateInput.uiManager.showEnding();
            return;
        }
        if (stateInput.bossManager.GetStateInput().bossController.resolve <= 50.0f) {
            stateInput.uiManager.showChoice1();
        } 

    }

    public override void Update(GameManagerStateInput stateInput)
    {
        if (choiceChosen == 1) {
            
            stateInput.bossManager.GetStateInput().bossController.resolve = 70.0f;
            character.ChangeState<GameManagerBossState>();
            return;
        }

        if (choiceChosen == 2) {
            stateInput.bossManager.GetStateInput().bossController.resolve = 30.0f;
            character.ChangeState<GameManagerBossState>();
            
            return;
        }
    }

    public override void ForceCleanUp(GameManagerStateInput stateInput)
    {
        base.ForceCleanUp(stateInput);
        stateInput.uiManager.hideAll();
        stateInput.playerManager.GetStateInput().playerController.setCanAct(true);
        stateInput.bossManager.GetStateInput().bossController.setCanAct(true);
    }
}

