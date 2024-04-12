using System;
using System.Collections.Generic;
using System.Text;
using App.Scripts.AllScenes.Interfaces;
using App.Scripts.MixedScenes.Inventory.Model;
using App.Scripts.MixedScenes.Inventory.Model.ItemParameters;
using App.Scripts.MixedScenes.Inventory.UI;
using UnityEngine;
using App.Scripts.DungeonScene.Items;
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
        public void ShowOrHideInventory()
        {
            if (!Input.GetKeyDown(KeyCode.Tab)) return;
            if (_isTrading) return; 

            if (!_isOpen)
            {
                _inventoryUI.Show();
            }
            else
            {
                _inventoryUI.Hide();
            }
            _isOpen = !_isOpen; 
        }


        
        protected override void PrepareUI()
        {
            _inventoryUI.Show();
            _inventoryUI.Hide();
            _inventoryUI.InitializeInventoryUI(_inventoryData.Size);
            _inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            _inventoryUI.OnSwapItems += HandleSwapItems;
            _inventoryUI.OnStartDragging += HandleDragging;
            _inventoryUI.OnItemActionRequested += HandleItemActionRequested;
            TraderAndPlayerInventoriesUpdater.Instance.OnPlayerTrading += OnPlayerTrading;
            TraderAndPlayerInventoriesUpdater.Instance.OnInventoryOpen += OnInventoryOpen;
            
            foreach (var item in _inventoryData.GetCurrentInventoryState())
            {
                _inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
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
            InventoryItem inventoryItem = _inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                _inventoryUI.ShowItemAction(itemIndex);
                _inventoryUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
            }

            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                _inventoryUI.AddAction("Drop", () => DropItem(itemIndex, inventoryItem.quantity));
            }

            if (_isTrading)
            {
                _inventoryUI.AddAction("Sell", () => SellItem(inventoryItem, itemIndex));
            }
        }
        private void SellItem(InventoryItem inventoryItem, int itemIndex)
        {
            if (TrySellItem(inventoryItem))
            {
                IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
                if (destroyableItem != null)
                {
                    _inventoryData.RemoveItem(itemIndex, 1);
                }

                if (_inventoryData.GetItemAt(itemIndex).IsEmpty)
                    _inventoryUI.ResetSelection();
            }
        }
        private void DropItem(int itemIndex, int quantity)
        {
            _inventoryData.RemoveItem(itemIndex, quantity);
            _inventoryUI.ResetSelection();
            _audioSource.PlayOneShot(_dropClip);
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
            InventoryItem inventoryItem = _inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                _inventoryData.RemoveItem(itemIndex, 1);
            }

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                itemAction.PerformAction(null, inventoryItem.itemState);
                _inventoryUI.ResetSelection();
                if (_inventoryData.GetItemAt(itemIndex).IsEmpty)
                    _inventoryUI.ResetSelection();
            }
        }
    }
}