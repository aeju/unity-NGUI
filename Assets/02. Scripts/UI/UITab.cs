using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITab : MonoBehaviour
{
    public List<UIButton> tabButtons;
    public List<GameObject> tabContents;

    private int currentTabIndex = 0;

    void Start()
    {
        // 초기 탭 설정
        SetTab(0);

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
        tabButtons[currentTabIndex].isEnabled = true;
        NGUITools.SetActive(tabContents[currentTabIndex], false);

        // 새 탭 활성화
        currentTabIndex = tabIndex;
        tabButtons[currentTabIndex].isEnabled = false;
        NGUITools.SetActive(tabContents[currentTabIndex], true);
    }
}
