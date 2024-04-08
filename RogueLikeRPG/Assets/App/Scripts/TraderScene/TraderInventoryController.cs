using System.Collections.Generic;
using System.Text;
using App.Scripts.AllScenes.Interfaces;
using App.Scripts.DungeonScene.Items;
using App.Scripts.GameScenes.Player.Components;
using App.Scripts.GameScenes.Player.EditableValues;
using App.Scripts.MixedScenes;
using App.Scripts.MixedScenes.Inventory.Controller;
using App.Scripts.MixedScenes.Inventory.Model;
using App.Scripts.MixedScenes.Inventory.Model.ItemParameters;
using App.Scripts.MixedScenes.Inventory.UI;
using UnityEngine;

namespace App.Scripts.TraderScene
{
    public class TraderInventoryController : IInteractable
    {
        private static TraderInventoryController _instance;
        public static TraderInventoryController Instance => _instance ??= new TraderInventoryController();

        private UIInventoryPage _inventoryUI;

        private InventorySO _inventoryData;

        private List<InventoryItem> _initialItems = new();

        private AudioClip _dropClip;
        private AudioSource _audioSource;

        private GameObject player;

        public void Initialize(UIInventoryPage inventoryUI, InventorySO inventoryData, List<InventoryItem> initialItems,
            AudioClip dropClip, AudioSource audioSource) 
        {
            _inventoryUI = inventoryUI;
            _inventoryData = inventoryData;
            _initialItems = initialItems;
            _dropClip = dropClip;
            _audioSource = audioSource;
            
            PrepareUI();
            PrepareInventoryData();
        }

        public void Dispose()
        {
            _inventoryData.OnInventoryUpdated -= UpdateInventoryUI;
        }
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
            this._inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            this._inventoryUI.OnSwapItems += HandleSwapItems;
            this._inventoryUI.OnStartDragging += HandleDragging;
            this._inventoryUI.OnItemActionRequested += HandleItemActionRequested;
        }

        private void HandleItemActionRequested(int itemIndex)
        {
            InventoryItem inventoryItem = _inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                _inventoryUI.ShowItemAction(itemIndex);
                _inventoryUI.AddAction("Buy", () => SellItem(inventoryItem, itemIndex));
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

                //audioSource.PlayOneShot(itemAction.itemActionSound);
                //audioSource.PlayOneShot(dropClip); // тут мб другой звук
                if (_inventoryData.GetItemAt(itemIndex).IsEmpty)
                    _inventoryUI.ResetSelection();
            }
        }

        private bool TrySellItem(InventoryItem inventoryItem)
        {
            var itemSO = inventoryItem.item;

            Debug.Log("Sell Cost = " + itemSO.ItemBuyCost);


            if (PlayerMoney.Instance.CanAffordReduceMoney(itemSO.ItemBuyCost)) // тут тоже, наверное, нужно количество
            {
                Debug.Log("Player can afford it");
                if (InventoryController.Instance.TryAddItem(itemSO)) // сюда нужно будет количество передавать
                {
                    PlayerMoney.Instance.TryReduceMoney(itemSO.ItemBuyCost);
                    TraderMoney.Instance.AddMoney(itemSO.ItemBuyCost);
                    return true;
                }
                else
                {
                    Debug.Log("Player doesn't have enough space in inventory");
                    return false;
                }
            }
            else
            {
                Debug.Log("Player can't afford it.");
                return false;
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

        public void Interact(GameObject player) // мб реализовать в отдельном скрипте
        {
            if (_inventoryUI.isActiveAndEnabled == false)
            {
                this.player = player;
                _inventoryUI.Show();

                foreach (var item in _inventoryData.GetCurrentInventoryState())
                {
                    _inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                }
            }
            else
            {
                this.player = null;
                _inventoryUI.Hide();
            }
        }
    }
}