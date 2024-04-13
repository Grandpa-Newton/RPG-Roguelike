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
        base.currentMoney = currentMoney;
    }
    public override void AddMoney(int moneyBoost)
    {
        currentMoney.CurrentValue += moneyBoost;
    }

    public override bool CanAffordReduceMoney(int reducingMoney)
    {

        if (currentMoney.CurrentValue >= reducingMoney)
        {
            return true;
        }
        else
        {
            Debug.Log("�� ��� ������� �����!");
            return false;
        }
    }

    public override bool TryReduceMoney(int reducingMoney)
    {
        if(currentMoney.CurrentValue >= reducingMoney)
        {
            currentMoney.CurrentValue -= reducingMoney;
            return true;
        }
        else
        {
            throw new System.Exception("Player cannot afford this!!");
        }
    }
}
