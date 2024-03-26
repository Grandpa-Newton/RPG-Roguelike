using System.Collections;
using System.Collections.Generic;
using App.Scripts.MixedScenes;
using UnityEngine;

public interface IHealth {
    public float maxHealth { get; }
    public FloatValueSO currentHealth { get; }


    public void IncreaseHealth(int healthToIncrease);
    public void IncreaseMaxHealth(int maxHealthToIncrease);
    public void ReduceHealth(int healthToReduce);
    public void Die();
    public void InitializeHealth();
    
}
