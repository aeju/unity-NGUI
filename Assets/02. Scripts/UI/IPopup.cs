using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPopup
{
    void Show();
    void Hide();
    
    bool IsVisible { get; }
}
