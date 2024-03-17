using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private IntValueSO currentMoney;

    public void AddMoney(int moneyBoost)
    {
        currentMoney.Value += moneyBoost;
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
            Debug.Log("Ну нет столько денег!");
            return false;
        }
    }
}
