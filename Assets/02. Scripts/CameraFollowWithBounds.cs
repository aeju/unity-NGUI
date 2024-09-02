using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowWithBounds : MonoBehaviour
{
    public Transform target; // 따라갈 대상 (플레이어)
    public float smoothSpeed = 0.125f; // 카메라 이동 부드러움
    public Vector2 maxBounds; // 카메라 이동 최대 범위
    public Vector2 minBounds; // 카메라 이동 최소 범위

    private Vector3 velocity = Vector3.zero;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam == null)
        {
            Debug.LogError("카메라 컴포넌트를 찾을 수 없습니다!");
        }
    }

    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("따라갈 대상(target)이 설정되지 않았습니다!");
            return;
        }

        // 목표 위치 계산
        Vector3 desiredPosition = target.position;
        desiredPosition.z = transform.position.z; // 카메라의 z 위치 유지

        // 부드러운 이동
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

        // 카메라 경계 계산
        float camHeight = cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        float minX = minBounds.x + camWidth;
        float maxX = maxBounds.x - camWidth;
        float minY = minBounds.y + camHeight;
        float maxY = maxBounds.y - camHeight;

        // 카메라 위치를 경계 내로 제한
        smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minX, maxX);
        smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minY, maxY);

        // 카메라 위치 업데이트
        transform.position = smoothedPosition;
    }

    void OnDrawGizmos()
    {
        // 에디터에서 카메라 이동 범위 시각화
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube((maxBounds + minBounds) / 2, maxBounds - minBounds);
    }
}
