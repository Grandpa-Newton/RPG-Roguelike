using App.Scripts.MixedScenes;
using UnityEngine;

namespace App.Scripts.TraderScene
{
    public class Money : MonoBehaviour
    {
        [SerializeField] private IntValueSO currentMoney;

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
                Debug.Log("�� ��� ������� �����!");
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