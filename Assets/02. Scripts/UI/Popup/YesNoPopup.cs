using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YesNoPopup : PopupBase
{
    [SerializeField] private UILabel messageLabel;
    [SerializeField] private UIButton yesButton;
    [SerializeField] private UIButton noButton;
    [SerializeField] private UILabel yesButtonLabel;
    [SerializeField] private UILabel noButtonLabel;
    
    private Action onYesCallback;
    private Action onNoCallback;
    /*
    public override UILabel MessageLabel { get => messageLabel; protected set => messageLabel = value; }
    public override UIButton YesButton { get => yesButton; protected set => yesButton = value; }
    public override UIButton NoButton { get => noButton; protected set => noButton = value; }
    public override UILabel YesButtonLabel { get => yesButtonLabel; protected set => yesButtonLabel = value; }
    public override UILabel NoButtonLabel { get => noButtonLabel; protected set => noButtonLabel = value; }
    */

    protected override void Awake()
    {
        base.Awake();
        InitializeButtonListeners();
    }
    
    // 버튼 리스너 초기화
    private void InitializeButtonListeners()
    {
        if (yesButton != null)
        {
            UIEventListener.Get(yesButton.gameObject).onClick = OnYesClicked;
        }
        else
        {
            Debug.LogError("YesButton is not assigned in the inspector");
        }

        if (noButton != null)
        {
            UIEventListener.Get(noButton.gameObject).onClick = OnNoClicked;
        }
        else
        {
            Debug.LogError("NoButton is not assigned in the inspector");
        }
    }
    
    public override void Show(string title)
    {
        SetTitle(title);
        gameObject.SetActive(true);
    }

    public override void Hide()
    {
        gameObject.SetActive(false);
    }
    
    // 팝업 내용 설정
    public void SetContent(string content)
    {
        if (messageLabel != null)
        {
            messageLabel.text = content;
        }
    }

    // 콜백 함수 설정
    public void SetCallbacks(Action onYes, Action onNo)
    {
        onYesCallback = onYes;
        onNoCallback = onNo;
    }

    // 예 버튼 클릭 처리
    private void OnYesClicked(GameObject go)
    {
        onYesCallback?.Invoke();
        PopupManager.Instance.CloseTopPopup();
    }

    // 아니오 버튼 클릭 처리
    private void OnNoClicked(GameObject go)
    {
        onNoCallback?.Invoke();
        PopupManager.Instance.CloseTopPopup();
    }
}
