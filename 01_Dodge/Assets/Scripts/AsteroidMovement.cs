using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    Vector2 asteroidPos;
    float x_direction;
    float y_direction;
    float speed;

    private void InitializeAsteroid()
    {
        int spawnDirection = Random.Range(1, 5);
        if      (spawnDirection == 1) asteroidPos = new Vector2(Random.Range(-10.0f, 10.0f),  6);
        else if (spawnDirection == 2) asteroidPos = new Vector2(Random.Range(-10.0f, 10.0f), -6);
        else if (spawnDirection == 3) asteroidPos = new Vector2(-10, Random.Range(-6.0f, 6.0f));
        else if (spawnDirection == 4) asteroidPos = new Vector2( 10, Random.Range(-6.0f, 6.0f));

        Vector2 rocketPos = GameObject.Find("rocket").transform.position;
        float x_diff = rocketPos.x - asteroidPos.x + Random.Range(-3.0f, 3.0f);
        float y_diff = rocketPos.y - asteroidPos.y + Random.Range(-3.0f, 3.0f);
        float sqrt_xy = Mathf.Sqrt(x_diff * x_diff + y_diff * y_diff);

        transform.position = asteroidPos;
        x_direction = x_diff / sqrt_xy;
        y_direction = y_diff / sqrt_xy;
        speed = Random.Range(0.05f, 0.15f);
    }

    void Start()
    {
        InitializeAsteroid();
    }

    void Update()
    {
        asteroidPos.x += x_direction * speed;
        asteroidPos.y += y_direction * speed;
        transform.position = asteroidPos;

        if (asteroidPos.x > 10 || asteroidPos.x < -10) InitializeAsteroid();
        if (asteroidPos.y >  6 || asteroidPos.y <  -6) InitializeAsteroid();
    }
}