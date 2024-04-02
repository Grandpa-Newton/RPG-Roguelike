using App.Scripts.MixedScenes;
using App.Scripts.TraderScene;
using UnityEngine;

public class PlayerMoney : Money
{
    private static PlayerMoney _instance;
    public static PlayerMoney Instance => _instance ??= new PlayerMoney();

    public override void Initialize(ChangeableValueSO currentMoney)
    {
        _currentMoney = currentMoney;
    }
    public override void AddMoney(int moneyBoost)
    {
        Debug.Log(_currentMoney);
        Debug.Log(_currentMoney.CurrentValue);
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
