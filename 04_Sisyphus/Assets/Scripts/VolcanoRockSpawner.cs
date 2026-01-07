using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoRockSpawner : MonoBehaviour
{
    public GameObject volcanoRock;
    Vector3 spawnerPosition;
    Vector3 spawnPos;

    float spawnCoolTime;
    float spawnElapsedTime;

    private void Start()
    {
        spawnerPosition = transform.position;

        spawnCoolTime = 5.5f;
        spawnElapsedTime = Random.Range(3.5f, 5f);
    }

    private void Update()
    {
        spawnElapsedTime += Time.deltaTime;
        if (spawnCoolTime < spawnElapsedTime)
        {
            spawnPos.x = spawnerPosition.x + Random.Range(-3f, 3f);
            spawnPos.y = spawnerPosition.y;
            spawnPos.z = spawnerPosition.z;
            Instantiate(volcanoRock, spawnPos, transform.rotation);
            spawnElapsedTime = Random.Range(0f, 2.5f);
        }
    }
}
