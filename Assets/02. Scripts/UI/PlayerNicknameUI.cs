using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNicknameUI : MonoBehaviour
{
    // 닉네임 UI가 따라갈 대상 (플레이어)
    public Transform target;
    // 적용할 마지막  오프셋
    public Vector3 offset;
    // 따라다닐 오브젝트를 비추는 메인 카메라
    public Camera mainCamera;
    // UI를 렌더링하는 카메라 
    public Camera uiCamera;

    // 매 프레임마다 위젯 위치를 갱신
    private void Update()
    {
        if (target != null)
        {
            // 1. 타겟 위치를 메인 카메라의 스크린 좌표로 변환
            Vector3 finalPos = mainCamera.WorldToScreenPoint(target.position);
            
            // 2. 스크린 좌표를 UI 카메라의 월드 좌표로 변환
            finalPos = uiCamera.ScreenToWorldPoint(finalPos);
            
            // 3. Z 좌표를 0으로 설정 (2D UI에서는 Z 좌표가 필요 없음)
            finalPos = new Vector3(finalPos.x, finalPos.y, 0);
            
            // 4. 최종 위치에 오프셋을 더하여 UI 요소의 위치 설정
            transform.position = finalPos + offset;
        }
    }
}
