using System;
using App.Scripts.MixedScenes;
using App.Scripts.MixedScenes.Player.Interface;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerHealth : IHealth
{
    public float maxHealth { get; private set; }
    public FloatValueSO playerHealth { get; }
        
    public event Action OnPlayerHealthReduce;
    public event Action OnPlayerIncreaseHealth;
    public event Action<int> OnPlayerIncreaseMaxHealth;
    public event Action OnPlayerDie;

    public PlayerHealth(FloatValueSO playerHealth)
    {
        this.playerHealth = playerHealth;
    }
    public void IncreaseHealth(int healthToIncrease)
    {
        int health = Mathf.RoundToInt(playerHealth.CurrentValue * maxHealth);
        int val = health + healthToIncrease;
        playerHealth.CurrentValue = val > maxHealth ? 1 : val / maxHealth;
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
        if (!playerHealth.IsInitialized)
        {
            playerHealth.CurrentValue = playerHealth.MaxValue;
            playerHealth.IsInitialized = true;
        }
    }
    
}
