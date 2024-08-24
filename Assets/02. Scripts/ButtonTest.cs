using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTest : MonoBehaviour
{
    public UIButton testButton;

    private void Start()
    {
        if (testButton == null)
        {
            testButton = GetComponent<UIButton>();
            if (testButton == null)
            {
                Debug.LogError("UIButton component not found! Please assign it in the inspector or attach this script to a GameObject with a UIButton.");
                return;
            }
        }
    }

    private void OnEnable()
    {
        if (testButton != null)
        {
            testButton.GetComponent<UIButtonMessage>().target = gameObject;
            testButton.GetComponent<UIButtonMessage>().functionName = "OnButtonClicked";
        }
    }

    private void OnDisable()
    {
        if (testButton != null)
        {
            testButton.GetComponent<UIButtonMessage>().target = null;
            testButton.GetComponent<UIButtonMessage>().functionName = "";
        }
    }

    private void OnButtonClicked()
    {
        Debug.Log("NGUI 버튼이 클릭되었습니다!");
    }
}
