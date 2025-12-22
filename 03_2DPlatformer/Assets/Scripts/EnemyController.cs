using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyController : MonoBehaviour
{
    public int enemyHealth;
    Animator enemyAnimator;
    public LayerMask playerLayermask;
    float enemySightRange;

    float attackCooltime;
    float attackElapsedTime;

    bool isDead;
    float deathDelay;
    float deathElapsedTime;

    public GameObject enemyAttackPoint;
    public GameObject enemyAttack;
    Transform target;

    void Start()
    {
        enemyHealth = 3;
        enemyAnimator = GetComponent<Animator>();
        enemySightRange = 2;

        attackCooltime = 2;
        attackElapsedTime = attackCooltime;

        isDead = false;
        deathDelay = 0.8f;
        deathElapsedTime = 0;
    }

    void Update()
    {
        attackElapsedTime += Time.deltaTime;

        if (isDead)
        {
            deathElapsedTime += Time.deltaTime;
            if (deathElapsedTime > deathDelay)
            {
                GameManager.instance.EnemyKill();
                Destroy(gameObject);
            }
        }
        else DetectPlayer();
    }

    void DetectPlayer()
    {
        Collider2D player;
        player = Physics2D.OverlapCircle(transform.position, enemySightRange, playerLayermask);

        if (player != null)
        {
            target = player.transform;
            if (target.position.x <= transform.position.x) transform.rotation = new Quaternion(0, 0, 0, 0);
            else transform.rotation = new Quaternion(0, 180, 0, 0);
            if (attackElapsedTime > attackCooltime) AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        Instantiate(enemyAttack, enemyAttackPoint.transform.position, transform.rotation);
        attackElapsedTime = 0;
        enemyAnimator.SetTrigger("IsAttacking");
    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            enemyAnimator.SetTrigger("IsDead");
            isDead = true;
        }
    }
}
