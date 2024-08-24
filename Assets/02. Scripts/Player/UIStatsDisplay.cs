using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStatsDisplay : MonoBehaviour
{
    public UISlider hpSlider;
    public UILabel hpLabel;
    
    public UISlider mpSlider;
    public UILabel mpLabel;

    public void UpdateHealthUI(int currentHealth, int maxHealth)
    {
        if (hpSlider != null)
        {
            hpSlider.sliderValue = (float)currentHealth / maxHealth;
        }
        
        if (hpLabel != null)
        {
            hpLabel.text = currentHealth.ToString();
        }
    }
    
    public void UpdateManaUI(int currentMana, int maxMana)
    {
        if (mpSlider != null)
        {
            mpSlider.sliderValue = (float)currentMana / maxMana;
        }
        
        if (mpLabel != null)
        {
            mpLabel.text = currentMana.ToString();
        }
    }
}
