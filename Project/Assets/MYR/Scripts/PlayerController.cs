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
    public float maxDashTime = 0.01f;  // 최대 대시 시간 설정

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
/*        HandleJumping();  // 점프 상태 처리*/
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
        // 낙하 중일 때만 바닥 감지
        if (rigidbody2D.velocity.y <= 0)  // 플레이어가 떨어질 때 (y 속도가 음수일 때)
        {
            Debug.DrawRay(rigidbody2D.position, Vector2.down, new Color(0, 1, 0)); // Ray를 그리기 (디버그용)

            // 바닥에 Raycast를 쏘기
            RaycastHit2D rayHit = Physics2D.Raycast(rigidbody2D.position, Vector2.down, 1f, LayerMask.GetMask("Platform"));

            // 바닥과 충돌하면 isJump를 false로 설정
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f) // 바닥과 0.5 이하로 가까워지면
                {
                    if (isJump) // 점프 상태에서만 바닥에 닿았을 때
                    {
                        isJump = false;
                        animator.SetBool("Jump", false); // 애니메이션에서 점프 상태 해제
                        animator.SetBool("Idle", true);  // 대기 상태로 변경
                    }
                }
            }
        }
    }*/
}
