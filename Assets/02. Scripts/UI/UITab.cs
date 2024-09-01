using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITab : MonoBehaviour
{
    public List<UIButton> tabButtons;
    public List<GameObject> tabContents;

    private int currentTabIndex = 0;

    void Awake()
    {
        // 프리팹 생성 시 모든 탭 콘텐츠를 비활성화하고 첫 번째 탭만 활성화
        for (int i = 0; i < tabContents.Count; i++)
        {
            NGUITools.SetActive(tabContents[i], i == 0);
        }

        // 모든 탭 버튼 초기화
        for (int i = 0; i < tabButtons.Count; i++)
        {
            SetButtonState(i, i == 0);
        }
    }
    
    void Start()
    {
        // 각 탭 버튼에 클릭 이벤트 추가
        for (int i = 0; i < tabButtons.Count; i++)
        {
            int index = i; // 클로저를 위해 로컬 변수 사용
            UIEventListener.Get(tabButtons[i].gameObject).onClick += (go) => OnTabClick(index);
        }
    }

    void OnTabClick(int tabIndex)
    {
        SetTab(tabIndex);
    }

    void SetTab(int tabIndex)
    {
        // 유효한 인덱스인지 확인
        if (tabIndex < 0 || tabIndex >= tabButtons.Count || tabIndex >= tabContents.Count)
            return;

        // 이전 탭 비활성화
        SetButtonState(currentTabIndex, false);
        NGUITools.SetActive(tabContents[currentTabIndex], false);

        // 새 탭 활성화
        currentTabIndex = tabIndex;
        SetButtonState(currentTabIndex, true);
        NGUITools.SetActive(tabContents[currentTabIndex], true);
    }

    void SetButtonState(int buttonIndex, bool isSelected)
    {
        if (buttonIndex < 0 || buttonIndex >= tabButtons.Count)
            return;

        UIButton button = tabButtons[buttonIndex];
        
        // 버튼의 collider는 계속 활성화 상태로 유지
        button.isEnabled = true;
    }
}