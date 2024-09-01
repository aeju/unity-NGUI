using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPopupSystem : MonoBehaviour
{
    [System.Serializable]
    public class ButtonPopupPair
    {
        public UIButton button;
        public Definitions.PopupType popupType;
    }

    public ButtonPopupPair[] PopupInfos;
    public PopupManager popupManager;
}
