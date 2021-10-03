using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerWaitState : GameManagerState {

    public bool triggerHit;
    public override void Enter(GameManagerStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        triggerHit = false;
    }

    public override void Update(GameManagerStateInput stateInput)
    {
        if (triggerHit) {
            character.ChangeState<GameManagerBossState>();
        }
    }

    public override void ForceCleanUp(GameManagerStateInput stateInput)
    {
        base.ForceCleanUp(stateInput);
        stateInput.bossWall.SetActive(true);
    }
}

