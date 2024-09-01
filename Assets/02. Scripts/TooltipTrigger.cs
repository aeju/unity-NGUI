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
                // 스프라이트의 알파값을 1로 설정
                SetSpriteAlpha(tooltip.background, 1f);
            }
            if (tooltip.text != null)
            {
                tooltip.text.gameObject.SetActive(true);
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
                // 스프라이트의 알파값을 0으로 설정
                SetSpriteAlpha(tooltip.background, 0f);
            }
            if (tooltip.text != null)
            {
                tooltip.text.gameObject.SetActive(false);
            }
            isTooltipVisible = false;
        }
    }

    void SetSpriteAlpha(UISprite sprite, float alpha)
    {
        Color color = sprite.color;
        color.a = alpha;
        sprite.color = color;
    }

    void OnDisable()
    {
        // 오브젝트가 비활성화될 때 툴팁 숨기기
        HideTooltip();
    }
}
