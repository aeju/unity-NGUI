using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 300;
    public int currentHealth;

    public int maxMana = 100;
    public int currentMana;
    
    public int attack = 10;
    public int defense = 5;
    
    public float moveSpeed = 2f;
    public float jumpForce = 4f;

    public int level = 1;
    public float exp = 0;
    
    public event System.Action<int, int> OnHealthChanged;
    public event System.Action<int, int> OnManaChanged;
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void UseMana(int amount)
    {
        currentMana -= amount;
        OnManaChanged?.Invoke(currentMana, maxMana);
    }
}
