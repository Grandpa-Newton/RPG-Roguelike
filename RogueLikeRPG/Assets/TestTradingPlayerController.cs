using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTradingPlayerController : MonoBehaviour // � �����, ��� ����� ��� � InventoryController
{
    [SerializeField] private InventorySO inventoryData;

    public bool TryAddItem(ItemSO item)
    {
        int reminder = inventoryData.AddItem(item, 1);// ����� ��� ����� ����� ������� ���, ����� ������������ ��� �������� ���������� ��������� ��� �������
        if (reminder == 0)
            return true;
        else
            return false;
    }
}
