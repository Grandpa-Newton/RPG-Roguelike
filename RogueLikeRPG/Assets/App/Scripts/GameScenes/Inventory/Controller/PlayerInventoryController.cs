using System;
using System.Collections.Generic;
using System.Text;
using App.Scripts.AllScenes.Interfaces;
using App.Scripts.MixedScenes.Inventory.Model;
using App.Scripts.MixedScenes.Inventory.Model.ItemParameters;
using App.Scripts.MixedScenes.Inventory.UI;
using UnityEngine;
using App.Scripts.DungeonScene.Items;
using App.Scripts.GameScenes.Player;
using App.Scripts.GameScenes.Player.Components;
using App.Scripts.GameScenes.Player.EditableValues;
using App.Scripts.TraderScene;
using Sirenix.OdinInspector;

namespace App.Scripts.MixedScenes.Inventory.Controller
{
    public class PlayerInventoryController : AbstractInventoryController
    {
        private static PlayerInventoryController _instance;
        public static PlayerInventoryController Instance => _instance ??= new PlayerInventoryController();
        
        private bool _isTrading;
        
        private bool _isOpen;
        private void ShowOrHideInventory()
        {
            //if (!Input.GetKeyDown(KeyCode.Tab)) return;
            if (_isTrading) return; 

            if (!_isOpen)
            {
                Debug.Log("Shoooooooooooooooooooooooooow!");
                InventoryUI.Show();
            }
            else
            {
                InventoryUI.Hide();
            }
            _isOpen = !_isOpen; 
        }


        
        protected override void PrepareUI()
        {
            InventoryUI.Show();
            InventoryUI.Hide();
            InventoryUI.InitializeInventoryUI(InventoryData.Size);
            InventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            InventoryUI.OnSwapItems += HandleSwapItems;
            InventoryUI.OnStartDragging += HandleDragging;
            InventoryUI.OnItemActionRequested += HandleItemActionRequested;
            TraderAndPlayerInventoriesUpdater.Instance.OnPlayerTrading += OnPlayerTrading;
            TraderAndPlayerInventoriesUpdater.Instance.OnInventoryOpen += OnInventoryOpen;
            PlayerController.Instance.OnPlayerShowOrHideInventory += ShowOrHideInventory;
            Debug.Log("We in PlayerInventoryController not in Abstract");
            foreach (var item in InventoryData.GetCurrentInventoryState())
            {
                InventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
        }

        private void OnInventoryOpen(bool isOpen)
        {
            _isOpen = isOpen;
        }

        private void OnPlayerTrading(bool isTrading)
        {
            _isTrading = isTrading;
        }

        protected override void HandleItemActionRequested(int itemIndex)
        {
            InventoryItem inventoryItem = InventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                InventoryUI.ShowItemAction(itemIndex);
                InventoryUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
            }

            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
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
            if (TrySellItem(inventoryItem))
            {
                IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
                if (destroyableItem != null)
                {
                    InventoryData.RemoveItem(itemIndex, 1);
                }

                if (InventoryData.GetItemAt(itemIndex).IsEmpty)
                    InventoryUI.ResetSelection();
            }
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
                if (TraderInventoryController.Instance.TryAddItem(itemSO)) // сюда нужно будет количество передавать
                {
                    TraderMoney.Instance.TryReduceMoney(itemSO.ItemSellCost);
                    PlayerMoney.Instance.AddMoney(itemSO.ItemSellCost);
                    return true;
                }
                else
                {
                    Debug.Log("Trader doesn't have enough space in inventory");
                    return false;
                }
            }
            else
            {
                Debug.Log("Trader can't afford it.");
                return false;
            }
        }
        private void PerformAction(int itemIndex)
        {
            InventoryItem inventoryItem = InventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                InventoryData.RemoveItem(itemIndex, 1);
            }

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                itemAction.PerformAction(null, inventoryItem.itemState);
                InventoryUI.ResetSelection();
                if (InventoryData.GetItemAt(itemIndex).IsEmpty)
                    InventoryUI.ResetSelection();
            }
        }

        public new void Dispose()
        {
            InventoryData.OnInventoryUpdated -= UpdateInventoryUI;
            PlayerController.Instance.OnPlayerShowOrHideInventory -= ShowOrHideInventory;
        }
    }
}