using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoRockSpawner : MonoBehaviour
{
    public GameObject volcanoRock;
    Transform spawnerTransform;
    Vector3 spawnPosition;

    float spawnCoolTime;
    float spawnElapsedTime;

    private void Start()
    {
        spawnerTransform = transform;
        spawnPosition = transform.position;

        spawnCoolTime = 5.5f;
        spawnElapsedTime = Random.Range(3.5f, 5f);
    }

    private void Update()
    {
        spawnElapsedTime += Time.deltaTime;
        if (spawnCoolTime < spawnElapsedTime)
        {
            spawnPosition.x += Random.Range(-3f, 3f);
            Instantiate(volcanoRock, spawnPosition, transform.rotation);
            spawnElapsedTime = Random.Range(0f, 2.5f);
        }
    }
}
