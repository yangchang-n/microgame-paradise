using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float lifetime;

    void Start()
    {
        lifetime = 0.5f;
        Destroy(gameObject, lifetime);
    }

    private void FixedUpdate()
    {
        Vector2 tempPos = GameObject.Find("PlayerAttackPoint").transform.position;
        transform.position = tempPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
