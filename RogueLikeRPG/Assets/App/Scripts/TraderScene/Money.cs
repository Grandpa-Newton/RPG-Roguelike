using App.Scripts.MixedScenes;
using UnityEngine;

namespace App.Scripts.TraderScene
{
    public class Money 
    {
        private readonly IntValueSO currentMoney;

        public Money(IntValueSO currentMoney)
        {
            this.currentMoney = currentMoney;
        }
        public void AddMoney(int moneyBoost)
        {
            currentMoney.Value += moneyBoost;
        }

        public bool CanAffordReduceMoney(int reducingMoney)
        {

            if (currentMoney.Value >= reducingMoney)
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
            if(currentMoney.Value >= reducingMoney)
            {
                currentMoney.Value -= reducingMoney;
                return true;
            }
            else
            {
                throw new System.Exception("Player cannot afford this!!");
            }
        }
    }
}
