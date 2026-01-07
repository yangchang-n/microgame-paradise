using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingIceSpawner : MonoBehaviour
{
    public GameObject fallingIce;
    Vector3 spawnerPosition;
    Vector3 spawnPos;

    float spawnCoolTime;
    float spawnElapsedTime;

    private void Start()
    {
        spawnerPosition = transform.position;

        spawnCoolTime = 0.3f;
        spawnElapsedTime = 0;
    }

    private void Update()
    {
        spawnElapsedTime += Time.deltaTime;
        if (spawnCoolTime < spawnElapsedTime)
        {
            spawnPos.x = spawnerPosition.x + Random.Range(-5f, 5f);
            spawnPos.y = spawnerPosition.y;
            spawnPos.z = spawnerPosition.z + Random.Range(-8f, 8f);
            Instantiate(fallingIce, spawnPos, transform.rotation);
            spawnElapsedTime = 0;
        }
    }
}
