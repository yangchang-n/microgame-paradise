using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rigid;

    bool isMoving;
    float moveConstant;
    float jumpConstant;
    float jumpElapsedTime;
    float dragDownConstant;

    public Transform bodyPos;
    public Transform camPos;
    public float mouseX;
    public float mouseY;
    public float camRotSpeed;
    public Camera cam;
    public Animator animator;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();

        isMoving = false;
        moveConstant = 250;
        jumpConstant = 15;
        jumpElapsedTime = 1;
        dragDownConstant = 20;

        camRotSpeed = 1;
        cam = Camera.main;
    }

    private void Update()
    {
        if (GameManager.instance.isPaused) return;

        isMoving = false;
        jumpElapsedTime += Time.deltaTime;

        mouseX += Input.GetAxis("Mouse X");
        mouseY -= Input.GetAxis("Mouse Y");
        bodyPos.transform.rotation = Quaternion.Euler(mouseY * camRotSpeed, mouseX * camRotSpeed, 0);
        cam.transform.rotation = Quaternion.Euler(mouseY * camRotSpeed, mouseX * camRotSpeed, 0);
        cam.transform.position = camPos.position;
        transform.rotation = Quaternion.Euler(0, mouseX * camRotSpeed, 0);

        if (transform.position.y < -20)
        {
            transform.position = GameManager.instance.playerRespawnPoint;
            rigid.velocity = Vector3.zero;
        }

        Vector3 moveVector = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveVector.z += 1;
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveVector.z -= 1;
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveVector.x += 1;
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveVector.x -= 1;
            isMoving = true;
        }

        if (isMoving)
        {
            moveVector = transform.TransformDirection(moveVector);
            moveVector.Normalize();
            rigid.AddForce(moveVector * moveConstant);
            animator.SetBool("IsRunning", true);
        }
        else animator.SetBool("IsRunning", false);

        if (Input.GetKeyDown(KeyCode.Space) && jumpElapsedTime > 1)
        {
            rigid.AddForce(Vector3.up * jumpConstant, ForceMode.Impulse);
            jumpElapsedTime = 0;
            animator.SetTrigger("IsJumping");
        }

        rigid.AddForce(Vector3.down * dragDownConstant);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("IsGround", true);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("IsGround", true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("IsGround", false);
        }
    }
}
