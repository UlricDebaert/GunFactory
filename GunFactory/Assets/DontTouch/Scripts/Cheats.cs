using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    public Transform[] floorsTransforms;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gameObject.transform.position = floorsTransforms[0].position;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gameObject.transform.position = floorsTransforms[1].position;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            gameObject.transform.position = floorsTransforms[2].position;
        }
    }
}
