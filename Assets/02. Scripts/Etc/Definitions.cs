using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Definitions 
{
    public enum ButtonType
    {
        Settings,
        Inventory,
        YesNo,
        Quest
    }
    
    public enum PopupType
    {
        Toast,
        YesNo,
        FullSize,
    }

    public enum QuestType
    {
        Normal,
        Party
    }

    // 배터리 슬라이더 색상 
    public enum BatterySliderColorStatus
    {
        Low, // ~20 : red
        Medium, // ~40 : yellow
        High // ~100 : green 
    }
    
    public enum TooltipType
    {
        First,
        Second
    }
}
