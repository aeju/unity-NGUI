using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public PlayerController playerController;
    private Transform target; 

    private Vector3 offset; 
    private Quaternion initialRotation; 
    
    void Start()
    {
        if (playerController != null)
        {
            target = playerController.transform;
        }
        else
        {
            Debug.LogError("PlayerController not found in the scene.");
        }
        
        // initial offset : 카메라 - 플레이어 위치
        offset = transform.position - target.position;
        
        initialRotation = transform.rotation;
    }

    void LateUpdate()
    {
        // 플레이어 위치 + offset
        Vector3 newPos = target.position + offset;
        
        // 카메라의 위치 = newPos로 업데이트
        transform.position = newPos;
        
        transform.rotation = initialRotation;
    }
}
