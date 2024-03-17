using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTradingPlayerController : MonoBehaviour // я думаю, что потом это в InventoryController
{
    [SerializeField] private InventorySO inventoryData;

    public bool TryAddItem(ItemSO item)
    {
        int reminder = inventoryData.AddItem(item, 1);// потом тут нужно будет сделать так, чтобы пользователь мог выбирать количество предметов для покупки
        if (reminder == 0)
            return true;
        else
            return false;
    }
}
