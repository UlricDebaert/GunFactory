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

    public float destroyAnimDelay;

    private void Start()
    {
        targetPenetrated = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.layer == hittableLayers)
        //{
        //    print("hit" + collision.gameObject.layer);
        //    DestroyItself();
        //}
        if ((wallLayers & (1 << collision.transform.gameObject.layer)) > 0)
        {
            DestroyItself();
        }
        if ((dummyLayers & (1 << collision.transform.gameObject.layer)) > 0)
        {
            collision.GetComponent<DummyHP>().TakeDamage(Mathf.RoundToInt(bulletDamage/(1+targetPenetrated*penetrationMultiplier)));

            if (targetPenetrated >= maxTargetsPenetration) DestroyItself();
            else targetPenetrated++;
        }
    }

    public void DestroyItself()
    {
        Destroy(gameObject, destroyAnimDelay);
    }
}
