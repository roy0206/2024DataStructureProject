using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;
    public float jumpPower = 5f;
    public bool isJump = false;

    Rigidbody2D rigidbody2D;
    Animator animator;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody2D.velocity = new Vector2(-speed, rigidbody2D.velocity.y);
            animator.SetBool("Run", true);
            animator.SetBool("Idle", false);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
            animator.SetBool("Run", true);
            animator.SetBool("Idle", false);
            transform.localScale = new Vector3(1, 1, 1);
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJump = false;
            animator.SetBool("Jump", false);
        }
    }
}
