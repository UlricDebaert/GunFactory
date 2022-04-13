using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    public Rigidbody2D rb;

    void Start()
    {
        
    }


    void Update()
    {
        CheckInput();
    }


    void CheckInput()
    {
        if (Input.GetButton("Horizontal"))
        {
            rb.velocity = new Vector2(speed,rb.velocity.y);
        }
    }
}
