using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    public Rigidbody2D rb;

    [Header("Ground Check")]
    public Transform groundCheckPos;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    bool isGrounded;

    [Header("Graphics")]
    public SpriteRenderer playerSprite;

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
        if (Input.GetButton("Horizontal"))
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");

            rb.velocity = new Vector2(speed * horizontalInput, rb.velocity.y);
        }

        if(!Input.GetButton("Horizontal") && isGrounded)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (horizontalInput>0)
        {
            playerSprite.flipX = false;
        }
        if (horizontalInput<0)
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
}
