using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController : MonoBehaviour {
    [Header("Physics")]
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundChecker;

    //grounded checks
    const float GROUNDED_RADIUS = .1f;
    private bool grounded;

    private Rigidbody2D rb;

    private bool facingRight = false; //determine sprite direction

    //player input
    private float horizontalInput;
    private bool jump;

    private float move;

    //health
    public float Health;
    public float MaxHealth;
    private float healthDecayMult;

    [Header("Sprites")]
    [SerializeField] private Sprite walkSpr;
    [SerializeField] private Sprite jumpSpr;

    private SpriteRenderer spr;

    [SerializeField] private GameObject level;

    private float score = 0;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();

        Health = MaxHealth;
        healthDecayMult = 1;
    }

    private void Update() {
        horizontalInput = Input.GetAxis("Horizontal");
        jump = Input.GetButton("Jump");

        if (grounded) {
            spr.sprite = walkSpr;
        } else {
            spr.sprite = jumpSpr;
        }

        if (rb.position.y > score) score = rb.position.y;

        Health -= Time.deltaTime * healthDecayMult;
        Debug.Log("HP: " + Health);

        if (rb.position.y < -5 || Health < 0) {
            Debug.Log("GAME OVER!!! SCORE: " + Mathf.FloorToInt(score));
        }
    }

    private void FixedUpdate() {
        grounded = false;

        // from the ground checker, if any ground collider is in .2 distance that isn't the player, the player is grounded
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundChecker.position, GROUNDED_RADIUS, groundLayer);
        for (int i = 0; i < colliders.Length; i++) {
            if (colliders[i].gameObject != gameObject) {
                grounded = true;
            }
        }

        //moving character horizontally
        move = horizontalInput * Time.fixedDeltaTime * runSpeed;
        rb.velocity = new Vector2(move, rb.velocity.y);
        
        //flips player if moving in opposite direction of facing
        if (move > 0 && !facingRight) {
            facingRight = !facingRight;

            Vector3 flippedScale = transform.localScale;
            flippedScale.x *= -1;
            transform.localScale = flippedScale;
        } else if (move < 0 && facingRight) {
            facingRight = !facingRight;

            Vector3 flippedScale = transform.localScale;
            flippedScale.x *= -1;
            transform.localScale = flippedScale;
        }

        //allows for short or long jumps
        if (!grounded && !jump && rb.velocity.y > 0) {
            rb.gravityScale = 2;
        } else {
            rb.gravityScale = 1;
        }

        //can only jump when grounded
        if (grounded && jump) {
            grounded = false;
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Health") {
            Health = MaxHealth;
            healthDecayMult += .2f;
            
            level.GetComponent<LevelGenerator>().SpawnNextSet();

            Destroy(other.gameObject);
        }
    }
}
