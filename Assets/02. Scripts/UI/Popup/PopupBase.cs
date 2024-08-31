using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PopupBase : MonoBehaviour, IPopup
{
    [SerializeField] protected UILabel TitleLabel;
    [SerializeField] bool canCloseWithEsc = true; // 뒤로가기로 닫을 수 있는지 여부 (toast : x) 
    
    public abstract void Show(string title);
    public abstract void Hide();
    
    protected virtual void Awake()
    {
        ValidateComponents();
    }
    
    protected void ValidateComponents()
    {
        if (TitleLabel == null)
        {
            Debug.LogError($"{GetType().Name}: TitleLabel is not set");
        }
    }

    protected virtual void SetTitle(string title)
    {
        if (TitleLabel != null)
        {
            TitleLabel.text = title;
        }
        else
        {
            Debug.LogWarning($"{GetType().Name}: Attempted to set title, but TitleLabel is null");
        }
    }
    
    protected virtual void Update()
    {
        if (canCloseWithEsc && Input.GetKeyDown(KeyCode.Escape))
        {
            Hide();
        }
    }
}
