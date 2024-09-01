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
            Debug.LogError("CloseButton is null. Make sure it exists in the prefab.");
        }
        else
        {
            Debug.Log("CloseButton found. Attaching onClick listener.");
            UIEventListener.Get(CloseButton.gameObject).onClick = OnCloseButtonClick;
        }
        
        // 닫기 버튼 
        //UIEventListener.Get(CloseButton.gameObject).onClick = OnCloseButtonClick;
    }

    public override void Show(string title)
    {
        SetTitle(title);
    }

    public override void Hide()
    {
        Debug.Log("CloseButton clicked!");
        PopupManager.Instance.CloseTopPopup();
    }
    
    private void OnCloseButtonClick(GameObject go)
    {
        Hide();
    }
}
