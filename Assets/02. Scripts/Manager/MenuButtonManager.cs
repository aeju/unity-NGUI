using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject buttonContainer; // 버튼들의 상위 오브젝트
    [SerializeField] private PopupManager popupManager;

    // private List<UIButton> menuButtons = new List<UIButton>();

    [System.Serializable]
    private struct ButtonPopupInfo
    {
        public UIButton button;
        public Definitions.PopupType popupType;
        public Definitions.ButtonType buttonType;
        public string popupTitle;

        public ButtonPopupInfo(UIButton btn, Definitions.PopupType pType, Definitions.ButtonType bType, string title)
        {
            button = btn;
            popupType = pType;
            buttonType = bType;
            popupTitle = title;
        }
    }
    
    [SerializeField] private List<ButtonPopupInfo> buttonPopupInfos = new List<ButtonPopupInfo>();
    
    private void Start()
    {
        InitializeButtons();
    }

    private void InitializeButtons()
    {
        /*
        menuButtons.AddRange(buttonContainer.GetComponentsInChildren<UIButton>());

        
        for (int i = 0; i < menuButtons.Count; i++)
        {
            int index = i; 
            UIEventListener.Get(menuButtons[i].gameObject).onClick = (go) => OnButtonClicked(index);
        }
        */
        // 버튼 컨테이너에서 모든 UIButton 컴포넌트를 가져오기
        UIButton[] buttons = buttonContainer.GetComponentsInChildren<UIButton>();

        // 각 버튼에 클릭 이벤트를 연결
        for (int i = 0; i < buttons.Length; i++)
        {
            ButtonPopupInfo info = new ButtonPopupInfo(
                buttons[i],
                Definitions.PopupType.FullSize,  // 기본값
                Definitions.ButtonType.Default,  // 기본값
                $"Popup {i}"
            );
            buttonPopupInfos.Add(info);

            int index = i;
            UIEventListener.Get(buttons[i].gameObject).onClick = (go) => OnButtonClicked(index);
        }
    }

    private void OnButtonClicked(int buttonIndex)
    {
        ButtonPopupInfo info = buttonPopupInfos[buttonIndex];

        switch (info.buttonType)
        {
            case Definitions.ButtonType.Default: // 버튼의 종류가 ButtonType == default
                string popupTitle = $"Popup {buttonIndex - 2}";
                popupManager.ShowFullSizePopup(popupTitle);
                break;
            case Definitions.ButtonType.Inventory:
                Debug.Log("Inventory button clicked");
                // 인벤토리 관련 동작 구현
                break;
            case Definitions.ButtonType.Settings:
                Debug.Log("Settings button clicked");
                // 설정 관련 동작 구현
                break;
            case Definitions.ButtonType.ButtonSettings:
                Debug.Log("ButtonSettings button clicked");
                // 버튼 설정 관련 동작 구현
                break;
            case Definitions.ButtonType.Notify:
                Debug.Log("Notify button clicked");
                // 알림 관련 동작 구현
                break;
            default: 
                Debug.LogWarning($"Unhandled button type: {info.buttonType}");
                break;
        }
        
    }
}
