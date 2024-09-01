using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PopupButtonInfo
{
    public UIButton button;
    // public Definitions.PopupType PopupType;
    public string labelText;
}

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private List<PopupButtonInfo> popupButtons = new List<PopupButtonInfo>();
    
    private void Start()
    {
        InitializeButtons();
    }

    private void InitializeButtons()
    {
        foreach (var buttonInfo in popupButtons)
        {
            //buttonInfo.button.onClick.Add(new EventDelegate(() => OnPopupButtonClicked(buttonInfo)));
            UILabel label = buttonInfo.button.GetComponentInChildren<UILabel>();
            if (label != null)
            {
                label.text = buttonInfo.labelText;
            }
        }
    }

    private void OnPopupButtonClicked(PopupButtonInfo buttonInfo)
    {
        PopupManager.Instance.ShowFullSizePopup(buttonInfo.labelText);
    }
    
    /*
    private void OnPopupButtonClicked(PopupButtonInfo buttonInfo)
    {
        switch (buttonInfo.PopupType)
        {
            case Definitions.PopupType.Toast:
                PopupManager.Instance.ShowToast(buttonInfo.labelText, 3f);
                break;
            case Definitions.PopupType.FullSize:
                PopupManager.Instance.ShowFullSizePopup(buttonInfo.labelText);
                break;
            case Definitions.PopupType.YesNo:
                PopupManager.Instance.ShowYesNoPopup(buttonInfo.labelText, "추가 내용을 여기에 입력하세요.", () => Debug.Log("Yes"), () => Debug.Log("No"));
                break;
        }
    }
    */
    
    /*
    // public GameObject popupParent; // 팝업 생성될 곳 (center)
    
    // 팝업 프리팹들을 저장할 딕셔너리
    public Dictionary<string, GameObject> popupPrefabs = new Dictionary<string, GameObject>();

    private GameObject currentPopup;
    
    // 팝업 프리팹 등록 메소드
    public void RegisterPopupPrefab(string popupName, GameObject prefab)
    {
        if (!popupPrefabs.ContainsKey(popupName))
        {
            popupPrefabs.Add(popupName, prefab);
        }
    }

    // 팝업 생성 메소드
    public void ShowPopup(string popupName)
    {
        if (popupPrefabs.TryGetValue(popupName, out GameObject popupPrefab))
        {
            // 이전 팝업이 있다면 제거
            if (currentPopup != null)
            {
                Destroy(currentPopup);
            }

            // 새 팝업 생성
            currentPopup = NGUITools.AddChild(popupParent, popupPrefab);

            // 팝업 위치 조정 (필요한 경우)
            UIWidget widget = currentPopup.GetComponent<UIWidget>();
            if (widget != null)
            {
                widget.pivot = UIWidget.Pivot.Center;
                //widget.centerOnChild = popupParent.transform;
            }
        }
        else
        {
            Debug.LogWarning($"Popup prefab '{popupName}' not found!");
        }
    }

    // 현재 팝업 닫기 메소드
    public void CloseCurrentPopup()
    {
        if (currentPopup != null)
        {
            Destroy(currentPopup);
            currentPopup = null;
        }
    }
    */
}
