using System;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.DungeonScene.Items;
using UnityEngine;

namespace App.Scripts.GameScenes.Inventory.Model
{
    [CreateAssetMenu(fileName = "Inventory_", menuName = "InventorySO")]
    public class InventorySO : ScriptableObject
    {
        [SerializeField] public List<InventoryItem> inventoryItems;

        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;
        [field: SerializeField] public int Size { get; private set; }

        public int AddItem(ItemSO item, int quantity, List<ItemParameter> itemState = null)
        {
            if (item.IsStackable == false)
            {
                    while (quantity > 0 && !IsInventoryFull())
                    {
                       quantity -= AddItemToFirstFreeSlot(item, 1, itemState);
                    }
                    InformAboutChanges();
                    return quantity;
                
            }

            quantity = AddStackableItem(item, quantity);
            InformAboutChanges();
            return quantity;
        }

        private int AddItemToFirstFreeSlot(ItemSO item, int quantity, List<ItemParameter> itemState = null)
        {
            InventoryItem newItem = new InventoryItem()
            {
                item = item,
                quantity = quantity,
                itemState =  new List<ItemParameter>(itemState ?? item.DefaultParametersList)
            };

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    inventoryItems[i] = newItem;
                    return quantity;
                }
            }

            return 0;
        }

        private bool IsInventoryFull()
        {
            return inventoryItems.Where(item => item.IsEmpty).Any() == false;
        }

        private int AddStackableItem(ItemSO item, int quantity)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    continue;
                }

                if (inventoryItems[i].item.ID == item.ID)
                {
                    int amountPossibleToTake = inventoryItems[i].item.MaxStackSize - inventoryItems[i].quantity;

                    if (quantity > amountPossibleToTake)
                    {
                        inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].item.MaxStackSize);
                        quantity -= amountPossibleToTake;
                    }
                    else
                    {
                        inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].quantity + quantity);
                        InformAboutChanges();
                        return 0;
                    }
                }
            }

            while (quantity > 0 && !IsInventoryFull())
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot(item, newQuantity);
            }

            return quantity;
        }

        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                    continue;
                returnValue[i] = inventoryItems[i];
            }

            return returnValue;
        }

        public InventoryItem GetItemAt(int itemIndex)
        {
            return inventoryItems[itemIndex];
        }

        public void SwapItems(int itemIndex1, int itemIndex2)
        {
            if (itemIndex1 < 0 || itemIndex1 >= inventoryItems.Count || itemIndex2 < 0 || itemIndex2 >= inventoryItems.Count)
            {
                Debug.LogError("Invalid item index");
                return;
            }
            (inventoryItems[itemIndex1], inventoryItems[itemIndex2]) =
                (inventoryItems[itemIndex2], inventoryItems[itemIndex1]);
            InformAboutChanges();
        }

        private void InformAboutChanges()
        {
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }

        public void RemoveItem(int itemIndex, int amount)
        {
            if (inventoryItems.Count > itemIndex)
            {
                if (inventoryItems[itemIndex].IsEmpty)
                    return;
                int reminder = inventoryItems[itemIndex].quantity - amount;
                if (reminder <= 0)
                    inventoryItems[itemIndex] = InventoryItem.GetEmptyItem();
                else
                {
                    inventoryItems[itemIndex] = inventoryItems[itemIndex].ChangeQuantity(reminder);
                }
                
                InformAboutChanges();
            }
        }
    }

    [Serializable]
    public struct InventoryItem
    {
        public int quantity;
        public ItemSO item;
        public List<ItemParameter> itemState;
        public bool IsEmpty => item == null;

        public InventoryItem ChangeQuantity(int newQuantity)
        {
            return new InventoryItem()
            {
                item = this.item,
                quantity = newQuantity,
                itemState =  new List<ItemParameter>(this.itemState),
            };
        }

        public static InventoryItem GetEmptyItem() => new InventoryItem()
        {
            item = null, 
            quantity = 0, 
            itemState = new List<ItemParameter>(),
            
        };
    }
}