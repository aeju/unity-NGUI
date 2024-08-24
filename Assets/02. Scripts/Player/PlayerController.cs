using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class PlayerController : MonoBehaviour
{
    public PlayerStats stats;
    public UIButton jumpButton;
    private bool isJumpButtonEnabled = true;
    private BoxCollider jumpButtonCollider;
    
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    
    private float moveHorizontal;
    
    [Header("# 플레이어 상태")]
    [SerializeField] private bool isFacingRight = true;
    [SerializeField] private bool isGrounded = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        stats = GetComponent<PlayerStats>();
    }
    
    private void Start()
    {
        // 점프 버튼 초기화 및 이벤트 연결
        if (jumpButton != null)
        {
            jumpButtonCollider = jumpButton.GetComponent<BoxCollider>();
            UIEventListener.Get(jumpButton.gameObject).onClick += OnJumpButtonClicked;
            SetJumpButtonState(isGrounded);
        }
        else
        {
            Debug.LogError("Jump button is not assigned to the PlayerController!");
        }
    }

    void Update()
    {
        // 입력 감지
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        
        // 점프 입력 감지
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            TryJump();
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
        Vector2 movement = new Vector2(moveHorizontal * stats.moveSpeed, rb.velocity.y);

        // Rigidbody2D의 속도 설정
        rb.velocity = movement;
    }

    // 우선 점프 : 시도부터
    public void TryJump()
    {
        if (isGrounded)
        {
            Jump();
        }
    }
    
    // 실제 점프 수행 
    public void Jump()
    {
        rb.AddForce(new Vector2(0f, stats.jumpForce), ForceMode2D.Impulse);
        isGrounded = false;
        SetJumpButtonState(false); // 점프 후 버튼 비활성화
    }
    
    // 현재 방향과 이동 방향이 다르면, 캐릭터 보는 방향 뒤집기
    void CheckFlip(bool isMovingRight)
    {
        
        if (isMovingRight != isFacingRight)
        {
            isFacingRight = !isFacingRight;
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }
    
    void UpdateAnimationState()
    {
        // 이동 중이면 walk 애니메이션, 그렇지 않으면 idle 애니메이션
        bool isWalking = Mathf.Abs(moveHorizontal) > 0.1f;
        animator.SetBool("IsWalking", isWalking);
    }
    
    // 바닥에 닿았을 때, 점프 가능 상태로 변경
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            SetJumpButtonState(true); // 점프 버튼 활성화 
        }
    }
    
    /*
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            SetJumpButtonState(false);
        }
    }
    */
    
    // 점프 버튼 활성화 상태 설정 
    private void SetJumpButtonState(bool enabled)
    {
        if (jumpButton != null)
        {
            // 버튼의 시각적 상태는 변경하지 않음
            // jumpButton.isEnabled = enabled;
            
            
            isJumpButtonEnabled = enabled;
            
            // 대신 콜라이더를 통해 상호작용을 제어
            if (jumpButtonCollider != null)
            {
                jumpButtonCollider.enabled = enabled;
            }
        }
    }

    // 점프 버튼 클릭 이벤트 핸들러 
    private void OnJumpButtonClicked(GameObject go)
    {
        TryJump();
    }
}
