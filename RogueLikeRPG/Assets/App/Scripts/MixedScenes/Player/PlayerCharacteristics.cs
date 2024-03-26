using System;
using UnityEngine;

namespace App.Scripts.MixedScenes.Player
{
    public class PlayerCharacteristics : MonoBehaviour, IHealth
    {
        public float maxHealth { get; private set; }
        public FloatValueSO currentHealth { get; private set; }
        
        public event Action OnHealthReduce;
        
        public void IncreaseHealth(int healthToIncrease)
        {
            int health = Mathf.RoundToInt(currentHealth.CurrentValue * maxHealth);
            int val = health + healthToIncrease;
            currentHealth.CurrentValue = val > maxHealth ? 1 : val / maxHealth;
        }

        public void IncreaseMaxHealth(int maxHealthToIncrease)
        {
            maxHealth = maxHealthToIncrease;
        }

        public void ReduceHealth(int healthToReduce)
        {
            OnHealthReduce?.Invoke();
            currentHealth.CurrentValue -= healthToReduce / maxHealth;
            if (currentHealth.CurrentValue <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
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
}
