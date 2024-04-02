using System;
using System.Collections;
using System.Collections.Generic;
using App.Scripts.MixedScenes;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCurrencyUI : MonoBehaviour
{
    [SerializeField] private TMP_Text currentCurrency;
    [SerializeField] private GameObject moneyPanel;
    [FormerlySerializedAs("_intValueSO")] [SerializeField] private ChangeableValueSO changeableValueSO; 
    
    private void Awake()
    {
        changeableValueSO.OnValueChange += UpdatePlayerCurrentCurrency;
    }

    private void Start()
    {
        UpdatePlayerCurrentCurrency(changeableValueSO.CurrentValue);
    }

    private void UpdatePlayerCurrentCurrency(int money)
    {
        currentCurrency.text = money.ToString();
    }
}
