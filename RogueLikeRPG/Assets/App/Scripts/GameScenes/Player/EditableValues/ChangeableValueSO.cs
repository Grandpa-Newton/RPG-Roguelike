using System;
using UnityEngine;

namespace App.Scripts.GameScenes.Player.EditableValues
{
    [CreateAssetMenu(menuName = "Data/IntData")]
    public class ChangeableValueSO : ScriptableObject
    {
       [SerializeField] private int currentValue;

        public int CurrentValue
        {
            get => currentValue;
            set
            {
                currentValue = value;
                OnValueChange?.Invoke(currentValue);
            }

        }

        public bool IsInitialized;
        public event Action<int> OnValueChange;
    }
}
