using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Config parameters
    [Range(1, 10)][Tooltip("Movement speed")]
    [SerializeField] float movementSpeed = 5f;
    [Range(1, 10)][Tooltip("Jump power")]
    [SerializeField] float jumpPower = 5f;
    [Range(1, 10)][Tooltip("Climb speed")]
    [SerializeField] float climbSpeed = 5f;
    [Range(1, 10)][Tooltip("Jump power")]
    [SerializeField] float deathKick = 5f;

    // State variables
    bool isAlive = true;

    // Cached component references
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D feetCollider;
    float gravityScaleAtStart;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();

        gravityScaleAtStart = myRigidbody.gravityScale;
    }

    private void Update()
    {
        if (!isAlive) { return; }

        Move();
        Jump();
        Climb();
        FlipSprite();
        SetAnimationState();

        Die();
    }

    private void Move()
    {
        float controlThrow = Input.GetAxis("Horizontal");

        myRigidbody.velocity = new Vector2
            (controlThrow * movementSpeed, myRigidbody.velocity.y);
    }

    private void Jump()
    {
        bool isTouchingGround = feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
        if (Input.GetButtonDown("Jump") && isTouchingGround)
        {
            Vector2 jumpVelocity = new Vector2(0f, jumpPower);
            myRigidbody.velocity += jumpVelocity;
        }
    }

    private void Climb()
    {
        bool isTouchingClimbableObject = feetCollider.IsTouchingLayers(LayerMask.GetMask("Climbable"));
        if (!isTouchingClimbableObject) { myRigidbody.gravityScale = gravityScaleAtStart; myAnimator.SetBool("isClimbing", false); return; }

        float controlThrow = Input.GetAxis("Vertical");

        myRigidbody.gravityScale = 0f;

        myRigidbody.velocity = new Vector2
            (myRigidbody.velocity.x, controlThrow * climbSpeed);
    }

    private void FlipSprite()
    {
        bool isMovingHorizontally = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (isMovingHorizontally)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

    private void Die()
    {
        bool touchedEnemyOrHazard = bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")) 
            || feetCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards"));

        if (touchedEnemyOrHazard)
        {
            isAlive = false;
            myAnimator.SetTrigger("Die");
            myRigidbody.velocity = new Vector2(Mathf.Sign(myRigidbody.velocity.x) * deathKick, deathKick);
            FindObjectOfType<GameSession>().processPlayerDeath();
        }
    }

    private void SetAnimationState()
    {
        bool isFalling = myRigidbody.velocity.y < -1;
        bool isMovingVertically = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        bool isMovingHorizontally = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        bool isTouchingGround = feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
        bool isTouchingClimbableObject = bodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbable"));
        
        myAnimator.SetBool("isFalling", isFalling);
        myAnimator.SetBool("isWalking", isMovingHorizontally);
        myAnimator.SetBool("isJumping", !isTouchingGround);
        myAnimator.SetBool("isClimbing", isMovingVertically && isTouchingClimbableObject);
        myAnimator.SetBool("isTouchingGround", isTouchingGround);
        myAnimator.SetBool("isTouchingClimbableObject", isTouchingClimbableObject);
    }
}


