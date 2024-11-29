using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;
    public float jumpPower = 5f;
    public bool isJump = false;

    public float dashTime;
    public bool isDash = false;
    public Vector2 tmpDir = Vector2.right;  // 기본 방향은 오른쪽
    public float maxDashTime = 0.5f;  // 최대 대시 시간 설정

    private Rigidbody2D rigidbody2D;
    private Animator animator;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Move();
        Jump();

        if (Input.GetKeyDown(KeyCode.Q) && !isDash)
        {
            StartDash();
        }

        if (isDash)
        {
            PlayerDash();
        }
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody2D.velocity = new Vector2(-speed, rigidbody2D.velocity.y);
            animator.SetBool("Run", true);
            animator.SetBool("Idle", false);
            transform.localScale = new Vector3(-1, 1, 1);

            tmpDir = Vector2.left; 
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
            animator.SetBool("Run", true);
            animator.SetBool("Idle", false);
            transform.localScale = new Vector3(1, 1, 1);

            tmpDir = Vector2.right;
        }
        else
        {
            animator.SetBool("Run", false);
            animator.SetBool("Idle", true);
            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
        }
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            isJump = true;
            animator.SetBool("Run", false);
            animator.SetBool("Jump", true);
            animator.SetBool("Idle", false);
            rigidbody2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    private void StartDash()
    {
        dashTime = 0;
        isDash = true;
    }

    public void PlayerDash()
    {
        dashTime += Time.deltaTime;

        rigidbody2D.velocity = tmpDir.normalized * (speed * 2);

        if (dashTime >= maxDashTime)
        {
            dashTime = 0;
            isDash = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJump = false;
            animator.SetBool("Jump", false);
        }
    }
}
