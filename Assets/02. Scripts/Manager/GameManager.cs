using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PopupManager popupManager;

    private void Update()
    {
        // ESC 키 입력 처리
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandleEscapeKey();
        }
    }

    private void HandleEscapeKey()
    {
        if (PopupManager.Instance.PopupCount == 0)
        {
            ShowExitConfirmationPopup();
        }
        else
        {
            popupManager.CloseTopPopup();
        }
    }

    private void ShowExitConfirmationPopup()
    {
        popupManager.ShowYesNoPopup("게임 종료", "정말로 게임을 종료하시겠습니까?", 
            () => Application.Quit(), 
            () => { /* 취소 시 아무 동작 없음 */ });
    }

}
