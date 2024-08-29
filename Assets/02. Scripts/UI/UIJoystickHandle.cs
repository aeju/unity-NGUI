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

    private int depth;

    void Start()
    {
        originalPosition = transform.position;
        
        handleSprite = GetComponent<UISprite>();
        Debug.Log("handle's radius" + handleSprite.transform.localScale.x);
        
        if (bgSprite != null && handleSprite != null)
        {
            // dragRadius를 handleSprite와 targetSprite의 localScale.x 비율로 설정
            dragRadius = handleSprite.transform.localScale.x / bgSprite.transform.localScale.x;
            Debug.Log("Handle's scale: " + handleSprite.transform.localScale.x);
            Debug.Log("Target's scale: " + bgSprite.transform.localScale.x);
            Debug.Log("Drag radius set to: " + dragRadius);
        }
        else
        {
            Debug.LogError("Target UISprite or Handle UISprite is not assigned!");
        }
    }

    void OnPress(bool isPressed)
    {
        if (isPressed)
        {
            // 드래그 시작
            isDragging = true;
            offset = transform.position - GetMouseWorldPosition();
        }
        else
        {
            // 드래그 종료
            isDragging = false;
            transform.position = originalPosition;
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
