using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    // public GameObject popupPrefab; // NGUI 팝업 프리팹
    public YesNoPopup popupPrefab; // NGUI 팝업 프리팹
    //private YesNoPopup currentPopup; // 현재 표시된 팝업을 추적
    private IPopup currentPopup; // 현재 표시된 팝업을 추적
    
    void Update()
    {
        // Android의 뒤로 가기 버튼 또는 ESC 키 감지
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape Key!");
            // 팝업 켜져있는데, 뒤로가기 -> 창닫기 
            if (currentPopup != null && currentPopup.IsVisible)
            {
                HidePopup();
            }
            else
            {
                ShowPopup();
            }
        }
    }

    void ShowPopup()
    {
        // 이미 팝업이 표시되어 있다면 무시
        if (currentPopup != null)
            return;

        
        // 팝업 인스턴스화
        //currentPopup = NGUITools.AddChild(gameObject, popupPrefab);
        
        //GameObject popupObject = NGUITools.AddChild(gameObject, popupPrefab.gameObject);
        //currentPopup = popupObject.GetComponent<YesNoPopup>();
        
        // 팝업 활성화
        /*
        if (!popupObject.activeSelf)
        {
            //NGUITools.SetActive(popupPrefab, true);
            NGUITools.SetActive(popupObject, true);
        }
        */
        
        // 팝업 인스턴스화
        if (currentPopup == null)
        {
            GameObject popupObject = NGUITools.AddChild(gameObject, popupPrefab.gameObject);
            currentPopup = popupObject.GetComponent<IPopup>();
        }
        
        currentPopup.Show();
        
        // YesNoPopup 특정 설정
        YesNoPopup yesNoPopup = currentPopup as YesNoPopup;
        if (yesNoPopup != null)
        {
            /*
            // 팝업 내용 설정
            if (currentPopup.titleLabel != null)
                currentPopup.titleLabel.text = "종료 확인";
            if (currentPopup.messageLabel != null)
                currentPopup.messageLabel.text = "정말로 종료하시겠습니까?";
            if (currentPopup.yesButton)

            // 버튼 내용 설정
            if (currentPopup.yesButtonLabel != null)
                    currentPopup.yesButtonLabel.text = "예";
            if (currentPopup.noButtonLabel != null)
                currentPopup.noButtonLabel.text = "아니오";
            
            // 버튼 이벤트 설정
            if (currentPopup.yesButton != null)
                UIEventListener.Get(currentPopup.yesButton.gameObject).onClick += OnYesClicked;
            if (currentPopup.noButton != null)
                UIEventListener.Get(currentPopup.noButton.gameObject).onClick += OnNoClicked;
                */
            
            if (yesNoPopup.TitleLabel != null)
                yesNoPopup.TitleLabel.text = "종료 확인";
            if (yesNoPopup.MessageLabel != null)
                yesNoPopup.MessageLabel.text = "정말로 종료하시겠습니까?";

            // 버튼 내용 설정
            if (yesNoPopup.YesButtonLabel != null)
                yesNoPopup.YesButtonLabel.text = "예";
            if (yesNoPopup.NoButtonLabel != null)
                yesNoPopup.NoButtonLabel.text = "아니오";
            
            // 버튼 이벤트 설정
            if (yesNoPopup.YesButton != null)
                UIEventListener.Get(yesNoPopup.YesButton.gameObject).onClick += OnYesClicked;
            if (yesNoPopup.NoButton != null)
                UIEventListener.Get(yesNoPopup.NoButton.gameObject).onClick += OnNoClicked;
        }
    }

    void OnYesClicked(GameObject go)
    {
        // 게임 종료 또는 필요한 동작 수행
        Application.Quit();
    }

    void OnNoClicked(GameObject go)
    {
        // 팝업 닫기
        if (currentPopup != null)
        {
            HidePopup();
            /*
            // 이벤트 리스너 제거
            if (currentPopup.yesButton != null)
                UIEventListener.Get(currentPopup.yesButton.gameObject).onClick -= OnYesClicked;
            if (currentPopup.noButton != null)
                UIEventListener.Get(currentPopup.noButton.gameObject).onClick -= OnNoClicked;
            
            Destroy(currentPopup.gameObject);
            currentPopup = null;
            */
        }
    }
    
    void HidePopup()
    {
        if (currentPopup != null)
        {
            YesNoPopup yesNoPopup = currentPopup as YesNoPopup;
            if (yesNoPopup != null)
            {
                // 이벤트 리스너 제거
                if (yesNoPopup.YesButton != null)
                    UIEventListener.Get(yesNoPopup.YesButton.gameObject).onClick -= OnYesClicked;
                if (yesNoPopup.NoButton != null)
                    UIEventListener.Get(yesNoPopup.NoButton.gameObject).onClick -= OnNoClicked;
            }

            currentPopup.Hide();
        }
    }
}
