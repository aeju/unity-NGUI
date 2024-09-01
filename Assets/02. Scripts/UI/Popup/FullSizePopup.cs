using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullSizePopup : PopupBase
{
    public UIButton CloseButton;

    private void Awake()
    {
        TitleLabel = GetComponentInChildren<UILabel>();
        CloseButton = GetComponentInChildren<UIButton>();
        
        if (CloseButton == null)
        {
            Debug.LogError("CloseButton is null.");
        }
        else
        {
            // 닫기 버튼
            CloseButton.isEnabled = true;
            UIEventListener.Get(CloseButton.gameObject).onClick = OnCloseButtonClick;
        }
    }

    public override void Show(string title)
    {
        SetTitle(title);
    }

    public override void Hide()
    {
        PopupManager.Instance.CloseTopPopup();
    }
    
    private void OnCloseButtonClick(GameObject go)
    {
        Hide();
    }
}
