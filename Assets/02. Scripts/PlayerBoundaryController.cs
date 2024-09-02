using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoundaryController : MonoBehaviour
{
    public Camera mainCamera;
    public Vector2 mapSize = new Vector2(100f, 100f); // 맵의 전체 크기
    public Vector2 cameraOffset = new Vector2(5f, 5f); // 카메라 경계 오프셋

    private float minX, maxX, minY, maxY;
    private float cameraHalfWidth, cameraHalfHeight;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        cameraHalfHeight = mainCamera.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * mainCamera.aspect;

        // 플레이어 이동 가능 범위 계산
        minX = -mapSize.x / 2 + cameraHalfWidth;
        maxX = mapSize.x / 2 - cameraHalfWidth;
        minY = -mapSize.y / 2 + cameraHalfHeight;
        maxY = mapSize.y / 2 - cameraHalfHeight;
    }

    void LateUpdate()
    {
        Vector3 playerPos = transform.position;

        // 플레이어 위치 제한
        playerPos.x = Mathf.Clamp(playerPos.x, minX, maxX);
        playerPos.y = Mathf.Clamp(playerPos.y, minY, maxY);

        transform.position = playerPos;

        // 카메라 위치 조정
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.x = Mathf.Clamp(playerPos.x, minX + cameraOffset.x, maxX - cameraOffset.x);
        cameraPos.y = Mathf.Clamp(playerPos.y, minY + cameraOffset.y, maxY - cameraOffset.y);
        mainCamera.transform.position = new Vector3(cameraPos.x, cameraPos.y, mainCamera.transform.position.z);
    }

    void OnDrawGizmos()
    {
        // 에디터에서 경계 시각화
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(mapSize.x, mapSize.y, 0));
    }
}
