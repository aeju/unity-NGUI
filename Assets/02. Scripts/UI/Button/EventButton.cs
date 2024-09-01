using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EventButton : MonoBehaviour
{
    public UIButton circleButton;
    
    public UIButton closeButton;
    public GameObject panel;

    void Start()
    {
        // 초기 상태 설정
        if (panel != null)
        {
            NGUITools.SetActive(panel, false);
        }
        
        // 버튼 클릭 이벤트 등록
        if (circleButton != null)
        {
            UIEventListener.Get(circleButton.gameObject).onClick += OnCircleButtonClick;
        }
        
        if (closeButton != null)
        {
            UIEventListener.Get(closeButton.gameObject).onClick += OnCloseButtonClick;
        }
    }
    
    // 처음 : 패널 비활성화 
    void InitializePanel()
    {
        if (panel != null)
        {
            NGUITools.SetActive(circleButton.gameObject, true);
            NGUITools.SetActive(panel, false);
        }
        else
        {
            Debug.LogWarning("Panel is not assigned in the inspector.");
        }
    }
    
    // 버튼 클릭 이벤트 리스너를 설정
    void SetupButtonListeners()
    {
        if (circleButton != null)
        {
            UIEventListener.Get(circleButton.gameObject).onClick += OnCircleButtonClick;
        }
        else
        {
            Debug.LogWarning("Circle button is not assigned in the inspector.");
        }
        
        if (closeButton != null)
        {
            UIEventListener.Get(closeButton.gameObject).onClick += OnCloseButtonClick;
        }
        else
        {
            Debug.LogWarning("Close button is not assigned in the inspector.");
        }
    }
    
    void OnCircleButtonClick(GameObject go)
    {
        if (panel != null && circleButton != null)
        {
            // 패널 활성화
            NGUITools.SetActive(panel, true);
            // 기존 버튼 비활성화 
            NGUITools.SetActive(circleButton.gameObject, false);
        }
        else
        {
            Debug.LogError("Panel or circle button is missing. Cannot perform OnCircleButtonClick action.");
        }
    }
    
    void OnCloseButtonClick(GameObject go)
    {
        if (panel != null && circleButton != null)
        {
            panel.SetActive(false); // 패널 비활성화 
            NGUITools.SetActive(circleButton.gameObject, true); // 버튼 활성화
        }
        else
        {
            Debug.LogError("Panel or circle button is missing. Cannot perform OnCloseButtonClick action.");
        }
    }
}
