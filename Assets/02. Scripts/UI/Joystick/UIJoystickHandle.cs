using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIJoystickHandle : MonoBehaviour
{
    private Vector3 originalPosition;
    private Vector3 offset;
    private bool isDragging = false;
    
    // 0.2 -> 배경 안에서만 드래그 가능 
    public float dragRadius; // handleSprite / targetSprite
    public UISprite bgSprite;
    private UISprite handleSprite;
    [SerializeField] private Color bgDragColor;

    private int depth;

    void Awake()
    {
        // 조이스틱 위치 초기화
        originalPosition = transform.position; 
        
        handleSprite = GetComponent<UISprite>();
        // dragRadius 초기화
        if (bgSprite != null && handleSprite != null)
        {
            // dragRadius를 handleSprite와 targetSprite의 localScale.x 비율로 설정
            dragRadius = handleSprite.transform.localScale.x / bgSprite.transform.localScale.x / 2;
        }
        else
        {
            Debug.LogError("Target UISprite or Handle UISprite is not assigned!");
        }
    }

    void Start()
    {
        // Awake에서 초기화한 값들을 로그로 출력
        Debug.Log("handle's radius: " + handleSprite.transform.localScale.x);
        Debug.Log("Handle's scale: " + handleSprite.transform.localScale.x);
        Debug.Log("Target's scale: " + bgSprite.transform.localScale.x);
        Debug.Log("Drag radius set to: " + dragRadius);
    }

    void OnPress(bool isPressed)
    {
        if (isPressed)
        {
            // 드래그 시작
            isDragging = true;
            offset = transform.position - GetMouseWorldPosition();
            
            // 조이스틱 배경 색상, 알파값 변경
            bgSprite.alpha = 1f;
            bgSprite.color = bgDragColor;
        }
        else
        {
            // 드래그 종료
            isDragging = false;
            transform.position = originalPosition;
            bgSprite.alpha = 0.5f;
            bgSprite.color = Color.white;
            
            Vector3 offset = transform.position - originalPosition;
            if (offset.magnitude > 0.05f) // 작은 오차 무시
            {
                transform.position = originalPosition;
            }
        }
    }

    void OnDrag(Vector2 delta)
    {
        if (isDragging)
        { 
            Vector3 newPosition = GetMouseWorldPosition() + offset;
            Vector2 dragDelta = new Vector2(newPosition.x - originalPosition.x, newPosition.y - originalPosition.y);
            
            // dragRadius 이상으로 드래그해도, 핸들 유지 
            if (dragDelta.magnitude > dragRadius)
            {
                dragDelta = dragDelta.normalized * dragRadius;
            }
            
            newPosition.x = originalPosition.x + dragDelta.x;
            newPosition.y = originalPosition.y + dragDelta.y;
            newPosition.z = originalPosition.z;  // Z 위치는 변경하지 않음

            transform.position = newPosition;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    
    // 조이스틱의 현재 값을 얻는 메서드
    public Vector2 GetJoystickValue()
    {
        if (bgSprite == null) return Vector2.zero;

        Vector2 diff = transform.position - bgSprite.transform.position;
        return diff / (bgSprite.transform.localScale.x * dragRadius);
    }

    // 수평 입력값만 반환하는 메서드
    public float GetHorizontalValue()
    {
        return GetJoystickValue().x;
    }
}
