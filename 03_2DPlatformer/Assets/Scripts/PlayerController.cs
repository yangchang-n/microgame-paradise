using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRb;
    int playerHealth;

    float jumpForce;
    bool jumpActivated;
    bool isGround;

    float moveForce;
    float horizontalInput;

    bool isAttackCasting;
    float attackCoolTime;
    float attackCoolTimeElapsed;
    float attackCastTime;
    float attackCastTimeElapsed;

    bool isDead;
    float deathDelay;
    float deathElapsedTime;

    public GameObject playerSprite;
    public GameObject attackPoint;
    public GameObject playerAttack;
    public Animator animator;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerHealth = 5;

        jumpForce = 50.0f;
        jumpActivated = false;
        isGround = true;

        moveForce = 125.0f;
        horizontalInput = 0;

        isAttackCasting = false;
        attackCoolTime = 1f;
        attackCoolTimeElapsed = attackCoolTime;
        attackCastTime = 0.3f;
        attackCastTimeElapsed = 0;

        isDead = false;
        deathDelay = 1f;
        deathElapsedTime = 0;

        animator = playerSprite.GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead)
        {
            deathElapsedTime += Time.deltaTime;
            if (deathElapsedTime > deathDelay) GameManager.instance.SetGameOver(true);
            return;
        }

        attackCoolTimeElapsed += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGround)
            {
                jumpActivated = true;
                animator.SetTrigger("IsJumping");
            }
        }

        horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput != 0)
        {
            animator.SetBool("IsWalking", true);
            if (horizontalInput > 0) transform.rotation = new Quaternion(0, 0, 0, 0);
            else transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else animator.SetBool("IsWalking", false);

        if (Input.GetKeyDown(KeyCode.J) && attackCoolTime < attackCoolTimeElapsed)
        {
            isAttackCasting = true;
            attackCoolTimeElapsed = 0;
            animator.SetTrigger("IsAttacking");
        }

        if (isAttackCasting)
        {
            attackCastTimeElapsed += Time.deltaTime;
            if (attackCastTime < attackCastTimeElapsed)
            {
                Instantiate(playerAttack, attackPoint.transform.position, transform.rotation);
                attackCastTimeElapsed = 0;
                isAttackCasting = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (jumpActivated)
        {
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpActivated = false;
        }

        if (horizontalInput != 0)
        {
            playerRb.AddForce(new Vector2(horizontalInput, 0) * moveForce);
        }
    }

    private void LateUpdate()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, -10);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && playerRb.velocity.y == 0)
        {
            isGround = true;
            animator.SetBool("IsInAir", false);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && playerRb.velocity.y == 0)
        {
            isGround = true;
            animator.SetBool("IsInAir", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
            animator.SetBool("IsInAir", true);
        }
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            playerRb.velocity = Vector2.zero;
            animator.SetTrigger("IsDead");
            isDead = true;
        }
        GameObject.Find("HPText").GetComponent<UIManager>().UpdateHPUI();
    }

    public int GetHP()
    {
        return playerHealth;
    }
}
