using Inventory.Model;
using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TraderInventoryController : MonoBehaviour
{
    [SerializeField] private UIInventoryPage inventoryUI;

    [SerializeField] private InventorySO inventoryData;

    public List<InventoryItem> initialItems = new List<InventoryItem>();

    [SerializeField] private AudioClip dropClip;
    [SerializeField] private AudioSource audioSource;

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
        if (itemAction != null)
        {
            inventoryUI.ShowItemAction(itemIndex);
            inventoryUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
        }

        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {
            inventoryUI.AddAction("Drop", () => DropItem(itemIndex, inventoryItem.quantity));
        }
    }

    private void DropItem(int itemIndex, int quantity)
    {
        inventoryData.RemoveItem(itemIndex, quantity);
        inventoryUI.ResetSelection();
        audioSource.PlayOneShot(dropClip);
    }

    public void PerformAction(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
            return;

        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {
            inventoryData.RemoveItem(itemIndex, 1);
        }

        IItemAction itemAction = inventoryItem.item as IItemAction;
        if (itemAction != null)
        {
            itemAction.PerformAction(gameObject, inventoryItem.itemState);
            audioSource.PlayOneShot(itemAction.actionSFX);
            if (inventoryData.GetItemAt(itemIndex).IsEmpty)
                inventoryUI.ResetSelection();
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
        sb.Append(inventoryItem.item.Description);
        sb.AppendLine();
        for (int i = 0; i < inventoryItem.itemState.Count; i++)
        {
            sb.Append(
                $"{inventoryItem.itemState[i].itemParameter.ParameterName} : " +
                $"{inventoryItem.itemState[i].value} / {inventoryItem.item.DefaultParametersList[i].value}");
        }

        return sb.ToString();
    }

    public void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        var other = collision.gameObject.GetComponent<IShopCustomer>();

        Debug.Log("jjjjjjjjjjj");
        if (other != null)
        {
            Debug.Log("AAAAAAAAAAAAA");
            if (Input.GetKeyDown(KeyCode.T))
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }
}