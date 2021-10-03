using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossController : MonoBehaviour
{
    public float attackSpeed = 6f;
    public float attackRange = 20f;
    public float attackTime = 2f;
    public float attackCooldownTime = 1f;
    public float accelerationTime;
    private float velocityXSmoothing;
    private float velocityYSmoothing;

    public float resolve = 100f;

    //the charger's rigidbody
    public Rigidbody2D rb;

    //Everything for colliding with walls
    public LayerMask whatIsWall;
    public BossManager bossManager;
    public PlayerManager playerManager;

    public BoxCollider2D boxCollider;

    public GameObject chargeHitbox;

    public GameManager gameManager;

    public Animator anim;
    public SpriteRenderer spriteRenderer;
    
    public GameObject shotPrefab;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        bossManager = GetComponent<BossManager>();
        boxCollider = GetComponent<BoxCollider2D>();
        playerManager = FindObjectOfType<PlayerManager>();

    }

    public void Update() {
        
    }

    public bool isPlayerInRange() {
        GameObject player = playerManager.GetStateInput().playerController.gameObject;
        Vector2 chargerPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        return Vector2.Distance(chargerPos, playerPos) <= attackRange;
    }

    public void pushBack() {
        rb.AddForce(new Vector2(rb.velocity.normalized.x * -50, rb.velocity.normalized.y * -50), ForceMode2D.Force);
    }

    public Vector2 CalculateVelocity(Vector2 RBvelocity, Vector2 input, float moveSpeed, float velocityXSmoothing, float velocityYSmoothing, float accelerationTime)
    {
        float targetVelocityx = input.x * moveSpeed;
        float targetVelocityy = input.y * moveSpeed;
        Vector2 output = new Vector2();
        output.x = Mathf.SmoothDamp(RBvelocity.x, targetVelocityx, ref velocityXSmoothing, accelerationTime);
        output.y = Mathf.SmoothDamp(RBvelocity.y, targetVelocityy, ref velocityYSmoothing, accelerationTime);
        return output;
    }

    public void HandleMovement(Vector2 input)
    {
        if (rb != null) {
            Vector2 velocity = CalculateVelocity(rb.velocity, input, attackSpeed, velocityXSmoothing, velocityYSmoothing, accelerationTime);
            rb.velocity = new Vector2(velocity.x, velocity.y);
        }
    }

    public void HandleLerpMovement()
    {
        //rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed)), 1f * Time.deltaTime);
    }

     void OnCollisionEnter2D(Collision2D collision)
     {
        if (collision.gameObject.layer == 8)
        {
            StatePlayerController player = collision.gameObject.GetComponent<StatePlayerController>();
            player.takeDamage();
            player.launchVelocity = rb.velocity.normalized;
            ((BossChargeState)bossManager.GetState()).parried = true;
        }
     }

     public void shoot(Vector2 input) {
        Rigidbody2D shot = Instantiate(shotPrefab, gameObject.transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        shot.velocity = input * 20;
     }

     public void takeDamage() {
         resolve -= 10f;
         Debug.Log("Enemy resolve: " + resolve);
     } 

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    transform.parent = null;
    //}
}
