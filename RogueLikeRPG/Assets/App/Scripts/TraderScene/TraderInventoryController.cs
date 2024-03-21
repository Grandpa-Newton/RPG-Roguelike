using System.Collections.Generic;
using System.Text;
using App.Scripts.AllScenes.Interfaces;
using App.Scripts.DungeonScene.Items;
using App.Scripts.MixedScenes.Inventory.Controller;
using App.Scripts.MixedScenes.Inventory.Model;
using App.Scripts.MixedScenes.Inventory.Model.ItemParameters;
using App.Scripts.MixedScenes.Inventory.UI;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace App.Scripts.TraderScene
{
    public class TraderInventoryController : MonoBehaviour, IInteractable
    {
        [SerializeField] private UIInventoryPage inventoryUI;

        [SerializeField] private InventorySO inventoryData;

        public List<InventoryItem> initialItems = new List<InventoryItem>();

        [SerializeField] private AudioClip dropClip;
        [SerializeField] private AudioSource audioSource;

        [SerializeField] private GameObject player;

        [SerializeField] private Money _money;

        private void Start()
        {
            PrepareUI();
            PrepareInventoryData();
        }

        private void PrepareInventoryData()
        {
            if (inventoryData.inventoryItems == null || inventoryData.inventoryItems.Count == 0)
            {
                inventoryData.Initialize();
                foreach (InventoryItem item in initialItems)
                {
                    if (item.IsEmpty)
                        continue;
                    inventoryData.AddItem(item);
                }
            }
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
        }

        private void PrepareUI()
        {
            inventoryUI.Show();
            inventoryUI.Hide();
            inventoryUI.InitializeInventoryUI(inventoryData.Size);
            this.inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            this.inventoryUI.OnSwapItems += HandleSwapItems;
            this.inventoryUI.OnStartDragging += HandleDragging;
            this.inventoryUI.OnItemActionRequested += HandleItemActionRequested;
        }

        private void HandleItemActionRequested(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            IItemAction itemAction = inventoryItem.item as IItemAction;
            /*if (itemAction != null)
            {
                inventoryUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
            }*/

            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                inventoryUI.ShowItemAction(itemIndex);
                inventoryUI.AddAction("Buy", () => SellItem(inventoryItem, itemIndex));
            }

            /**/
        }

        private void SellItem(InventoryItem inventoryItem, int itemIndex)
        {
            if (TrySellItem(inventoryItem))
            {
                IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
                if (destroyableItem != null)
                {
                    inventoryData.RemoveItem(itemIndex, 1);
                }
                //audioSource.PlayOneShot(itemAction.itemActionSound);
                audioSource.PlayOneShot(dropClip); // тут мб другой звук
                if (inventoryData.GetItemAt(itemIndex).IsEmpty)
                    inventoryUI.ResetSelection();
            }
        }

        private bool TrySellItem(InventoryItem inventoryItem)
        {
            var itemSO = inventoryItem.item;

            Debug.Log("Sell Cost = " + itemSO.ItemBuyCost);


            var playerMoney = player.GetComponent<Money>();

            if (playerMoney.CanAffordReduceMoney(itemSO.ItemBuyCost)) // тут тоже, наверное, нужно количество
            {
                Debug.Log("Player can afford it");
                if (player.GetComponent<InventoryController>().TryAddItem(itemSO)) // сюда нужно будет количество передавать
                {
                    playerMoney.TryReduceMoney(itemSO.ItemBuyCost);
                    _money.AddMoney(itemSO.ItemBuyCost);
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

        public void PerformAction(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
            
                if (itemAction.PerformAction(player, inventoryItem.itemState))
                {
                    IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
                    if (destroyableItem != null)
                    {
                        inventoryData.RemoveItem(itemIndex, 1);
                    }
                    audioSource.PlayOneShot(itemAction.itemActionSound);
                    if (inventoryData.GetItemAt(itemIndex).IsEmpty)
                        inventoryUI.ResetSelection();
                }
            }
        }

        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
        }

        private void HandleSwapItems(int itemIndex1, int itemIndex2)
        {
            inventoryData.SwapItems(itemIndex1, itemIndex2);
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                inventoryUI.ResetSelection();
                return;
            }

            ItemSO item = inventoryItem.item;
            string description = PrepareDescription(inventoryItem);
            inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.name, description);
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
            int reminder = inventoryData.AddItem(item, 1);// потом тут нужно будет сделать так, чтобы пользователь мог выбирать количество предметов для покупки
            if (reminder == 0)
                return true;
            else
                return false;
        }

        public void Interact() // мб реализовать в отдельном скрипте
        {
            if (inventoryUI.isActiveAndEnabled == false)
            {
                inventoryUI.Show();

                foreach (var item in inventoryData.GetCurrentInventoryState())
                {
                    inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                }
            }
            else
            {
                inventoryUI.Hide();
            }
        }
    }
}
