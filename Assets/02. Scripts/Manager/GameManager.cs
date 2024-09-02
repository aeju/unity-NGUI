using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject sky1;
    public GameObject sky2;

    void Start()
    {
        SelectRandomSky();
        ShowWelcomeMessage();
    }
    
    void SelectRandomSky()
    {
        if (sky1 == null || sky2 == null)
        {
            Debug.LogError("sky is null");
            return;
        }

        // 50% 확률로 선택
        bool selectSky1 = Random.value < 0.5f;

        sky1.SetActive(selectSky1);
        sky2.SetActive(!selectSky1);
    }
    
    void ShowWelcomeMessage()
    {
        PopupManager.Instance.ShowToast("게임에 오신 것을 환영합니다!", 3f);
    }
    
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
            PopupManager.Instance.CloseTopPopup();
        }
    }

    private void ShowExitConfirmationPopup()
    {
        PopupManager.Instance.ShowYesNoPopup("게임 종료", "정말로 게임을 종료하시겠습니까?", 
            //() => Application.Quit(), 
            () => StartCoroutine(ExitGameWithToast()), 
            () => PopupManager.Instance.CloseTopPopup());
    }

    private IEnumerator ExitGameWithToast()
    {
        PopupManager.Instance.ShowToast("게임을 종료합니다.", 2f);
        yield return new WaitForSeconds(2f);
        Application.Quit();
    }
}
