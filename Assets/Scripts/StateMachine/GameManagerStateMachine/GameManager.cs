using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Character<GameManager, GameManagerState, GameManagerStateInput> {

	override protected void Init()
    {
        stateInput.stateMachine = this;
        stateInput.variableHolder = GetComponent<GameManagerVariableHolder>();
        stateInput.gameManagerController = GetComponent<GameManagerController>();
        stateInput.playerManager = FindObjectOfType<PlayerManager>();
        stateInput.bossManager = FindObjectOfType<BossManager>();
        stateInput.uiManager = stateInput.variableHolder.uiManager;
        stateInput.uiManager.hideAll();
        stateInput.bossWall = stateInput.variableHolder.bossWall;
        stateInput.uiManager.showBeginningLine();

    }

    override protected void SetInitialState()
    {
        ChangeState<GameManagerBeginState>();
    }

    public GameManagerStateInput GetStateInput() {
        return stateInput;
    }

    public GameManagerState GetState() {
        return state;
    }

}

public abstract class GameManagerState : CharacterState<GameManager, GameManagerState, GameManagerStateInput>
{

}

public class GameManagerStateInput : CharacterStateInput
{
    public GameManagerController gameManagerController;
    public GameManager stateMachine;
    public GameManagerVariableHolder variableHolder;
    public UIManager uiManager;

    public PlayerManager playerManager;

    public BossManager bossManager;

    public GameObject bossWall;
}

