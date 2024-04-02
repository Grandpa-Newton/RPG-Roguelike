using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

namespace App.Scripts.MixedScenes.Player
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private CharacteristicValueSO playerHealthSO;

        public event Action OnHealthReduce;
        
        private void Start()
        {
            if (!playerHealthSO.IsInitialized)
            {
                playerHealthSO.CurrentValue = playerHealthSO.MaxValue;
                playerHealthSO.IsInitialized = true;
            }
        }
        public void AddHealth(int healthBoost)
        {
            int health = Mathf.RoundToInt(playerHealthSO.CurrentValue * maxHealth);
            int val = health + healthBoost;
            playerHealthSO.CurrentValue = val > maxHealth ? 1 : val / maxHealth;
        }

        public void Reduce(int damage)
        {
            OnHealthReduce?.Invoke();
            playerHealthSO.CurrentValue -= damage / maxHealth;
            if (playerHealthSO.CurrentValue <= 0)
            {
                Die();
            }
        }
        private void Die()
        {
            Debug.Log("Died");
            playerHealthSO.CurrentValue = 50;
        }

    
    }
}