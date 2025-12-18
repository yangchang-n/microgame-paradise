using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Vector2 playerPos;
    Vector2 velocity;
    Vector2 acceleration;

    Vector2 posLimit = new Vector2(9, 5);
    float   power    = 0.005f;

    void Start()
    {
        playerPos = transform.position;
        velocity  = Vector2.zero;

        GameObject.Find("GameOverText").GetComponent<Text>().enabled = false;
    }

    private void CalculateAcceleration()
    {
        acceleration = Vector2.zero;

        if (Input.GetKey(KeyCode.W)) acceleration.y = 1;
        else if (Input.GetKey(KeyCode.S)) acceleration.y = -1;

        if (Input.GetKey(KeyCode.A)) acceleration.x = -1;
        else if (Input.GetKey(KeyCode.D)) acceleration.x = 1;

        acceleration.Normalize();
        acceleration *= power;
    }

    private void LimitPosition()
    {
        if (playerPos.y > posLimit.y)
        {
            playerPos.y = posLimit.y;
            velocity.y = acceleration.y = 0;
        }
        else if (playerPos.y < -posLimit.y)
        {
            playerPos.y = -posLimit.y;
            velocity.y = acceleration.y = 0;
        }

        if (playerPos.x > posLimit.x)
        {
            playerPos.x = posLimit.x;
            velocity.x = acceleration.x = 0;
        }
        else if (playerPos.x < -posLimit.x)
        {
            playerPos.x = -posLimit.x;
            velocity.x = acceleration.x = 0;
        }
    }

    void Update()
    {
        CalculateAcceleration();

        velocity += acceleration;
        playerPos += velocity;

        LimitPosition();

        transform.position = playerPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject.Find("GameOverText").GetComponent<Text>().enabled = true;
    }
}