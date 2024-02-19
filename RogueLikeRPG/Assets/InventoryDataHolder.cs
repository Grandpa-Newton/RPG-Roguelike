using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

public class InventoryDataHolder : MonoBehaviour
{
    public InventorySO inventoryData;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
