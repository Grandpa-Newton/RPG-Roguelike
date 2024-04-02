using System.Collections;
using System.Collections.Generic;
using App.Scripts.MixedScenes;
using UnityEngine;

public class PlayerMoney
{
    private static PlayerMoney _instance;
    public static PlayerMoney Instance => _instance ??= new PlayerMoney();
        
    private IntValueSO _currentMoney;

    public void Initialize(IntValueSO currentMoney)
    {
        _currentMoney = currentMoney;
    }
    public void AddMoney(int moneyBoost)
    {
        Debug.Log(_currentMoney);
        Debug.Log(_currentMoney.CurrentValue);
        _currentMoney.CurrentValue += moneyBoost;
    }

    public bool CanAffordReduceMoney(int reducingMoney)
    {

        if (_currentMoney.CurrentValue >= reducingMoney)
        {
            return true;
        }
        else
        {
            Debug.Log("Ну нет столько денег!");
            return false;
        }
    }

    public bool TryReduceMoney(int reducingMoney)
    {
        if(_currentMoney.CurrentValue >= reducingMoney)
        {
            _currentMoney.CurrentValue -= reducingMoney;
            return true;
        }
        else
        {
            throw new System.Exception("Player cannot afford this!!");
        }
    }
}
