using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public LayerMask wallLayers;
    public LayerMask dummyLayers;
    public int bulletDamage;


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
            collision.GetComponent<DummyHP>().TakeDamage(bulletDamage);
            DestroyItself();
        }
    }

    public void DestroyItself()
    {
        Destroy(gameObject);
    }
}
