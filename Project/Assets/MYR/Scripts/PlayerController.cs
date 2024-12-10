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
    public Vector2 tmpDir = Vector2.right;  // �⺻ ������ ������
    public float maxDashTime = 0.01f;  // �ִ� ��� �ð� ����

    [HideInInspector] public Rigidbody2D rigidbody2D;
    [HideInInspector] public Animator animator;

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
/*        HandleJumping();  // ���� ���� ó��*/
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-speed, 0) * Time.deltaTime;
            animator.SetBool("Run", true);
            animator.SetBool("Idle", false);
            transform.localScale = new Vector3(-1, 1, 1);

            tmpDir = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
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

/*    private void HandleJumping()
    {
        // ���� ���� ���� �ٴ� ����
        if (rigidbody2D.velocity.y <= 0)  // �÷��̾ ������ �� (y �ӵ��� ������ ��)
        {
            Debug.DrawRay(rigidbody2D.position, Vector2.down, new Color(0, 1, 0)); // Ray�� �׸��� (����׿�)

            // �ٴڿ� Raycast�� ���
            RaycastHit2D rayHit = Physics2D.Raycast(rigidbody2D.position, Vector2.down, 1f, LayerMask.GetMask("Platform"));

            // �ٴڰ� �浹�ϸ� isJump�� false�� ����
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f) // �ٴڰ� 0.5 ���Ϸ� ���������
                {
                    if (isJump) // ���� ���¿����� �ٴڿ� ����� ��
                    {
                        isJump = false;
                        animator.SetBool("Jump", false); // �ִϸ��̼ǿ��� ���� ���� ����
                        animator.SetBool("Idle", true);  // ��� ���·� ����
                    }
                }
            }
        }
    }*/
}
