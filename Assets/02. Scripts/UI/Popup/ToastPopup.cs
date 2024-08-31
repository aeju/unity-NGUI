using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastPopup : PopupBase
{
    public float duration;

    public override void Show(string title)
    {
        gameObject.SetActive(true);
    }
    
    public override void Hide()
    {
        
    }
}
