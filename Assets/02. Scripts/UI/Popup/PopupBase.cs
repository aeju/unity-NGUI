using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PopupBase : MonoBehaviour, IPopup
{
    public UILabel TitleLabel;
    
    public abstract void Show(string title);
    public abstract void Hide();
    
    protected virtual void Awake()
    {
        ValidateComponents();
    }
    
    protected void ValidateComponents()
    {
        if (TitleLabel == null) Debug.LogError($"{GetType().Name}: TitleLabel is not set");
    }

    protected virtual void SetTitle(string title)
    {
        if (TitleLabel != null)
        {
            TitleLabel.text = title;
        }
    }
}
