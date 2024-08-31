using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject buttonContainer; // 버튼들의 상위 오브젝트
    [SerializeField] private PopupManager popupManager;

    private List<UIButton> menuButtons = new List<UIButton>();

    private void Start()
    {
        InitializeButtons();
    }

    private void InitializeButtons()
    {
        // 버튼 컨테이너에서 모든 UIButton 컴포넌트를 가져오기
        menuButtons.AddRange(buttonContainer.GetComponentsInChildren<UIButton>());

        // 각 버튼에 클릭 이벤트를 연결
        for (int i = 0; i < menuButtons.Count; i++)
        {
            int index = i; 
            UIEventListener.Get(menuButtons[i].gameObject).onClick = (go) => OnButtonClicked(index);
        }
    }

    private void OnButtonClicked(int buttonIndex)
    {
        string popupTitle = $"Popup {buttonIndex - 2}";
        popupManager.ShowFullSizePopup(popupTitle);
    }
}
