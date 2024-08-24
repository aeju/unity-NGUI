using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NGUIButtonHandler : MonoBehaviour
{
    void Start()
    {
        UIButton button = GetComponent<UIButton>();
        if (button != null)
        {
            UIEventListener.Get(gameObject).onClick += OnButtonClick;
        }
        else
        {
            Debug.LogError("UIButton component not found on this GameObject!");
        }
    }

    private void OnButtonClick(GameObject go)
    {
        Debug.Log("Button clicked: " + go.name);
        // 여기에 버튼 클릭 시 실행할 코드를 추가하세요
    }

    void OnDestroy()
    {
        // 이벤트 리스너 제거
        UIEventListener.Get(gameObject).onClick -= OnButtonClick;
    }
}
