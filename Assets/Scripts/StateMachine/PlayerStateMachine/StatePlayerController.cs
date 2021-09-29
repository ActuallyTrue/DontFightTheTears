using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Rewired;

public class StatePlayerController : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float accelerationTime;
    private float velocityXSmoothing;
    private float velocityYSmoothing;
    public float moveAfterLaunchTime;
    private float moveAfterLaunchTimer;
    public Vector2 launchVelocity;

    [HideInInspector]
    public Vector2 moveInput;
    public Player player;
    public int playerId;

    //the player's rigidbody
    public Rigidbody2D rb;

    //Everything for colliding with walls
    public LayerMask whatIsWall;

    public int playerIndex;
    public float dodgeTime;
    public float dodgeSpeed;
    public float dodgeCooldownTime;
    private float dodgeCooldownTimer;

    public GameObject parryHitbox;

    public float parryTime;
    private float parryTimer;

    public float parryCooldownTime;
    private float parryCooldownTimer;
    public PlayerManager playerManager;

    public BoxCollider2D boxCollider;
    public GameObject playerCamera;

    public Vector2 dodgeDirection;

    public GameManager gameManager;

    public Animator anim;
    public SpriteRenderer spriteRenderer;
    [HideInInspector]
    public bool invincible = false;
    [HideInInspector]
    private bool damaged = false;
    public float invincibilityTime;

    void Start()
    {
        playerId = 0;
        player = ReInput.players.GetPlayer(playerId);
        player.controllers.hasKeyboard = true;
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        playerManager = GetComponent<PlayerManager>();
        boxCollider = GetComponent<BoxCollider2D>();
        PlayerCamera camera = Instantiate(playerCamera).GetComponent<PlayerCamera>();
        camera.gameObject.SetActive(true);
        camera.makeFocusArea(this);

    }

    public void Update() {
        
        updateDodgeCooldown();
        updateParryTimer();
    }

    public Vector2 CalculatePlayerVelocity(Vector2 RBvelocity, Vector2 input, float moveSpeed, float velocityXSmoothing, float velocityYSmoothing, float accelerationTime)
    {
        float targetVelocityx = input.x * moveSpeed;
        float targetVelocityy = input.y * moveSpeed;
        Vector2 output = new Vector2();
        output.x = Mathf.SmoothDamp(RBvelocity.x, targetVelocityx, ref velocityXSmoothing, accelerationTime);
        output.y = Mathf.SmoothDamp(RBvelocity.y, targetVelocityy, ref velocityYSmoothing, accelerationTime);
        return output;
    }

    private void updateDodgeCooldown() {
        if (dodgeCooldownTimer > 0) {
            dodgeCooldownTimer -= Time.deltaTime;
        }
    }

    

    public void startDodgeCooldown() {
        dodgeCooldownTimer = dodgeCooldownTime;
    }

    public bool canDodge() {
        return dodgeCooldownTimer <= 0;
    }

    public void startParry() {
        parryTimer = parryTime;
        parryHitbox.SetActive(true);
    }

    private void updateParryTimer() {
        if (parryTimer > 0)
        {
            parryTimer -= Time.deltaTime;
        } else {
            if (parryHitbox.activeSelf) {
                cancelParry(false);
            }
        }

        if (parryCooldownTimer > 0) {
            parryCooldownTimer -= Time.deltaTime;
        }
    }

    public bool canParry() {
        return parryCooldownTimer <= 0;
    }

    public void cancelParry(bool dodgeCancel) {
        parryTimer = -1;
        parryHitbox.SetActive(false);
        if (dodgeCancel == false) {
            startParryCooldown();
        }
    }

    private void startParryCooldown() {
        parryCooldownTimer = parryCooldownTime;
    }

    public bool tookDamage() {
        return damaged;
    }

    public void setDamaged(bool enable) {
        damaged = enable;
    }


    public void HandleMovement()
    {
        moveInput = player.GetAxis2D("MoveHorizontal", "MoveVertical");
        if (rb != null) {
            Vector2 velocity = CalculatePlayerVelocity(rb.velocity, moveInput, moveSpeed, velocityXSmoothing, velocityYSmoothing, accelerationTime);
            rb.velocity = new Vector2(velocity.x, velocity.y);
        }
    }

    public void HandleLerpMovement()
    {
        moveInput = player.GetAxis2D("MoveHorizontal", "MoveVertical");
        rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed)), 1f * Time.deltaTime);
    }

    public Vector2 clampTo8Directions(Vector2 vectorToClamp) {
        if (vectorToClamp.x > 0.1f && (vectorToClamp.y < 0.1f && vectorToClamp.y > -0.1f))
        {
            //right
            return new Vector2(1,0);
        }
        if (vectorToClamp.x > 0.1f && vectorToClamp.y < -0.1f)
        {
            //down right
            return new Vector2(1,-1).normalized;
        }
        if ((vectorToClamp.x < 0.1f && vectorToClamp.x > -0.1) && vectorToClamp.y < -0.1f)
        {
            //down
            return new Vector2(0,-1);
        }
        if (vectorToClamp.x < -0.1f && vectorToClamp.y < -0.1f)
        {
            //down left
            return new Vector2(-1,-1).normalized;
        }
        if (vectorToClamp.x < -0.1f && (vectorToClamp.y < 0.1f && vectorToClamp.y > -0.1f))
        {
            //left
            return new Vector2(-1,0);
        }
        if (vectorToClamp.x < -0.1f && vectorToClamp.y > 0.1f)
        {
            //up left
            return new Vector2(-1,1).normalized;
        }
        if ((vectorToClamp.x < 0.1f && vectorToClamp.x > -0.1) && vectorToClamp.y > 0.1f)
        {
            //up
            return new Vector2(0,1);
        }
        if (vectorToClamp.x > 0.1f && vectorToClamp.y > 0.1f)
        {
            //up right
            return new Vector2(1,1).normalized;
        }

        return Vector2.zero;
    }

     //void OnCollisionEnter2D(Collision2D collision)
     //{
     //    if (collision.gameObject.CompareTag("Platform"))
     //    {
     //        transform.parent = collision.transform;
     //    }
     //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    transform.parent = null;
    //}

    public void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.layer == 12) { //if it's a hazard
            if (invincible == false) {
                setDamaged(true);
                SetPlayerInvincibility(true);
            }
        }
    }

    public void SetPlayerInvincibility(bool enable) {
        invincible = enable;
        if (enable) {
            StartCoroutine(InvincibleTimer());
        }
    }

    IEnumerator InvincibleTimer() {
        yield return new WaitForSeconds(invincibilityTime);
        SetPlayerInvincibility(false);
    } 
}
