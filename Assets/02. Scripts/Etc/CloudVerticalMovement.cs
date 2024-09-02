using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudVerticalMovement : MonoBehaviour
{
    public float amplitude = 1f; // 움직임의 폭
    public float frequency = 1f; // 움직임의 속도
    public bool affectChildren = true; // 자식 오브젝트도 영향을 받을지 여부

    private Vector3 startPos;
    private Transform[] childTransforms;

    void Start()
    {
        startPos = transform.position;
        
        if (affectChildren)
        {
            // 모든 자식 오브젝트의 Transform을 가져오기
            childTransforms = GetComponentsInChildren<Transform>();
        }
    }

    void Update()
    {
        // 시간에 따른 사인 파동을 계산
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;
        Vector3 newPos = startPos + new Vector3(0f, yOffset, 0f);

        // 부모 오브젝트의 위치를 업데이트합니다.
        transform.position = newPos;

        // 자식 오브젝트들도 움직이게 하려면
        if (affectChildren && childTransforms != null)
        {
            foreach (Transform child in childTransforms)
            {
                if (child != transform) // 부모 자신은 제외
                {
                    child.position = child.position + new Vector3(0f, yOffset, 0f);
                }
            }
        }
        
    }
}
