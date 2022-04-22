using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public LayerMask hittableLayers;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.layer == hittableLayers)
        //{
        //    print("hit" + collision.gameObject.layer);
        //    DestroyItself();
        //}
        if ((hittableLayers & (1 << collision.transform.gameObject.layer)) > 0)
        {
            DestroyItself();
        }
    }

    public void DestroyItself()
    {
        Destroy(gameObject);
    }
}
