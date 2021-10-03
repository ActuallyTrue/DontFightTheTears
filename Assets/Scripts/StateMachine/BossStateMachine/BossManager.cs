using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : Character<BossManager, BossState, BossStateInput> {

	override protected void Init()
    {
        //gameObject.name = GetComponent<PhotonView>().Owner.NickName;
        stateInput.stateMachine = this;
        stateInput.playerController = FindObjectOfType<StatePlayerController>();
        stateInput.bossController = GetComponent<BossController>();
        stateInput.anim = stateInput.playerController.anim;
        stateInput.spriteRenderer = stateInput.playerController.spriteRenderer;
        stateInput.rb = GetComponent<Rigidbody2D>();
        stateInput.boxCollider = GetComponent<BoxCollider2D>();
        stateInput.stateChanged = false;
        stateInput.gameManager = FindObjectOfType<GameManager>();
    }

    override protected void SetInitialState()
    {
        ChangeState<BossIdleState>();
    }

    public BossStateInput GetStateInput() {
        return stateInput;
    }

    public BossState GetState() {
        return state;
    }

}

public abstract class BossState : CharacterState<BossManager, BossState, BossStateInput>
{

}

public class BossStateInput : CharacterStateInput
{
    
    public Animator anim;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider;
    public StatePlayerController playerController;
    public BossController bossController;
    public bool stateChanged;
    public BossManager stateMachine;

    public GameManager gameManager;
}
