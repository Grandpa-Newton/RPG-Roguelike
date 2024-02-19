using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private FloatValueSO currentHealth;
    [SerializeField] private Slider healthBar;

    private void Start()
    {
        if (!currentHealth.IsInitialized)
        {
            currentHealth.Value = 1;
            currentHealth.IsInitialized = true;
        }
    }
    public void AddHealth(int healthBoost)
    {
        int health = Mathf.RoundToInt(currentHealth.Value * maxHealth);
        int val = health + healthBoost;
        currentHealth.Value = val > maxHealth ? 1 : val / maxHealth;
    }

    public void Reduce(int damage)
    {
        currentHealth.Value -= damage / maxHealth;
        if (currentHealth.Value <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("Died");
        currentHealth.Value = 1;
    }

    
}