using App.Scripts.GameScenes.Inventory.Model;
using App.Scripts.GameScenes.Player.Components;
using App.Scripts.MixedScenes.PickUpSystem;
using UnityEngine;

namespace App.Scripts.GameScenes.Inventory.PickUpSystem
{
    public class PlayerPickUpSystem : MonoBehaviour
    {
        [SerializeField] private InventorySO inventoryData;
        private void OnTriggerEnter2D(Collider2D other)
        {
            ItemPickable item = other.GetComponent<ItemPickable>();
            if (item != null)
            {
                if(item.ItemType == ItemType.Coin)
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