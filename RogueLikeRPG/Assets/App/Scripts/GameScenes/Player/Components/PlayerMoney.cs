using App.Scripts.GameScenes.Player.EditableValues;
using App.Scripts.TraderScene;
using UnityEngine;

namespace App.Scripts.GameScenes.Player.Components
{
    public class PlayerMoney : Money
    {
        private static PlayerMoney _instance;
        public static PlayerMoney Instance => _instance ??= new PlayerMoney();

        public override void Initialize(ChangeableValueSO currentMoney)
        {
            CurrentMoney = currentMoney;
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
}
