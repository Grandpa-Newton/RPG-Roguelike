using System;
using System.Collections;
using System.Collections.Generic;
using App.Scripts.MixedScenes;
using TMPro;
using UnityEngine;

public class PlayerCurrencyUI : MonoBehaviour
{
    [SerializeField] private TMP_Text currentCurrency;
    [SerializeField] private GameObject moneyPanel;
    [SerializeField] private IntValueSO _intValueSO; 
    
    private void Awake()
    {
        _intValueSO.OnValueChange += UpdatePlayerCurrentCurrency;
    }

    private void Start()
    {
        UpdatePlayerCurrentCurrency(_intValueSO.Value);
    }

    private void UpdatePlayerCurrentCurrency(int money)
    {
        currentCurrency.text = money.ToString();
    }
}
