using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    Rigidbody2D enemyBulletRb;
    GameObject playerInfo;
    Vector2 moveDirection;

    void Start()
    {
        enemyBulletRb = GetComponent<Rigidbody2D>();
        playerInfo = GameObject.Find("Player");

        moveDirection = (playerInfo.transform.position - transform.position);
        moveDirection.Normalize();
        enemyBulletRb.AddForce(moveDirection * 150);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall") Destroy(gameObject);

        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().DecreaseHp();
            Destroy(gameObject);
        }
    }
}
