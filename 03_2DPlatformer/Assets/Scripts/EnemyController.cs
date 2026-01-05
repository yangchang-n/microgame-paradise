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

    bool isAttackCasting;
    float attackCoolTime;
    float attackCoolTimeElapsed;
    float attackCastTime;
    float attackCastTimeElapsed;

    bool isDead;
    float deathDelay;
    float deathElapsedTime;

    public GameObject enemySprite;
    public GameObject enemyAttackPoint;
    public GameObject enemyAttack;
    Transform target;

    void Start()
    {
        enemyHealth = 3;
        enemyAnimator = enemySprite.GetComponent<Animator>();
        enemySightRange = 5;

        isAttackCasting = false;
        attackCoolTime = 2f;
        attackCoolTimeElapsed = attackCoolTime;
        attackCastTime = 0.5f;
        attackCastTimeElapsed = 0;

        isDead = false;
        deathDelay = 0.8f;
        deathElapsedTime = 0;
    }

    void Update()
    {
        attackCoolTimeElapsed += Time.deltaTime;

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

        if (isAttackCasting)
        {
            attackCastTimeElapsed += Time.deltaTime;
            if (attackCastTime < attackCastTimeElapsed)
            {
                Instantiate(enemyAttack, enemyAttackPoint.transform.position, transform.rotation);
                attackCastTimeElapsed = 0;
                isAttackCasting = false;
            }
        }
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
            if (attackCoolTime < attackCoolTimeElapsed)
            {
                isAttackCasting = true;
                attackCoolTimeElapsed = 0;
                enemyAnimator.SetTrigger("IsAttacking");
            }
        }
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
