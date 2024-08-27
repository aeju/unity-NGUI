using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YesNoPopup : MonoBehaviour
{
    public UILabel titleLabel;
    public UILabel messageLabel;
    
    public UIButton yesButton;
    public UIButton noButton;
    
    public UILabel yesButtonLabel;
    public UILabel noButtonLabel;
    
    public void Start()
    {
        if (titleLabel == null)
            Debug.LogError("YesNoPopup: titleLabel is not assigned");
        if (messageLabel == null)
            Debug.LogError("YesNoPopup: messageLabel is not assigned");
        if (yesButton == null)
            Debug.LogError("YesNoPopup: yesButton is not assigned");
        if (noButton == null)
            Debug.LogError("YesNoPopup: noButton is not assigned");
        if (yesButtonLabel == null)
            Debug.LogError("YesNoPopup: yesButtonLabel is not assigned");
        if (noButtonLabel == null)
            Debug.LogError("YesNoPopup: noButtonLabel is not assigned");
    }
}
