using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;
    public float jumpPower = 5f;
    public bool isJump = false;

    public float dashTime = 0;
    public bool isDash = false;
    public Vector2 tmpDir = Vector2.right;  // 기본 방향은 오른쪽
    public float maxDashTime = 0.5f;  // 최대 대시 시간 설정

    [HideInInspector]public Rigidbody2D rigidbody2D;
    [HideInInspector]public Animator animator;

    public Inventory inventory;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Move();
        dashTime += Time.deltaTime;

    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
/*            rigidbody2D.velocity = new Vector2(-speed, rigidbody2D.velocity.y);*/
            transform.position += new Vector3(-speed, 0) * Time.deltaTime;
            animator.SetBool("Run", true);
            animator.SetBool("Idle", false);
            transform.localScale = new Vector3(-1, 1, 1);

            tmpDir = Vector2.left; 
        }
        else if (Input.GetKey(KeyCode.D))
        {
            /*            rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);*/
            transform.position += new Vector3(speed, 0) * Time.deltaTime;
            animator.SetBool("Run", true);
            animator.SetBool("Idle", false);
            transform.localScale = new Vector3(1, 1, 1);

            tmpDir = Vector2.right;
        }
        else
        {
            animator.SetBool("Run", false);
            animator.SetBool("Idle", true);
/*            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);*/
        }
    }

    public bool Jump()
    {
        if (!isJump)
        {
            isJump = true;
            animator.SetBool("Run", false);
            animator.SetBool("Jump", true);
            animator.SetBool("Idle", false);
            rigidbody2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            return true;
        }
        return false;
    }

    public bool PlayerDash()
    {
        if (dashTime >= maxDashTime)
        {
            rigidbody2D.AddForce(tmpDir.normalized * (speed * 3), ForceMode2D.Impulse);
            dashTime = 0;
            return true;
        }
        return false;
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
