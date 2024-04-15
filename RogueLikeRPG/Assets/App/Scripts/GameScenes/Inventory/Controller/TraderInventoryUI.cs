using App.Scripts.GameScenes.Inventory.Model;
using App.Scripts.GameScenes.Inventory.Model.ItemParameters;
using App.Scripts.GameScenes.Player.Components;
using App.Scripts.MixedScenes.Inventory.Model;
using App.Scripts.MixedScenes.Inventory.Model.ItemParameters;
using UnityEngine;

namespace App.Scripts.GameScenes.Inventory.Controller
{
    public sealed class TraderInventoryUI : AbstractInventoryUI
    {
        private static TraderInventoryUI _instance;
        public static TraderInventoryUI Instance => _instance ??= new TraderInventoryUI();

        protected override void PrepareUI()
        {
            InventoryUI.InitializeInventoryUI(InventoryData.Size);
            InventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            InventoryUI.OnItemActionRequested += HandleItemActionRequested;

            InventoryData.OnInventoryUpdated += UpdateInventoryUI;
        }

        private void HandleItemActionRequested(int itemIndex)
        {
            InventoryItem inventoryItem = InventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            if (inventoryItem.item is IDestroyableItem)
            {
                InventoryUI.ShowItemAction(itemIndex);
                InventoryUI.AddAction("Buy", () => BuyItem(inventoryItem, itemIndex));
            }
        }

        private void BuyItem(InventoryItem inventoryItem, int itemIndex)
        {
            if (!TryBuyItem(inventoryItem)) return;

            if (inventoryItem.item is IDestroyableItem)
            {
                InventoryData.RemoveItem(itemIndex, 1);
            }

            //audioSource.PlayOneShot(itemAction.itemActionSound);
            //audioSource.PlayOneShot(dropClip); // тут мб другой звук
            if (InventoryData.GetItemAt(itemIndex).IsEmpty)
                InventoryUI.ResetSelection();
        }

        private bool TryBuyItem(InventoryItem inventoryItem)
        {
            var itemSO = inventoryItem.item;

            Debug.Log("Sell Cost = " + itemSO.ItemBuyCost);

            if (PlayerMoney.Instance.CanAffordReduceMoney(itemSO.ItemBuyCost)) // тут тоже, наверное, нужно количество
            {
                Debug.Log("Player can afford it");
                if (PlayerInventoryUI.Instance.TryAddItem(itemSO)) // сюда нужно будет количество передавать
                {
                    PlayerMoney.Instance.TryReduceMoney(itemSO.ItemBuyCost);
                    TraderMoney.Instance.AddMoney(itemSO.ItemBuyCost);
                    return true;
                }
                Debug.Log("Player doesn't have enough space in inventory");
                return false;
            }

            Debug.Log("Player can't afford it.");
            return false;
        }

        public void Dispose()
        {
            InventoryData.OnInventoryUpdated -= UpdateInventoryUI;
            InventoryUI.OnDescriptionRequested -= HandleDescriptionRequest;
            InventoryUI.OnItemActionRequested -= HandleItemActionRequested;
        }
    }
}