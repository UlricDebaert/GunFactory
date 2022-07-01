using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public LayerMask wallLayers;
    public LayerMask dummyLayers;

    [HideInInspector] public int bulletDamage;
    [HideInInspector] public int maxTargetsPenetration;
    [HideInInspector] int targetPenetrated;
    [HideInInspector] public float penetrationMultiplier;
    //[HideInInspector] public float knockbackOnTarget;
    [HideInInspector] public bool destroyed;

    Rigidbody2D rb;
    Animator anim;

    public float destroyAnimDelay;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        destroyed = false;
        anim.SetBool("destroyed", destroyed);

        targetPenetrated = 0;
    }

    private void Update()
    {
        if (destroyed)
        {
            rb.isKinematic = true;
            rb.velocity = new Vector2(0, 0); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((wallLayers & (1 << collision.transform.gameObject.layer)) > 0)
        {
            DestroyItself();
        }
        if ((dummyLayers & (1 << collision.transform.gameObject.layer)) > 0)
        {
            if (targetPenetrated >= maxTargetsPenetration) DestroyItself();
            else targetPenetrated++;
        }
    }

    public void DestroyItself()
    {
        destroyed = true;
        anim.SetBool("destroyed", destroyed);
        Destroy(gameObject, destroyAnimDelay);
    }
}
