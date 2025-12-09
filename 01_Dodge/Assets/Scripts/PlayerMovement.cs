using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Vector2 playerPos;
    Vector2 velocity;
    Vector2 acceleration;
    bool    ac_ver;
    bool    ac_hor;

    Vector2  posLimit = new Vector2(9, 5);
    float    power    = 0.001f;

    void Start()
    {
        playerPos = transform.position;
        velocity  = Vector2.zero;
        ac_ver    = false;
        ac_hor    = false;

        GameObject.Find("GameOverText").GetComponent<Text>().enabled = false;
    }

    private void CalculateAcceleration()
    {
        acceleration = Vector2.zero;

        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            ac_ver = true;
            acceleration.y = power;
        }
        else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            ac_ver = true;
            acceleration.y = -power;
        }
        else ac_ver = false;

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            ac_hor = true;
            acceleration.x = -power;
        }
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            ac_hor = true;
            acceleration.x = power;
        }
        else ac_hor = false;

        if (ac_ver && ac_hor)
        {
            acceleration.x /= 1.414f;
            acceleration.y /= 1.414f;
        }
    }

    private void LimitPosition()
    {
        if (playerPos.y > posLimit.y)
        {
            playerPos.y = posLimit.y;
            velocity.y = 0;
            acceleration.y = 0;
        }
        else if (playerPos.y < -posLimit.y)
        {
            playerPos.y = -posLimit.y;
            velocity.y = 0;
            acceleration.y = 0;
        }

        if (playerPos.x > posLimit.x)
        {
            playerPos.x = posLimit.x;
            velocity.x = 0;
            acceleration.x = 0;
        }
        else if (playerPos.x < -posLimit.x)
        {
            playerPos.x = -posLimit.x;
            velocity.x = 0;
            acceleration.x = 0;
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