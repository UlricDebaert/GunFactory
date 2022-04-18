using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    public Rigidbody2D rb;

    [Header("Jump")]
    public float jumpForce;
    public float airDragMultiplier;

    [Header("Ground Check")]
    public Transform groundCheckPos;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    bool isGrounded;

    [Header("Graphics")]
    public SpriteRenderer playerSprite;
    public Animator anim;
    string currentAnimation;
    const string playerIdle = "PlayerIdle_Anim";
    const string playerJump = "PlayerJump_Anim";
    const string playerWalk = "PlayerWalk_Anim";

    float horizontalInput;

    void Start()
    {

    }


    void Update()
    {
        CheckInput();
        CheckSurroundings();
    }


    void CheckInput()
    {
        if (isGrounded)
        {
            if (Input.GetButton("Horizontal"))
            {
                horizontalInput = Input.GetAxisRaw("Horizontal");

                rb.velocity = new Vector2(speed * horizontalInput, rb.velocity.y);

                ChangeAnimationState(playerWalk);
            }

            if (!Input.GetButton("Horizontal"))
            {
                rb.velocity = new Vector2(0, rb.velocity.y);

                ChangeAnimationState(playerIdle);
            }
        }

        if (!isGrounded)
        {
            if (Input.GetButton("Horizontal"))
            {
                horizontalInput = Input.GetAxisRaw("Horizontal");

                rb.velocity = new Vector2(speed * horizontalInput * airDragMultiplier, rb.velocity.y);
            }

            if (!Input.GetButton("Horizontal"))
            {
                rb.velocity = new Vector2(rb.velocity.x*airDragMultiplier, rb.velocity.y);
            }

            ChangeAnimationState(playerJump);
        }

        if (horizontalInput>0)
        {
            playerSprite.flipX = false;
        }
        if (horizontalInput<0)
        {
            playerSprite.flipX = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheckPos.position, groundCheckRadius);
    }

    void ChangeAnimationState(string newAnimation)
    {
        print("change anim to" + newAnimation);
        
        if (currentAnimation == newAnimation) return;

        anim.Play(newAnimation);

        currentAnimation = newAnimation;
    }
}
