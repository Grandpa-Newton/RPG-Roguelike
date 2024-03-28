using System;
using App.Scripts.MixedScenes;
using App.Scripts.MixedScenes.Player.Interface;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerHealth : IHealth
{
    public int maxHealth { get; private set; }
    public CharacteristicValueSO playerHealth { get; }
        
    public event Action OnPlayerHealthReduce;
    public event Action OnPlayerIncreaseHealth;
    public event Action<int> OnPlayerIncreaseMaxHealth;
    public event Action OnPlayerDie;

    public PlayerHealth(CharacteristicValueSO playerHealth)
    {
        this.playerHealth = playerHealth;
    }
    public void IncreaseHealth(int healthToIncrease)
    {
        int health = Mathf.RoundToInt(playerHealth.CurrentValue * maxHealth);
        int val = health + healthToIncrease;
        
        playerHealth.CurrentValue = val > maxHealth ? playerHealth.MaxValue : val / maxHealth;
        OnPlayerIncreaseHealth?.Invoke();
    }
    [Button]
    public void IncreaseMaxHealth(int maxHealthToIncrease)
    {
        maxHealth = maxHealthToIncrease;
        OnPlayerIncreaseMaxHealth?.Invoke(maxHealthToIncrease);
    }

    public void ReduceHealth(int healthToReduce)
    {
        OnPlayerHealthReduce?.Invoke();
        playerHealth.CurrentValue -= healthToReduce;
        Debug.Log(playerHealth.CurrentValue + "-=" + healthToReduce + "/" + playerHealth.MaxValue);
        Debug.Log(playerHealth.CurrentValue);
        if (playerHealth.CurrentValue <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        OnPlayerDie?.Invoke();
        playerHealth.CurrentValue = playerHealth.MaxValue;
    }
    
    public void InitializeHealth()
    {
        if (playerHealth.IsInitialized) return;
        
        playerHealth.CurrentValue = playerHealth.MaxValue;
        playerHealth.IsInitialized = true;
    }

    public void Dispose()
    {
        
    }
}
