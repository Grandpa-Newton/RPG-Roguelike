using App.Scripts.MixedScenes;
using UnityEngine;

namespace App.Scripts.TraderScene
{
    public class Money1 
    {
        private static Money1 _instance;
        public static Money1 Instance => _instance ??= new Money1();
        
        private IntValueSO _currentMoney;

        /*public Money(IntValueSO currentMoney)
        {
            this.currentMoney = currentMoney;
        }*/

        public void Initialize(IntValueSO currentMoney)
        {
            _currentMoney = currentMoney;
        }
        public void AddMoney(int moneyBoost)
        {
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
}
