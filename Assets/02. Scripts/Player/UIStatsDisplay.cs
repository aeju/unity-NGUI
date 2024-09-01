using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStatsDisplay : MonoBehaviour
{
    public UISlider hpSlider;
    public UILabel hpLabel;
    
    public UISlider mpSlider;
    public UILabel mpLabel;

    public UISlider expSlider;
    public UILabel expLable;

    public UILabel combatPower; // 공격력
    
    public void UpdateHealthUI(int currentHealth, int maxHealth)
    {
        if (hpSlider != null)
        {
            hpSlider.sliderValue = (float)currentHealth / maxHealth;
        }
        
        if (hpLabel != null)
        {
            // hpLabel.text = currentHealth.ToString();
            hpLabel.ToStringFormattedNumber(currentHealth);
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
            // mpLabel.text = currentMana.ToString();
            mpLabel.ToStringFormattedNumber(currentMana);
        }
    }
}
