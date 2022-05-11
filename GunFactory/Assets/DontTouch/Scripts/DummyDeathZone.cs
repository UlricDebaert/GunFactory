using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyDeathZone : MonoBehaviour
{
    public LayerMask dummyLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((dummyLayer & (1 << collision.transform.gameObject.layer)) > 0)
        {
            Destroy(collision.gameObject);
        }
    }
}
