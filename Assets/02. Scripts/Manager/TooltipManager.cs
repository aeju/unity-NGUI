using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    public UILabel lable;
    public UITooltip tooltip;
    private bool isTooltipVisible = false;

    void Start()
    {
        // UIButton 컴포넌트가 없다면 추가
        if (GetComponent<UIButton>() == null)
        {
            gameObject.AddComponent<UIButton>();
        }

        UIEventListener.Get(gameObject).onClick += OnClick;
    }

    void OnClick(GameObject go)
    {
        if (!isTooltipVisible)
        {
            // 툴팁 표시
            //tooltip.ShowTooltip(true);
            UITooltip.ShowText(lable.text);
            isTooltipVisible = true;
        }
        else
        {
            // 툴팁 숨기기
            // tooltip.HideTooltip();
            isTooltipVisible = false;
        }
    }

    void OnDisable()
    {
        // 오브젝트가 비활성화될 때 툴팁 숨기기
        if (tooltip != null)
        {
            //tooltip.HideTooltip();
        }
        isTooltipVisible = false;
    }
}
