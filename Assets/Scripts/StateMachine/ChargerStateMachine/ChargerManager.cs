using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerManager : Character<ChargerManager, ChargerState, ChargerStateInput> {

	override protected void Init()
    {
        //gameObject.name = GetComponent<PhotonView>().Owner.NickName;
        stateInput.stateMachine = this;
        stateInput.playerController = FindObjectOfType<StatePlayerController>();
        stateInput.chargerController = GetComponent<ChargerController>();
        stateInput.anim = stateInput.playerController.anim;
        stateInput.spriteRenderer = stateInput.playerController.spriteRenderer;
        stateInput.rb = GetComponent<Rigidbody2D>();
        stateInput.boxCollider = GetComponent<BoxCollider2D>();
        stateInput.stateChanged = false;
        stateInput.gameManager = FindObjectOfType<GameManager>();
    }

    override protected void SetInitialState()
    {
        ChangeState<ChargerIdleState>();
    }

    public ChargerStateInput GetStateInput() {
        return stateInput;
    }

    public ChargerState GetState() {
        return state;
    }

}

public abstract class ChargerState : CharacterState<ChargerManager, ChargerState, ChargerStateInput>
{

}

public class ChargerStateInput : CharacterStateInput
{
    
    public Animator anim;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider;
    public StatePlayerController playerController;
    public ChargerController chargerController;
    public bool stateChanged;
    public ChargerManager stateMachine;

    public GameManager gameManager;
}
