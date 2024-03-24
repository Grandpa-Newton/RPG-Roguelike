using App.Scripts.MixedScenes.Inventory.Model;
using App.Scripts.TraderScene;
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
                if(other.GetComponent<Coin>() != null)
                {
                    GetComponent<Money>().AddMoney(1);
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