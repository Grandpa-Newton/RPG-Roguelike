using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace App.Scripts.MixedScenes
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
