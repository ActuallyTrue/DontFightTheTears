using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerBossState : GameManagerState {

    bool doneFirstChoice = false;
    public override void Enter(GameManagerStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        stateInput.bossManager.GetStateInput().bossController.setCanAct(true);
        stateInput.uiManager.turnOnEnemyResolve();
    }

    public override void Update(GameManagerStateInput stateInput)
    {
        if (doneFirstChoice == false && stateInput.bossManager.GetStateInput().bossController.resolve <= 50.0f) {
            doneFirstChoice = true;
            character.ChangeState<GameManagerChoiceState>();
        }

        if (stateInput.bossManager.GetStateInput().bossController.resolve <= 0.0f) {
            character.ChangeState<GameManagerChoiceState>();
        }
    }

    public override void ForceCleanUp(GameManagerStateInput stateInput)
    {
        base.ForceCleanUp(stateInput);
    }
}

