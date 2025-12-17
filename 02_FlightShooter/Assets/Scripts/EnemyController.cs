using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyController : MonoBehaviour
{
    Rigidbody2D EnemyRb;
    Vector2 moveForce;

    GameObject playerInfo;

    int maxCooltime = 50;
    int cooltime;

    public GameObject enemyBulletPrefab;

    void Start()
    {
        EnemyRb = GetComponent<Rigidbody2D>();
        moveForce = new Vector2(0, -10);
        playerInfo = GameObject.Find("Player");
        cooltime = 0;
    }

    void Update()
    {
        bool isGameOver = GameManager.instance.GetIsGameOver();
        if (isGameOver) return;

        if (cooltime < maxCooltime) cooltime++;

        if (transform.position.y > 4.5) EnemyRb.AddForce(moveForce);
        else if (cooltime == maxCooltime)
        {
            Vector2 bulletMoveDirection = (playerInfo.transform.position - transform.position);
            float bulletRotationAngle = Mathf.Atan2(bulletMoveDirection.y, bulletMoveDirection.x) * Mathf.Rad2Deg;
            Quaternion bulletRotation = Quaternion.Euler(0f, 0f, bulletRotationAngle - 90f); // -90¡Æ º¸Á¤

            Instantiate(enemyBulletPrefab, transform.position, bulletRotation);
            cooltime = 0;
        }
    }
}
