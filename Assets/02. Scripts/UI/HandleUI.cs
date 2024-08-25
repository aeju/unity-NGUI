using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleUI : MonoBehaviour
{
    private Vector3 originalPosition;
    private Vector3 offset;
    private bool isDragging = false;
    
    private float dragRadius; // handleSprite / targetSprite
    public UISprite targetSprite;
    private UISprite handleSprite;

    void Start()
    {
        originalPosition = transform.position;
        
        handleSprite = GetComponent<UISprite>();
        Debug.Log("handle's radius" + handleSprite.transform.localScale.x);
        
        if (targetSprite != null && handleSprite != null)
        {
            // dragRadius를 handleSprite와 targetSprite의 localScale.x 비율로 설정
            dragRadius = handleSprite.transform.localScale.x / targetSprite.transform.localScale.x;
            Debug.Log("Handle's scale: " + handleSprite.transform.localScale.x);
            Debug.Log("Target's scale: " + targetSprite.transform.localScale.x);
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
            // transform.position = GetMouseWorldPosition() + offset;
            
            Vector3 newPosition = GetMouseWorldPosition() + offset;
            Vector2 dragDelta = new Vector2(newPosition.x - originalPosition.x, newPosition.y - originalPosition.y);
            
            // 원형 제한 적용
            //dragDelta = Vector2.ClampMagnitude(dragDelta, dragRadius);
            
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
}
