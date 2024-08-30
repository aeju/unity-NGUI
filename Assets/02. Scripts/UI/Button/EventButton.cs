using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventButton : MonoBehaviour
{
    public UIButton circleButton;
    
    // 나중 : 둘 중 하나로
    public UISprite roundedRectSprite;
    public GameObject pannel;

    void Start()
    {
        // 초기 상태 설정
        if (roundedRectSprite != null)
        {
            // roundedRectSprite.enabled = false;
            
            pannel.SetActive(false);
        }
        
        // 버튼 클릭 이벤트 등록
        if (circleButton != null)
        {
            UIEventListener.Get(circleButton.gameObject).onClick += OnCircleButtonClick;
        }
    }
    
    void OnCircleButtonClick(GameObject go)
    {
        if (roundedRectSprite != null)
        {
            // 스프라이트 활성화
            roundedRectSprite.enabled = true;
            pannel.SetActive(true);
            
            // 기존 버튼 비활성화 
            circleButton.gameObject.SetActive(false);
        }
    }
}
