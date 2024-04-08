using System;
using System.Collections.Generic;
using System.Text;
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
    public class InventoryController
    {
        private static InventoryController _instance;
        public static InventoryController Instance => _instance ??= new InventoryController();
        
        [Title("Inventory Components")]
        private UIInventoryPage _inventoryUI;
        private InventorySO _inventoryData;

        private List<InventoryItem> _initialItems = new();
        
        [Title("Audio Sources")] 
        private AudioClip _dropClip;
        private AudioSource _audioSource;

        [Title("Trading System")]
        private GameObject trader;

        public void Initialize(UIInventoryPage inventoryPage,InventorySO inventoryData, List<InventoryItem> initialItems,
            AudioClip dropClip, AudioSource audioSource)
        {
            _inventoryUI = inventoryPage;
            _inventoryData = inventoryData;
            _initialItems = initialItems;
            _dropClip = dropClip;
            _audioSource = audioSource;
            
            PrepareUI();
            PrepareInventoryData();
        }
        
        public void Dispose()
        {
            UnSubscribeEvent();
        }
        
        /*private void Start()
     {
         PrepareUI();
         PrepareInventoryData();
     }*/
        /*private void OnDestroy()
        {
            UnSubscribeEvent();
        }*/

        private void PrepareInventoryData()
        {
            if (_inventoryData.inventoryItems == null || _inventoryData.inventoryItems.Count == 0)
            {
                _inventoryData.Initialize();
                foreach (InventoryItem item in _initialItems)
                {
                    if (item.IsEmpty)
                        continue;
                    _inventoryData.AddItem(item);
                }
            }

            _inventoryData.OnInventoryUpdated += UpdateInventoryUI;
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            _inventoryUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                _inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
        }

        private void PrepareUI()
        {
            _inventoryUI.Show();
            _inventoryUI.Hide();
            _inventoryUI.InitializeInventoryUI(_inventoryData.Size);
            _inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            _inventoryUI.OnSwapItems += HandleSwapItems;
            _inventoryUI.OnStartDragging += HandleDragging;
            _inventoryUI.OnItemActionRequested += HandleItemActionRequested;
        }

        private void HandleItemActionRequested(int itemIndex)
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

            if (trader != null)
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

        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = _inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            _inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
        }
        private void HandleSwapItems(int itemIndex1, int itemIndex2)
        {
            _inventoryData.SwapItems(itemIndex1, itemIndex2);
        }
        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = _inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                _inventoryUI.ResetSelection();
                return;
            }

            ItemSO item = inventoryItem.item;
            string description = PrepareDescription(inventoryItem);
            _inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.itemName, description);
        }
        private string PrepareDescription(InventoryItem inventoryItem)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(inventoryItem.item.description);
            sb.AppendLine();
            for (int i = 0; i < inventoryItem.itemState.Count; i++)
            {
                sb.Append(
                    $"{inventoryItem.itemState[i].itemParameter.ParameterName} : " +
                    $"{inventoryItem.itemState[i].value} / {inventoryItem.item.DefaultParametersList[i].value}");
            }

            return sb.ToString();
        }

        public void UnSubscribeEvent()
        {
            _inventoryData.OnInventoryUpdated -= UpdateInventoryUI;
        }

        public void SetTraderObject(GameObject trader)
        {
            this.trader = trader;
        }

        public void Update()
        {
            ShowOrHideInventory();
        }

        public void ShowOrHideInventory()
        {
            if (!Input.GetKeyDown(KeyCode.Tab)) return;
            
            if (_inventoryUI.isActiveAndEnabled == false)
            {
                _inventoryUI.Show();

                foreach (var item in _inventoryData.GetCurrentInventoryState())
                {
                    _inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                }
            }
            else
            {
                _inventoryUI.Hide();
            }
        }

        public void CloseInventory()
        {
            _inventoryUI.Hide();
        }

        public bool TryAddItem(ItemSO item)
        {
            int reminder =
                _inventoryData.AddItem(item,
                    1); // потом тут нужно будет сделать так, чтобы пользователь мог выбирать количество предметов для покупки
            if (reminder == 0)
                return true;
            else
                return false;
        }
    }
}