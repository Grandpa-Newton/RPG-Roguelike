using System.Collections;
using System.Collections.Generic;
using App.Scripts.GameScenes.Player.EditableValues;
using App.Scripts.MixedScenes;
using App.Scripts.TraderScene;
using UnityEngine;

public class TraderMoney : Money
{
    private static TraderMoney _instance;
    public static TraderMoney Instance => _instance ??= new TraderMoney();
    
    public override void Initialize(ChangeableValueSO currentMoney)
    {
        base.CurrentMoney = currentMoney;
    }
    public override void AddMoney(int moneyBoost)
    {
        CurrentMoney.CurrentValue += moneyBoost;
    }

    public override bool CanAffordReduceMoney(int reducingMoney)
    {

        if (CurrentMoney.CurrentValue >= reducingMoney)
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
        if(CurrentMoney.CurrentValue >= reducingMoney)
        {
            CurrentMoney.CurrentValue -= reducingMoney;
            return true;
        }
        else
        {
            throw new System.Exception("Player cannot afford this!!");
        }
    }
}
