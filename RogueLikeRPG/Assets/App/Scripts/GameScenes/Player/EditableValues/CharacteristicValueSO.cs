using System;
using UnityEngine;

namespace App.Scripts.GameScenes.Player.EditableValues
{
    [CreateAssetMenu(menuName ="Data/FloatData")]
    public class CharacteristicValueSO : ScriptableObject
    {
        [SerializeField] private int currentValue;

        [SerializeField] private int _maxValue;
        public int CurrentValue
        {
            get => currentValue;
            set
            {
                currentValue = value;
                OnValueChange?.Invoke();
            }

        }
    
        public int MaxValue
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