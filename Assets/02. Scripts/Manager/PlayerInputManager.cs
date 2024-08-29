using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInputManager : Singleton<PlayerInputManager>
{
    [System.Serializable]
    public class InputButton
    {
        public UIButton button; // 버튼
        public BoxCollider buttonCollider; // 버튼 boxCollider 컴포넌트 
        public bool isEnabled = true; // 버튼 활성화 상태 
    }

    public PlayerController playerController; 
    
    public InputButton jumpButton; 
    public InputButton basicAttackButton;
    /* 스킬 버튼 : 시간 된다면 구현 
    public InputButton firstSkillButton;
    public InputButton secondSkillButton;
    public InputButton thirdSkillButton;
    public InputButton fourthSkillButton;
    */
    
    public UIJoystickHandle joystick;
    public float joystickSensitivity = 500f;
    
    // 버튼 클릭 이벤트 
    public event Action OnJumpButtonClicked;
    public event Action OnBasicAttackButtonClicked;
    /* 
    public event Action OnFirstSkillButtonClicked;
    public event Action OnSecondSkillButtonClicked;
    public event Action OnThirdSkillButtonClicked;
    public event Action OnFourthSkillButtonClicked;
    */
    
    private void Start()
    {
        // 버튼 초기화 (점프, 기본 공격)
        InitializeButton(jumpButton, OnJumpButtonPressed);
        InitializeButton(basicAttackButton, OnBasicAttackButtonPressed);
        
        /*
        InitializeButton(firstSkillButton, OnFirstSkillButtonPressed);
        InitializeButton(secondSkillButton, OnSecondSkillButtonPressed);
        InitializeButton(thirdSkillButton, OnThirdSkillButtonPressed);
        InitializeButton(fourthSkillButton, OnFourthSkillButtonPressed);
        */
    }

    // 버튼 초기화 메서드
    private void InitializeButton(InputButton inputButton, Action onClickAction)
    {
        if (inputButton.button != null)
        {
            inputButton.buttonCollider = inputButton.button.GetComponent<BoxCollider>();
            UIEventListener.Get(inputButton.button.gameObject).onClick += (go) => onClickAction?.Invoke();
        }
    }

    // 버튼 상태 설정 메서드 
    public void SetButtonState(InputButton inputButton, bool enabled)
    {
        if (inputButton.button != null)
        {
            inputButton.isEnabled = enabled;
            if (inputButton.buttonCollider != null)
            {
                inputButton.buttonCollider.enabled = enabled;
            }
        }
    }

    // 조이스틱으로부터 수평 입력값 얻기 
    public float GetHorizontalInput()
    {
        if (joystick != null)
        {
            return joystick.GetHorizontalValue() * joystickSensitivity;
        }
        return 0f;
    }

    // 버튼 클릭 이벤트 핸들러 
    private void OnJumpButtonPressed() => OnJumpButtonClicked?.Invoke();
    private void OnBasicAttackButtonPressed() => OnBasicAttackButtonClicked?.Invoke();
    /*
    private void OnFirstSkillButtonPressed() => OnFirstSkillButtonClicked?.Invoke();
    private void OnSecondSkillButtonPressed() => OnSecondSkillButtonClicked?.Invoke();
    private void OnThirdSkillButtonPressed() => OnThirdSkillButtonClicked?.Invoke();
    private void OnFourthSkillButtonPressed() => OnFourthSkillButtonClicked?.Invoke();
    */
}
