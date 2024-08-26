using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleTest : MonoBehaviour
{
    private Vector3 originalPosition;
    private Vector3 offset;
    private bool isDragging = false;

    void Start()
    {
        originalPosition = transform.position;
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
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
