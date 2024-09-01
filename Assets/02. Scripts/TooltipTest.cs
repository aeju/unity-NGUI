using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipTest : MonoBehaviour
{
    public UITooltip tooltip;  // UITooltip 컴포넌트
    public UILabel buttonLabel;  // 버튼에 있는 UILabel (있다면)
    
    private bool isTooltipVisible = false;

    void Start()
    {
        // UIButton 컴포넌트가 없다면 추가
        if (GetComponent<UIButton>() == null)
        {
            gameObject.AddComponent<UIButton>();
        }

        UIEventListener.Get(gameObject).onClick += OnClick;

        // 시작할 때 툴팁 숨기기
        HideTooltip();
    }

    void OnClick(GameObject go)
    {
        ToggleTooltip();
    }

    void ToggleTooltip()
    {
        if (tooltip != null)
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
    }

    void ShowTooltip()
    {
        if (buttonLabel != null)
        {
            UITooltip.ShowText(buttonLabel.text);
        }
        else
        {
            UITooltip.ShowText("Tooltip Text");  // 기본 텍스트
        }
        isTooltipVisible = true;

        // 툴팁 위치 조정 (필요하다면)
        PositionTooltip();
    }

    void HideTooltip()
    {
        UITooltip.ShowText("");  // 빈 문자열을 전달하여 툴팁 숨기기
        isTooltipVisible = false;
    }

    void PositionTooltip()
    {
        if (tooltip != null && tooltip.transform != null)
        {
            // 버튼의 위치를 기반으로 툴팁 위치 설정
            Vector3 buttonPosition = transform.position;
            tooltip.transform.position = buttonPosition + new Vector3(0, 50, 0);  // 버튼 위 50 유닛에 위치
        }
    }

    void OnDisable()
    {
        // 오브젝트가 비활성화될 때 툴팁 숨기기
        HideTooltip();
    }

    // 툴팁 텍스트를 설정하는 메서드 (필요하다면 사용)
    public void SetTooltipText(string text)
    {
        if (tooltip != null && tooltip.text != null)
        {
            tooltip.text.text = text;
        }
    }
}
