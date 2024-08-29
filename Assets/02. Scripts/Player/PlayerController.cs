using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class PlayerController : MonoBehaviour
{
    public PlayerStats stats;
    public PlayerInputManager inputManager;

    /*
    public UIButton jumpButton;
    public UIButton basicAttackButton;
    public UIJoystickHandle joystick;
    public float joystickSensitivity = 500f;
    */
    
    public float attackTime = 1f;
    
    /*
    private bool isJumpButtonEnabled = true;
    private BoxCollider jumpButtonCollider;
    
    private BoxCollider basicAttackButtonCollider;
    private bool isBasicAttackButtonEnabled = true;
    */
    
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    
    private float moveHorizontal;
    
    [Header("# 플레이어 상태")]
    [SerializeField] private bool isFacingRight = true;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private bool isJumping = false;
    [SerializeField] private bool isAttacking = false;

    private void Awake() // 컴포넌트 초기화 
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        stats = GetComponent<PlayerStats>();
    }
    
    private void Start()
    {
        // 입력 이벤트 연결 (점프, 공격)
        if (inputManager != null)
        {
            inputManager.OnJumpButtonClicked += TryJump;
            inputManager.OnBasicAttackButtonClicked += Attack;
        }
        else 
        {
            Debug.LogError("PlayerInputManager is not assigned to the PlayerController!");
            return;
        }
        
        /*
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
        
        if (basicAttackButton != null)
        {
            basicAttackButtonCollider = basicAttackButton.GetComponent<BoxCollider>();
            UIEventListener.Get(basicAttackButton.gameObject).onClick += OnBasicAttackButtonClicked;
            SetBasicAttackButtonState(true);
        }
        else
        {
            Debug.LogError("Basic attack button is not assigned to the PlayerController!");
        }
        */
    }

    void Update()
    {
        // 입력 감지
        // moveHorizontal = Input.GetAxisRaw("Horizontal");
        
        // 조이스틱 입력 사용
        // moveHorizontal = moveHorizontal = joystick.GetHorizontalValue();
        
        // 조이스틱 입력 사용 및 감도 적용
        // moveHorizontal = joystick.GetHorizontalValue() * joystickSensitivity;
        moveHorizontal = inputManager.GetHorizontalInput();
        
        /*
        // 점프 입력 감지
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            TryJump();
        }
        
        // 공격 입력 감지 + 점프 상태 아닐 때 
        if (Input.GetKeyDown(KeyCode.Z) && !isJumping)
        {
            Attack();
        }
        */
        
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
        Vector2 movement = new Vector2(moveHorizontal * stats.moveSpeed, rb.velocity.y); // 이동 벡터 계산
        rb.velocity = movement; // Rigidbody2D의 속도 설정
    }

    // 점프 시도
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
        isJumping = true;
        // SetJumpButtonState(false); // 점프 후 버튼 비활성화
        inputManager.SetButtonState(inputManager.jumpButton, false);
        animator.SetBool("IsJumping", true); // 점프 애니메이션 시작
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
        
        // 점프 상태 업데이트
        if (!isGrounded && rb.velocity.y > 0)
        {
            animator.SetBool("IsJumping", true);
        }
        else if (isGrounded)
        {
            animator.SetBool("IsJumping", false);
        }
    }
    
    // 바닥에 닿았을 때, 점프 가능 상태로 변경
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false; 
            // SetJumpButtonState(true); // 점프 버튼 활성화 
            inputManager.SetButtonState(inputManager.jumpButton, true);
            animator.SetBool("IsJumping", false); // 점프 애니메이션 종료
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
    
    /*
    // 점프 버튼 활성화 상태 설정 
    private void SetJumpButtonState(bool enabled)
    {
        if (jumpButton != null)
        {
            isJumpButtonEnabled = enabled;
            
            // 대신 콜라이더를 통해 상호작용을 제어
            if (jumpButtonCollider != null)
            {
                jumpButtonCollider.enabled = enabled;
            }
        }
    }
    */

    /*
    // 점프 버튼 클릭 이벤트 핸들러 
    private void OnJumpButtonClicked(GameObject go)
    {
        TryJump();
    }
    */

    private void Attack()
    {
        // if (!isAttacking && isBasicAttackButtonEnabled)
        if (!isAttacking && inputManager.basicAttackButton.isEnabled)
        {
            Debug.Log("Attack method called");
            StartCoroutine(PerformAttack());
        }
    }
    
    /*
    // 기본 공격 버튼 활성화 상태 설정 
    private void SetBasicAttackButtonState(bool enabled)
    {
        if (basicAttackButton != null)
        {
            isBasicAttackButtonEnabled = enabled;

            if (basicAttackButtonCollider != null)
            {
                basicAttackButtonCollider.enabled = enabled;
            }
        }
    }
    */
    
    /*
    // 기본 공격 버튼 클릭 이벤트 핸들러 
    private void OnBasicAttackButtonClicked(GameObject go)
    {
        Attack();
    }
    */
    
    // 공격 코루틴
    private IEnumerator PerformAttack()
    {
        isAttacking = true;
        // SetBasicAttackButtonState(false); // 공격 시작 시 버튼 비활성화
        inputManager.SetButtonState(inputManager.basicAttackButton, false);
        animator.SetTrigger("Attack");

        // 공격 애니메이션 길이만큼 대기
        yield return new WaitForSeconds(attackTime); // 0.5f 

        isAttacking = false;
        // SetBasicAttackButtonState(true); // 공격 종료 후 버튼 다시 활성화
        inputManager.SetButtonState(inputManager.basicAttackButton, true);
    }
}
