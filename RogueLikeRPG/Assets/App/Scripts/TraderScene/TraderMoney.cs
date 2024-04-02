using System.Collections;
using System.Collections.Generic;
using App.Scripts.MixedScenes;
using App.Scripts.TraderScene;
using UnityEngine;

public class TraderMoney : Money
{
    private static TraderMoney _instance;
    public static TraderMoney Instance => _instance ??= new TraderMoney();
    
    public override void Initialize(IntValueSO currentMoney)
    {
        _currentMoney = currentMoney;
    }
    public override void AddMoney(int moneyBoost)
    {
        _currentMoney.CurrentValue += moneyBoost;
    }

    public override bool CanAffordReduceMoney(int reducingMoney)
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

    public override bool TryReduceMoney(int reducingMoney)
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
