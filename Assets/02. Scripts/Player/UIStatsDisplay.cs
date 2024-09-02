using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStatsDisplay : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats; 
    
    public UISlider hpSlider;
    public UILabel hpLabel;
    
    public UISlider mpSlider;
    public UILabel mpLabel;

    public UISlider expSlider;
    public UILabel expLabel;
    public UILabel levelLabel;

    public UILabel combatPower; // 공격력

    private void Start()
    {
        if (playerStats != null)
        {
            playerStats.OnHealthChanged += UpdateHealthUI;
            playerStats.OnManaChanged += UpdateManaUI;
            playerStats.OnExpChanged += UpdateExpUI;
        }
        else
        {
            Debug.LogError("PlayerStats is null");
        }
    }

    public void UpdateExpUI(float totalExp, int level)
    {
        if (expSlider != null)
        {
            expSlider.sliderValue = Calculator.CalculateExpPercentage(totalExp);
        }
        
        if (expLabel != null)
        {
            expLabel.text = $"{(expSlider.sliderValue * 100):F1}%";
        }

        if (levelLabel != null)
        {
            levelLabel.text = level.ToString();
        }
    }
    
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
