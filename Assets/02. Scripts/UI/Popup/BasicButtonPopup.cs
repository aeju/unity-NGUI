using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicButtonPopup : MonoBehaviour
{
    public GameObject popupPanel;
    public UIButton button; // Inspector에서 할당할 NGUI 버튼
    public GameObject popupPrefab; // Inspector에서 할당할 팝업 패널
    private GameObject currentPopup; // 현재 생성된 팝업 인스턴스

    void Start()
    {
        if (button != null)
        {
            // UIButton의 게임오브젝트에 EventListener 컴포넌트 추가 (없는 경우)
            UIEventListener listener = UIEventListener.Get(button.gameObject);
            
            // 클릭 이벤트에 메서드 연결
            listener.onClick += OnButtonClick;
        }
        else
        {
            Debug.LogError("Button is not assigned in the inspector!");
        }
    }

    void OnButtonClick(GameObject go)
    {
        // 팝업 패널을 활성화
        if (popupPrefab != null)
        {
            // 팝업 인스턴스화
            if (currentPopup == null)
            {
                currentPopup = NGUITools.AddChild(popupPanel, popupPrefab);
            }
            else
            {
                currentPopup.SetActive(true);
            }
        }
        else
        {
            Debug.LogError("Popup panel is not assigned in the inspector!");
        }
    }

    // 팝업을 닫는 메서드 (필요한 경우 사용)
    public void ClosePopup()
    {
        if (currentPopup != null)
        {
            currentPopup.SetActive(false);
        }
    }

    void OnDestroy()
    {
        // 메모리 누수 방지를 위해 이벤트 리스너 제거
        if (button != null)
        {
            UIEventListener listener = button.GetComponent<UIEventListener>();
            if (listener != null)
            {
                listener.onClick -= OnButtonClick;
            }
        }
        
        // 팝업 인스턴스 제거
        if (currentPopup != null)
        {
            Destroy(currentPopup);
        }
    }
}
