using System;
using System.Collections;
using System.Collections.Generic;
using App.Scripts.MixedScenes;
using App.Scripts.MixedScenes.Player.Interface;
using UnityEngine;

public class PlayerHealth : IHealth
{
    public float maxHealth { get; private set; }
    public FloatValueSO currentHealth { get; }
        
    public event Action OnPlayerHealthReduce;
    public event Action OnPlayerIncreaseHealth;
    public event Action OnPlayerDie;

    public PlayerHealth(FloatValueSO currentHealth)
    {
        this.currentHealth = currentHealth;
    }
    public void IncreaseHealth(int healthToIncrease)
    {
        int health = Mathf.RoundToInt(currentHealth.CurrentValue * maxHealth);
        int val = health + healthToIncrease;
        currentHealth.CurrentValue = val > maxHealth ? 1 : val / maxHealth;
        OnPlayerIncreaseHealth?.Invoke();
    }
    
    public void IncreaseMaxHealth(int maxHealthToIncrease)
    {
        maxHealth = maxHealthToIncrease;
    }

    public void ReduceHealth(int healthToReduce)
    {
        currentHealth.CurrentValue -= healthToReduce / maxHealth;
        OnPlayerHealthReduce?.Invoke();
        if (currentHealth.CurrentValue <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        OnPlayerDie?.Invoke();
        currentHealth.CurrentValue = 1;
    }
    
    public void InitializeHealth()
    {
        if (!currentHealth.IsInitialized)
        {
            currentHealth.CurrentValue = 1;
            currentHealth.IsInitialized = true;
        }
    }
    
}
