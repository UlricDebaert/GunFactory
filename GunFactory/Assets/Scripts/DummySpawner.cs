using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummySpawner : MonoBehaviour
{
    public GameObject entitySpawnedPrefab;
    public int totalEntitySpawned;
    public float spawnDelay;


    void Start()
    {
        StartCoroutine("StartSpawn");
    }

    IEnumerator StartSpawn()
    {
        for (int i = 0; i < totalEntitySpawned; i++)
        {
            Instantiate(entitySpawnedPrefab, gameObject.transform.position, transform.rotation);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
