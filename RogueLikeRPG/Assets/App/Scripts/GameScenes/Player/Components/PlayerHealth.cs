using System;
using App.Scripts.GameScenes.Player.EditableValues;
using App.Scripts.GameScenes.Player.Interfaces;
using App.Scripts.MixedScenes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.GameScenes.Player.Components
{
    public class PlayerHealth : IHealth
    {
        private static PlayerHealth _instance;

        public static PlayerHealth Instance => _instance ??= new PlayerHealth();
        public int maxHealth { get; private set; }
        public CharacteristicValueSO playerHealth { get; private set; }
            
        public event Action OnPlayerHealthReduce;
        public event Action OnPlayerIncreaseHealth;
        public event Action<int> OnPlayerIncreaseMaxHealth;
        public event Action OnPlayerDie;

        public void Initialize(CharacteristicValueSO health)
        {
            this.playerHealth = health;
        }
        
        public void IncreaseHealth(int healthToIncrease)
        {
            int val = playerHealth.CurrentValue + healthToIncrease;
            
            playerHealth.CurrentValue = val > playerHealth.MaxValue ? playerHealth.MaxValue : val;
            //OnPlayerIncreaseHealth?.Invoke();
        }
        public void IncreaseMaxHealth(int maxHealthToIncrease)
        {
            maxHealth = maxHealthToIncrease;
            OnPlayerIncreaseMaxHealth?.Invoke(maxHealthToIncrease);
        }

        public void ReduceHealth(int healthToReduce)
        {
            OnPlayerHealthReduce?.Invoke();
            playerHealth.CurrentValue -= healthToReduce;
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
}
