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

    float attackCoolTime;
    float attackElapsedTime;

    bool isDead;
    float deathDelay;
    float deathElapsedTime;

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

        attackCoolTime = 0.8f;
        attackElapsedTime = attackCoolTime;

        isDead = false;
        deathDelay = 1f;
        deathElapsedTime = 0;

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead)
        {
            deathElapsedTime += Time.deltaTime;
            if (deathElapsedTime > deathDelay) GameManager.instance.SetGameOver(true);
            return;
        }

        attackElapsedTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGround)
            {
                jumpActivated = true;
                animator.SetTrigger("IsJumping");
            }
        }

        horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            animator.SetBool("IsWalking", true);
            if (horizontalInput > 0) transform.rotation = new Quaternion(0, 0, 0, 0);
            else transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else animator.SetBool("IsWalking", false);

        if (Input.GetKeyDown(KeyCode.J) && attackCoolTime < attackElapsedTime)
        {
            Instantiate(playerAttack, attackPoint.transform.position, transform.rotation);
            attackElapsedTime = 0;
            animator.SetTrigger("IsAttacking");
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
