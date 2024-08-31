using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/UIGridSingleLineRight")]
public class UIGridSingleLineRight : MonoBehaviour
{
    public enum Anchor
    {
        Right,
    }
    
    public enum Arrangement
    {
        Horizontal,
        // Vertical,
    }
    
    public Anchor pivot = Anchor.Right;
    public Arrangement arrangement = Arrangement.Horizontal;
    public float cellWidth = 200f; // 각 셀 너비
    public float cellHeight = 200f; // 각 셀 높이 
    public bool repositionNow = false;
    public bool sorted = false;
    public bool hideInactive = true; // 비활성화된 오브젝트 숨김 여부 (true!)

    bool mStarted = false;
    
    void Start()
    {
        mStarted = true;
        Reposition(); // 초기 위치 지정
    }

    void Update()
    {
        if (repositionNow)
        {
            repositionNow = false;
            Reposition();
        }
    }

    static public int SortByName(Transform a, Transform b) { return string.Compare(a.name, b.name); }

    // 그리드 내의 모든 요소 위치를 재계산
    public void Reposition()
    {
        if (!mStarted)
        {
            repositionNow = true;
            return;
        }

        Transform myTrans = transform;

        // x, y 0으로 초기화
        int x = 0;
        int y = 0;
        
        // 총 너비 계산
        float totalWidth = cellWidth * myTrans.childCount;
        
        // 활성화된 자식 오브젝트 수 계산
        int activeChildCount = 0;
        for (int i = 0; i < myTrans.childCount; ++i)
        {
            if (!hideInactive || NGUITools.GetActive(myTrans.GetChild(i).gameObject))
            {
                activeChildCount++;
            }
        }
        
        // 현재 줄의 아이템 수
        int itemsInCurrentLine = activeChildCount;
        // 시작 x 위치 계산
        int startX = (int)totalWidth - (int)(itemsInCurrentLine * cellWidth);
        
        if (sorted) // 정렬이 필요한 경우
        {
            List<Transform> list = new List<Transform>();

            // 활성화된 자식 오브젝트만 리스트에 추가 
            for (int i = 0; i < myTrans.childCount; ++i)
            {
                Transform t = myTrans.GetChild(i);
                if (t && (!hideInactive || NGUITools.GetActive(t.gameObject))) list.Add(t);
            }
            list.Sort(SortByName);

            // 정렬된 리스트의 각 요소를 그리드에 배치 
            for (int i = 0, imax = list.Count; i < imax; ++i)
            {
                Transform t = list[i];

                if (!NGUITools.GetActive(t.gameObject) && hideInactive) continue;

                float depth = t.localPosition.z;
                t.localPosition = (arrangement == Arrangement.Horizontal) ?
                    //new Vector3(cellWidth * x, -cellHeight * y, depth) :
                    new Vector3(startX + cellWidth * x, -cellHeight * y, depth) :
                    new Vector3(cellWidth * y, -cellHeight * x, depth);

                x++;
            }
        }
        else // 정렬이 필요없는 경우
        {
            // 모든 자식 오브젝트를 순서대로 그리드에 배치
            for (int i = 0; i < myTrans.childCount; ++i)
            {
                // 자식 오브젝트 위치 가져오기 
                Transform t = myTrans.GetChild(i);

                // 비활성화 오브젝트 처리 (hideInactive - true일 때)
                if (!NGUITools.GetActive(t.gameObject) && hideInactive) continue;

                float depth = t.localPosition.z;
                // 위치 설정 
                t.localPosition = (arrangement == Arrangement.Horizontal) ?
                    // Horizontal : (cellWidth * x, -cellHeight * y, z)
                    new Vector3(startX + cellWidth * x, -cellHeight * y, depth) :
                    // Vertical : (cellWidth * y, -cellHeight * x, z) 
                    new Vector3(cellWidth * y, -cellHeight * x, depth);

                x++;
            }
        }

        UIDraggablePanel drag = NGUITools.FindInParents<UIDraggablePanel>(gameObject);
        if (drag != null) drag.UpdateScrollbars(true);
    }
}