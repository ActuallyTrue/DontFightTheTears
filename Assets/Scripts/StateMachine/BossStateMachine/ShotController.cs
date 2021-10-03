using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    Rigidbody2D rb;
    public BossManager boss;
    
    public float timeAlive = 3f;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boss = FindObjectOfType<BossManager>();
        timer = timeAlive;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            Destroy(this.gameObject);
        }   
    }

    public void killShot() {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == 8) {
            StatePlayerController player = other.gameObject.GetComponent<StatePlayerController>();
            player.takeDamage();
            player.launchVelocity = rb.velocity.normalized;
            ((BossShootState)boss.GetState()).parried = true;
            killShot();
        }
    }
}
