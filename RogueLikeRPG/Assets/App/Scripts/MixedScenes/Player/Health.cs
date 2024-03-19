using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private FloatValueSO currentHealth;

    private void Start()
    {
        if (!currentHealth.IsInitialized)
        {
            currentHealth.CurrentValue = 1;
            currentHealth.IsInitialized = true;
        }
    }
    public void AddHealth(int healthBoost)
    {
        int health = Mathf.RoundToInt(currentHealth.CurrentValue * maxHealth);
        int val = health + healthBoost;
        currentHealth.CurrentValue = val > maxHealth ? 1 : val / maxHealth;
    }

    public void Reduce(int damage)
    {
        currentHealth.CurrentValue -= damage / maxHealth;
        if (currentHealth.CurrentValue <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("Died");
        currentHealth.CurrentValue = 1;
    }

    
}