using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipTrigger : MonoBehaviour
{
    public UITooltip tooltip;
    private bool isTooltipVisible = false;

    void Start()
    {
        // UIButton 컴포넌트가 없다면 추가
        if (GetComponent<UIButton>() == null)
        {
            gameObject.AddComponent<UIButton>();
        }

        // 시작 시 툴팁 숨기기
        if (tooltip != null)
        {
            HideTooltip();
        }
    }

    void OnClick()
    {
        ToggleTooltip();
    }

    void ToggleTooltip()
    {
        if (!isTooltipVisible)
        {
            ShowTooltip();
        }
        else
        {
            HideTooltip();
        }
    }

    void ShowTooltip()
    {
        if (tooltip != null)
        {
            if (tooltip.background != null)
            {
                NGUITools.SetActive(tooltip.background.gameObject, true);
            }
            if (tooltip.text != null)
            {
                NGUITools.SetActive(tooltip.text.gameObject, true);
            }
            isTooltipVisible = true;
        }
    }

    void HideTooltip()
    {
        if (tooltip != null)
        {
            if (tooltip.background != null)
            {
                NGUITools.SetActive(tooltip.background.gameObject, false);
            }
            if (tooltip.text != null)
            {
                NGUITools.SetActive(tooltip.text.gameObject, false);
            }
            isTooltipVisible = false;
        }
    }

    void OnDisable()
    {
        // 오브젝트가 비활성화될 때 툴팁 숨기기
        HideTooltip();
    }
}
