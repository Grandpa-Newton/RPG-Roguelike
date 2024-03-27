using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace App.Scripts.MixedScenes
{
    [CreateAssetMenu(menuName ="Data/FloatData")]
    public class FloatValueSO : ScriptableObject
    {
        [SerializeField] private float currentValue;

        [SerializeField] private float _maxValue;
        public float CurrentValue
        {
            get => currentValue;
            set
            {
                currentValue = value;
                OnValueChange?.Invoke();
            }

        }
    
        public float MaxValue
        {
            get => _maxValue;
            set
            {
                _maxValue = value;
                OnValueChange?.Invoke();
            }

        }

        public bool IsInitialized;
        public event Action OnValueChange;

    }
}