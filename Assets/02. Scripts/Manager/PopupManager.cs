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
    private bool isFullSizePopupOpen = false;
    
    public int PopupCount => popupStack.Count;
    
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

    // fullSize부터 
    public void ShowYesNoPopup(string title, string content, System.Action onYes, System.Action onNo)
    {
        GameObject yesNoObject = NGUITools.AddChild(popupPanel, yesNoPrefab);
        YesNoPopup yesNo = yesNoObject.GetComponent<YesNoPopup>();
        //yesNo.SetContent(title, content);
        //yesNo.SetCallbacks(onYes, onNo);
        //yesNo.Show(title);
        popupStack.Push(yesNoObject);
    }
    
    public void ShowToast(string message, float duration)
    {
        GameObject toastObject = NGUITools.AddChild(popupPanel, toastPrefab);
        ToastPopup toast = toastObject.GetComponent<ToastPopup>();
        //toast.Show(title);
        //StartCoroutine(HideAfterDuration(toast, duration));
    }
    

    public void CloseTopPopup()
    {
        if (popupStack.Count > 0)
        {
            GameObject topPopup = popupStack.Pop();
            
            // FullSizePopup이 닫힐 때 플래그를 false로 설정
            if (topPopup.GetComponent<FullSizePopup>() != null)
            {
                isFullSizePopupOpen = false;
            }
            
            Debug.Log("Destroy");
            Destroy(topPopup);
        }
    }
}
