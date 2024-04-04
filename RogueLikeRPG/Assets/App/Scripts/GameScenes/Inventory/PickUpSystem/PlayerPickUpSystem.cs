using System;
using App.Scripts.GameScenes.Player.Components;
using App.Scripts.MixedScenes.Inventory.Model;
using App.Scripts.MixedScenes.PickUpSystem;
using UnityEngine;

namespace App.Scripts.MixedScenes.Player.Controller
{
    public class PlayerPickUpSystem : MonoBehaviour
    {
        [SerializeField] private InventorySO inventoryData;
        private void OnTriggerEnter2D(Collider2D other)
        {
            ItemPickable item = other.GetComponent<ItemPickable>();
            if (item != null)
            {
                if(other.GetComponent<Coin>() != null)
                {
                    PlayerMoney.Instance.AddMoney(1);
                    item.DestroyItem();
                    return;
                }
                int reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
                if (reminder == 0)
                    item.DestroyItem();
                else
                    item.Quantity = reminder;
            }
        }
    }
}