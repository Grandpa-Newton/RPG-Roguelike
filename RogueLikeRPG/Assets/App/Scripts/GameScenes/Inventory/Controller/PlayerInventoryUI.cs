using App.Scripts.GameScenes.Inventory.Model;
using App.Scripts.GameScenes.Inventory.Model.ItemParameters;
using App.Scripts.GameScenes.Player;
using App.Scripts.GameScenes.Player.Components;
using App.Scripts.MixedScenes.Inventory.Model;
using App.Scripts.MixedScenes.Inventory.Model.ItemParameters;
using App.Scripts.TraderScene;
using UnityEngine;

namespace App.Scripts.GameScenes.Inventory.Controller
{
    public sealed class PlayerInventoryUI : AbstractInventoryUI
    {
        private static PlayerInventoryUI _instance;
        public static PlayerInventoryUI Instance => _instance ??= new PlayerInventoryUI();

        private bool _isTrading;

        private bool _isOpen;

        protected override void PrepareUI()
        {
            InventoryUI.InitializeInventoryUI(InventoryData.Size);
            InventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            InventoryUI.OnSwapItems += HandleSwapItems;
            InventoryUI.OnStartDragging += HandleDragging;
            InventoryUI.OnItemActionRequested += HandleItemActionRequested;

            InventoryData.OnInventoryUpdated += UpdateInventoryUI;
            
            //TradingSystem.Instance.OnPlayerTrading += OnPlayerTrading;
            //TradingSystem.Instance.OnInventoryOpen += OnInventoryOpen;
            //PlayerController.Instance.OnPlayerShowOrHideInventory += ShowOrHideInventory;
        }

        private void OnTraderScene()
        {
            TradingSystem.Instance.OnPlayerTrading += OnPlayerTrading;
            TradingSystem.Instance.OnInventoryOpen += OnInventoryOpen;
        }
        public void ShowOrHideInventory()
        {
            //if (!Input.GetKeyDown(KeyCode.Tab)) return;
            if (_isTrading) return;

            if (!_isOpen)
                InventoryUI.Show();
            else
                InventoryUI.Hide();

            _isOpen = !_isOpen;
        }

        private void OnInventoryOpen(bool isOpen)
        {
            _isOpen = isOpen;
        }

        private void OnPlayerTrading(bool isTrading)
        {
            _isTrading = isTrading;
        }

        private void HandleItemActionRequested(int itemIndex)
        {
            InventoryItem inventoryItem = InventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            if (inventoryItem.item is IItemAction itemAction)
            {
                InventoryUI.ShowItemAction(itemIndex);
                InventoryUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
            }

            if (inventoryItem.item is IDestroyableItem)
            {
                InventoryUI.AddAction("Drop", () => DropItem(itemIndex, inventoryItem.quantity));
            }

            if (_isTrading)
            {
                InventoryUI.AddAction("Sell", () => SellItem(inventoryItem, itemIndex));
            }
        }

        private void SellItem(InventoryItem inventoryItem, int itemIndex)
        {
            if (!TrySellItem(inventoryItem)) return;

            if (inventoryItem.item is IDestroyableItem)
            {
                InventoryData.RemoveItem(itemIndex, 1);
            }

            if (InventoryData.GetItemAt(itemIndex).IsEmpty)
                InventoryUI.ResetSelection();
        }

        private void DropItem(int itemIndex, int quantity)
        {
            InventoryData.RemoveItem(itemIndex, quantity);
            InventoryUI.ResetSelection();
            AudioSource.PlayOneShot(DropClip);
        }

        private bool TrySellItem(InventoryItem inventoryItem)
        {
            var itemSO = inventoryItem.item;

            Debug.Log("Sell Cost = " + itemSO.ItemBuyCost);


            if (TraderMoney.Instance.CanAffordReduceMoney(itemSO.ItemSellCost)) // тут тоже, наверное, нужно количество
            {
                Debug.Log("Trader can afford it");
                if (TraderInventoryUI.Instance.TryAddItem(itemSO)) // сюда нужно будет количество передавать
                {
                    TraderMoney.Instance.TryReduceMoney(itemSO.ItemSellCost);
                    PlayerMoney.Instance.AddMoney(itemSO.ItemSellCost);
                    return true;
                }

                Debug.Log("Trader doesn't have enough space in inventory");
                return false;
            }

            Debug.Log("Trader can't afford it.");
            return false;
        }

        private void PerformAction(int itemIndex)
        {
            InventoryItem inventoryItem = InventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            if (inventoryItem.item is IDestroyableItem)
            {
                InventoryData.RemoveItem(itemIndex, 1);
            }

            if (inventoryItem.item is IItemAction itemAction)
            {
                itemAction.PerformAction(null, inventoryItem.itemState);
                InventoryUI.ResetSelection();
                if (InventoryData.GetItemAt(itemIndex).IsEmpty)
                    InventoryUI.ResetSelection();
            }
        }

        public void Dispose()
        {
            InventoryData.OnInventoryUpdated -= UpdateInventoryUI;

            InventoryUI.OnDescriptionRequested -= HandleDescriptionRequest;
            InventoryUI.OnSwapItems -= HandleSwapItems;
            InventoryUI.OnStartDragging -= HandleDragging;
            InventoryUI.OnItemActionRequested -= HandleItemActionRequested;

            TradingSystem.Instance.OnPlayerTrading -= OnPlayerTrading;
            TradingSystem.Instance.OnInventoryOpen -= OnInventoryOpen;
            //PlayerController.Instance.OnPlayerShowOrHideInventory -= ShowOrHideInventory;
        }
    }
}