using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    public Rigidbody2D rb;

    [Header("Friction Forces")]
    public AnimationCurve acceleration = AnimationCurve.EaseInOut(0, 0, 0.75f, 1);
    public AnimationCurve decceleration = AnimationCurve.EaseInOut(0, 1, 2, 0);
    float accelerationMultiplier;
    float deccelerationMultiplier;
    float timeSinceAccelerated;
    float timeSinceDeccelerated;

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
        UpdateGraphicLookingDir();
        CheckInput();
    }

    private void FixedUpdate()
    {
        CheckSurroundings();
    }


    void CheckInput()
    {
        if (isGrounded)
        {
            if (Input.GetButton("Horizontal"))
            {
                horizontalInput = Input.GetAxisRaw("Horizontal");

                rb.velocity = new Vector2(speed * horizontalInput * accelerationMultiplier, rb.velocity.y);
                //rb.AddForce(new Vector2(horizontalInput, 0) * speed, ForceMode2D.Force);

                timeSinceAccelerated += Time.deltaTime;
                timeSinceDeccelerated = 0;

                ChangeAnimationState(playerWalk);
            }

            if (!Input.GetButton("Horizontal"))
            {
                rb.velocity = new Vector2(rb.velocity.x * deccelerationMultiplier, rb.velocity.y);

                timeSinceDeccelerated += Time.deltaTime;
                timeSinceAccelerated = 0;

                ChangeAnimationState(playerIdle);
            }
        }

        if (!isGrounded)
        {
            if (Input.GetButton("Horizontal"))
            {
                horizontalInput = Input.GetAxisRaw("Horizontal");

                rb.velocity = new Vector2(speed * horizontalInput * airDragMultiplier * accelerationMultiplier, rb.velocity.y);
                //rb.AddForce(new Vector2(horizontalInput, 0) * speed * airDragMultiplier, ForceMode2D.Force);
            }

            if (!Input.GetButton("Horizontal"))
            {
                rb.velocity = new Vector2(rb.velocity.x * deccelerationMultiplier, rb.velocity.y);
            }

            ChangeAnimationState(playerJump);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            print("jump input");
            Jump();
        }

        accelerationMultiplier = acceleration.Evaluate(timeSinceAccelerated);
        deccelerationMultiplier = decceleration.Evaluate(timeSinceDeccelerated);
    }

    void Jump()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        //rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        rb.AddForce(new Vector2(horizontalInput, Vector2.up.y) * jumpForce, ForceMode2D.Impulse);
    }

    void UpdateGraphicLookingDir()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        if (rotation_z >= -90 && rotation_z <= 90)
        {
            playerSprite.flipX = false;
        }
        else
        {
            playerSprite.flipX = true;
        }
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
        if (currentAnimation == newAnimation) return;

        anim.Play(newAnimation);

        currentAnimation = newAnimation;
    }
}
