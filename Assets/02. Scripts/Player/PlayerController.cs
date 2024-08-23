using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 2f; // 이동 속도
    private float moveHorizontal;
    
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isFacingRight = true;
    
    public float jumpForce = 10f; // 점프 힘
    private bool isJumping = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 입력 감지
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        
        // 점프 입력 감지
        if (Input.GetKeyDown(KeyCode.LeftAlt) && !isJumping)
        // if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            // isJumping = true;
            Jump();
        }
        
        // 방향 전환 체크
        if (moveHorizontal != 0)
        {
            CheckFlip(moveHorizontal > 0);
        }
        
        // 애니메이션 상태 업데이트
        UpdateAnimationState();
    }
    
    // 물리 기반 이동
    void FixedUpdate()
    {
        Move();
    }
    
    void Move()
    {
        // 이동 벡터 계산
        Vector2 movement = new Vector2(moveHorizontal * moveSpeed, rb.velocity.y);

        // Rigidbody2D의 속도 설정
        rb.velocity = movement;
    }
    
    void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }
    
    void CheckFlip(bool isMovingRight)
    {
        // 현재 방향과 이동 방향이 다르면 뒤집기
        if (isMovingRight != isFacingRight)
        {
            Flip();
        }
    }
    
    // 캐릭터 보는 방향 뒤집기
    void Flip()
    {
        isFacingRight = !isFacingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
    
    void UpdateAnimationState()
    {
        // 이동 중이면 walk 애니메이션, 그렇지 않으면 idle 애니메이션
        bool isWalking = Mathf.Abs(moveHorizontal) > 0.1f;
        animator.SetBool("IsWalking", isWalking);
    }
    
    // 바닥에 닿았을 때 점프 가능 상태로 변경
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
