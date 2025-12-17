using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    int maxCount = 10;
    int maxCooltime = 85;
    int count;
    int cooltime;

    void Start()
    {
        count = 0;
        cooltime = maxCooltime;
    }

    void Update()
    {
        if (cooltime < maxCooltime) cooltime++;

        if (cooltime == maxCooltime && count < maxCount)
        {
            Vector2 tempPosition = transform.position;
            tempPosition.x = Random.Range(-2.0f, 2.0f);
            Instantiate(enemyPrefab, tempPosition, transform.rotation);
            count++;
            cooltime = 0;
        }
    }
}
