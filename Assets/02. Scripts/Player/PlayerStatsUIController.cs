using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerStatsUIController : MonoBehaviour
{
    public PlayerStats playerStats;
    
    public UISlider hpSlider;
    public UILabel currentHpLabel;
    
    public UISlider mpSlider;
    public UILabel currentMpLabel;

    private void Start()
    {
        if (playerStats == null)
        {
            playerStats = GetComponent<PlayerStats>();
        }

        if (playerStats == null)
        {
            Debug.LogError("PlayerStats component not found!");
            return;
        }

        UpdateUI();
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        // Update HP UI
        if (hpSlider != null)
        {
            hpSlider.sliderValue = (float)playerStats.currentHealth / playerStats.maxHealth;
        }
        
        if (currentHpLabel != null)
        {
            currentHpLabel.text = playerStats.currentHealth.ToString();
        }

        // Update MP UI
        if (mpSlider != null)
        {
            mpSlider.sliderValue = (float)playerStats.currentMana / playerStats.maxMana;
        }
        
        if (currentMpLabel != null)
        {
            currentMpLabel.text = playerStats.currentMana.ToString();
        }
    }
}
