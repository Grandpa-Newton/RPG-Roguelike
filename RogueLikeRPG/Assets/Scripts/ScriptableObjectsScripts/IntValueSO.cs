using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/IntData")]
public class IntValueSO : ScriptableObject
{
    [SerializeField]
    private int _value;

    public int Value
    {
        get => _value;
        set
        {
            _value = value;
            OnValueChange?.Invoke(_value);
        }

    }

    public bool IsInitialized;
    public event Action<int> OnValueChange;
}
