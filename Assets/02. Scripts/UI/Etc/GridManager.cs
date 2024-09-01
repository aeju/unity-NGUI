using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public UIGrid grid;

    void UpdateGrid()
    {
        // 그리드 업데이트
        grid.Reposition();

        // 전체 너비 계산
        float totalWidth = 0;
        foreach (Transform child in grid.transform)
        {
            UIWidget widget = child.GetComponent<UIWidget>();
            if (widget != null)
            {
                // totalWidth += widget.width + grid.padding.x;
            }
        }

        // 각 자식 요소의 위치 조정
        foreach (Transform child in grid.transform)
        {
            Vector3 pos = child.localPosition;
            // pos.x = -pos.x - totalWidth + grid.padding.x;
            child.localPosition = pos;
        }
    }
}
