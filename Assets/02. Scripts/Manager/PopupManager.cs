using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : Singleton<PopupManager>
{
    [SerializeField] private GameObject popupPanel; // Inspector에서 할당할 팝업 패널
    
    [SerializeField] private GameObject toastPrefab;
    [SerializeField] private GameObject fullSizePrefab;
    [SerializeField] private GameObject yesNoPrefab;

    public Stack<GameObject> popupStack = new Stack<GameObject>();
    public int PopupCount => popupStack.Count;
    
    private bool isFullSizePopupOpen = false;
    
    public void ShowFullSizePopup(string title)
    {
        if (isFullSizePopupOpen)
        {
            return;
        }
        
        GameObject fullSizeObject = NGUITools.AddChild(popupPanel, fullSizePrefab);
        FullSizePopup fullSize = fullSizeObject.GetComponent<FullSizePopup>();
        fullSize.Show(title);
        popupStack.Push(fullSizeObject);
        isFullSizePopupOpen = true;
    }
    
    public void ShowYesNoPopup(string title, string content, System.Action onYes, System.Action onNo)
    {
        GameObject yesNoObject = NGUITools.AddChild(popupPanel, yesNoPrefab);
        YesNoPopup yesNo = yesNoObject.GetComponent<YesNoPopup>();
        yesNo.Show(title);
        yesNo.SetContent(content);
        yesNo.SetCallbacks(onYes, onNo);
        popupStack.Push(yesNoObject);
    }
    
    // 자동으로 사라짐 -> 스택에 넣을 필요 x 
    public void ShowToast(string message, float duration)
    {
        GameObject toastObject = NGUITools.AddChild(popupPanel, toastPrefab);
        ToastPopup toast = toastObject.GetComponent<ToastPopup>();
        if (toast != null)
        {
            toast.duration = duration;
            toast.Show(message);
        }
        else
        {
            Debug.LogError("ToastPopup component not found on instantiated prefab");
        }
    }

    public void CloseTopPopup()
    {
        Debug.Log("CloseTopPopup called. Current stack count: " + popupStack.Count);
        if (popupStack.Count > 0)
        {
            GameObject topPopup = popupStack.Pop();
            IPopup popupComponent = topPopup.GetComponent<IPopup>();
            if (popupComponent != null)
            {
                popupComponent.Hide();
            }
            
            // FullSizePopup이 닫힐 때 플래그를 false로 설정
            if (topPopup.GetComponent<FullSizePopup>() != null)
            {
                isFullSizePopupOpen = false;
                Debug.Log("isFullSizePopupOpen after: " + isFullSizePopupOpen);
            }
            
            Debug.Log("Destroying popup: " + topPopup.name);
            Destroy(topPopup);
        }
        else
        {
            Debug.LogWarning("Attempted to close popup, but stack is empty.");
        }
    }
}
