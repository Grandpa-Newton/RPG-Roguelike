using System.Text;
using App.Scripts.DungeonScene.Items;
using App.Scripts.GameScenes.Player.Components;
using App.Scripts.MixedScenes.Inventory.Controller;
using App.Scripts.MixedScenes.Inventory.Model;
using App.Scripts.MixedScenes.Inventory.Model.ItemParameters;
using UnityEngine;

namespace App.Scripts.TraderScene
{
    public class TraderInventoryController : AbstractInventoryController
    {
        private static TraderInventoryController _instance;
        public static TraderInventoryController Instance => _instance ??= new TraderInventoryController();

        protected override void PrepareUI()
        {
            InventoryUI.Show();
            InventoryUI.Hide();
            InventoryUI.InitializeInventoryUI(InventoryData.Size);
            InventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            InventoryUI.OnItemActionRequested += HandleItemActionRequested;
        }

        protected override void HandleItemActionRequested(int itemIndex)
        {
            InventoryItem inventoryItem = InventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                InventoryUI.ShowItemAction(itemIndex);
                InventoryUI.AddAction("Buy", () => BuyItem(inventoryItem, itemIndex));
            }

        }

        private void BuyItem(InventoryItem inventoryItem, int itemIndex)
        {
            if (TryBuyItem(inventoryItem))
            {
                IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
                if (destroyableItem != null)
                {
                    InventoryData.RemoveItem(itemIndex, 1);
                }

                //audioSource.PlayOneShot(itemAction.itemActionSound);
                //audioSource.PlayOneShot(dropClip); // тут мб другой звук
                if (InventoryData.GetItemAt(itemIndex).IsEmpty)
                    InventoryUI.ResetSelection();
            }
        }

        private bool TryBuyItem(InventoryItem inventoryItem)
        {
            var itemSO = inventoryItem.item;

            Debug.Log("Sell Cost = " + itemSO.ItemBuyCost);


            if (PlayerMoney.Instance.CanAffordReduceMoney(itemSO.ItemBuyCost)) // тут тоже, наверное, нужно количество
            {
                Debug.Log("Player can afford it");
                if (PlayerInventoryController.Instance.TryAddItem(itemSO)) // сюда нужно будет количество передавать
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

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = InventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                InventoryUI.ResetSelection();
                return;
            }

            ItemSO item = inventoryItem.item;
            string description = PrepareDescription(inventoryItem);
            InventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.itemName, description);
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
    }
}