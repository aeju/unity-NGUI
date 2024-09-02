using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class PlayerController : MonoBehaviour
{
    public PlayerStats stats;
    
    public float attackTime = 1f;
    
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    
    private float moveHorizontal;
    
    [Header("# 플레이어 상태")]
    [SerializeField] private bool isFacingRight = true;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private bool isJumping = false;
    [SerializeField] private bool isAttacking = false;

    [Header("# 플레이어 효과음")]
    [SerializeField] private string jumpSoundName = "Jump";
    [SerializeField] private string attackSoundName = "Attack"; 
    
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
        if (PlayerInputManager.Instance != null)
        {
            PlayerInputManager.Instance.OnJumpButtonClicked += TryJump;
            PlayerInputManager.Instance.OnBasicAttackButtonClicked += Attack;
        }
        else 
        {
            Debug.LogError("PlayerInputManager is not assigned to the PlayerController!");
            return;
        }
    }

    public float GetHorizontalInput()
    {
        float joystickInput = PlayerInputManager.Instance.joystick.GetHorizontalValue() * PlayerInputManager.Instance.joystickSensitivity;

#if UNITY_EDITOR
        // 에디터에서는 화살표 키 입력도 처리
        float keyboardInput = Input.GetAxisRaw("Horizontal");  // GetAxis 대신 GetAxisRaw 사용
        
        // 조이스틱 입력이 있으면 조이스틱 입력을 사용, 없으면 키보드 입력을 사용
        if (Mathf.Abs(joystickInput) > 0.01f)  // 작은 임계값을 사용하여 조이스틱의 미세한 움직임 무시
        {
            return joystickInput;
        }
        else
        {
            return keyboardInput;  // 키보드 입력에는 sensitivity를 적용하지 않음
        }
#else
        return joystickInput;
#endif
    }
    
    void Update()
    {
        if (!isJumping)
        {
            // 수평 이동 입력 처리
            moveHorizontal = GetHorizontalInput();
        }
        else
        {
            moveHorizontal = 0;
        }
        
        
#if UNITY_EDITOR // 에디터 : 왼쪽 alt - 점프 / z - 기본 공격
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
#endif
        
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
        PlayerInputManager.Instance.SetButtonState(PlayerInputManager.Instance.jumpButton, false);
        animator.SetBool("IsJumping", true); // 점프 애니메이션 시작
        SoundManager.Instance.PlaySFX(jumpSoundName);
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
            PlayerInputManager.Instance.SetButtonState(PlayerInputManager.Instance.jumpButton, true);
            animator.SetBool("IsJumping", false); // 점프 애니메이션 종료
        }
    }
    
    private void Attack()
    {
        if (!isAttacking && PlayerInputManager.Instance.basicAttackButton.isEnabled)
        {
            Debug.Log("Attack method called");
            StartCoroutine(PerformAttack());
        }
    }

    // 공격 코루틴
    private IEnumerator PerformAttack()
    {
        isAttacking = true;
        PlayerInputManager.Instance.SetButtonState(PlayerInputManager.Instance.basicAttackButton, false);
        animator.SetTrigger("Attack");
        SoundManager.Instance.PlaySFX(attackSoundName);

        // 공격 애니메이션 길이만큼 대기
        yield return new WaitForSeconds(attackTime); // 0.5f 

        isAttacking = false;
        PlayerInputManager.Instance.SetButtonState(PlayerInputManager.Instance.basicAttackButton, true);
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
}
