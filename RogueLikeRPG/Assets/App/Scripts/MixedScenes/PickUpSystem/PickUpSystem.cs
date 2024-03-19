using App.Scripts.MixedScenes.Inventory.Model;
using UnityEngine;

namespace App.Scripts.MixedScenes.PickUpSystem
{
    public class PickUpSystem : MonoBehaviour
    {
        [SerializeField] private InventorySO inventoryData;

        private void OnTriggerEnter2D(Collider2D other)
        {
            ItemPickable item = other.GetComponent<ItemPickable>();
            if (item != null)
            {
                int reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
                if (reminder == 0)
                    item.DestroyItem();
                else
                    item.Quantity = reminder;
            }
        }
    }
}