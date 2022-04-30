using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    public Rigidbody2D rb;
    public float airDragMultiplier;
    
    [Header("Wall Detection")]
    public float wallDetectionDistance;
    public LayerMask wallLayer;

    [Header("Ground Check")]
    public Transform groundCheckPos;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    bool isGrounded;

    [Header("Graphics")]
    public SpriteRenderer dummySprite;
    public Animator anim;
    string currentAnimation;
    const string dummyJump = "DummyJump_Anim";
    const string dummyWalk = "DummyWalk_Anim";

    float horizontalOrientation;

    void Start()
    {
        horizontalOrientation = 1.0f;
    }


    void Update()
    {
        UpdateGraphicLookingDir();
        Move();
    }

    private void FixedUpdate()
    {
        CheckSurroundings();
    }


    void Move()
    {
        //Debug.DrawRay(transform.position, transform.right, Color.green, wallDetectionDistance);
        if (Physics2D.Raycast(transform.position, transform.right, wallDetectionDistance, wallLayer))
        {
            horizontalOrientation = -1.0f;
        }
        if (Physics2D.Raycast(transform.position, -transform.right, wallDetectionDistance, wallLayer))
        {
            horizontalOrientation = 1.0f;
        }

        if (isGrounded)
        {
            rb.velocity = new Vector2(speed * horizontalOrientation, rb.velocity.y);

            ChangeAnimationState(dummyWalk);
        }

        if (!isGrounded)
        {
            rb.velocity = new Vector2(speed * horizontalOrientation * airDragMultiplier, rb.velocity.y);

            ChangeAnimationState(dummyJump);
        }
    }

    void UpdateGraphicLookingDir()
    {
        if (horizontalOrientation == 1.0f)
        {
            dummySprite.flipX = false;
        }
        else
        {
            dummySprite.flipX = true;
        }
    }

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheckPos.position, groundCheckRadius);
        Gizmos.DrawRay(transform.position, transform.right * wallDetectionDistance);
    }

    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        anim.Play(newAnimation);

        currentAnimation = newAnimation;
    }
}
