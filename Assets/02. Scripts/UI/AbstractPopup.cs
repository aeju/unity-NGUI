using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPopup : MonoBehaviour, IPopup
{
    public abstract UILabel TitleLabel { get; protected set; }
    public abstract UILabel MessageLabel { get; protected set; }
    
    public abstract UIButton YesButton { get; protected set; }
    public abstract UIButton NoButton { get; protected set; }
    
    public abstract UILabel YesButtonLabel { get; protected set; }
    public abstract UILabel NoButtonLabel { get; protected set; }

    public bool IsVisible { get; protected set; }

    public abstract void Show();
    public abstract void Hide();
    
    protected virtual void Awake()
    {
        ValidateComponents();
    }

    protected void ValidateComponents()
    {
        if (TitleLabel == null) Debug.LogError($"{GetType().Name}: TitleLabel is not set");
        if (MessageLabel == null) Debug.LogError($"{GetType().Name}: MessageLabel is not set");
        if (YesButton == null) Debug.LogError($"{GetType().Name}: YesButton is not set");
        if (NoButton == null) Debug.LogError($"{GetType().Name}: NoButton is not set");
        if (YesButtonLabel == null) Debug.LogError($"{GetType().Name}: YesButtonLabel is not set");
        if (NoButtonLabel == null) Debug.LogError($"{GetType().Name}: NoButtonLabel is not set");
    }
}
