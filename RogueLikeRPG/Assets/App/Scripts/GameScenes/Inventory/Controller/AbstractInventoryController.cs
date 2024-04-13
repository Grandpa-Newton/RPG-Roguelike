using System.Collections.Generic;
using System.Text;
using App.Scripts.DungeonScene.Items;
using App.Scripts.MixedScenes.Inventory.Model;
using App.Scripts.MixedScenes.Inventory.UI;
using UnityEngine;

public class AbstractInventoryController
{
    protected UIInventoryPage InventoryUI;
    protected InventorySO InventoryData;

    protected List<InventoryItem> InitialItems = new();
    
    protected AudioClip DropClip;
    protected AudioSource AudioSource;
    
    public void Initialize(UIInventoryPage inventoryPage,InventorySO inventoryData, List<InventoryItem> initialItems,
        AudioClip dropClip, AudioSource audioSource)
    {
        InventoryUI = inventoryPage;
        InventoryData = inventoryData;
        InitialItems = initialItems;
        DropClip = dropClip;
        AudioSource = audioSource;
        
        PrepareUI();
        PrepareInventoryData();
        UpdateInventoryItems();
    }

    private void UpdateInventoryItems()
    {
        foreach (var item in InventoryData.GetCurrentInventoryState())
        {
            InventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
        }   
    }
    protected virtual void PrepareUI()
    {
        InventoryUI.Show();
        InventoryUI.Hide();
        InventoryUI.InitializeInventoryUI(InventoryData.Size);
        InventoryUI.OnDescriptionRequested += HandleDescriptionRequest; 
        InventoryUI.OnSwapItems += HandleSwapItems; //!!!
        InventoryUI.OnStartDragging += HandleDragging; //!!!
        InventoryUI.OnItemActionRequested += HandleItemActionRequested;
    }
    private void PrepareInventoryData()
    {
        InventoryData.OnInventoryUpdated += UpdateInventoryUI;
    }
    
    protected void HandleDescriptionRequest(int itemIndex)
    {
        InventoryItem inventoryItem = InventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
        {
            InventoryUI.ResetSelection();
            return;
        }

        ItemSO item = inventoryItem.item;
        InventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.itemName, item.description);
    }
    protected void HandleSwapItems(int itemIndex1, int itemIndex2)
    {
        InventoryData.SwapItems(itemIndex1, itemIndex2);
    }
    protected void HandleDragging(int itemIndex)
    {
        InventoryItem inventoryItem = InventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
            return;
        InventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
    }
    protected virtual void HandleItemActionRequested(int itemIndex)
    {
        Debug.LogError("Abstract HandleItemActionRequested run");
    }
    public bool TryAddItem(ItemSO item)
    {
        int reminder = InventoryData.AddItem(item, 1); 
        return reminder == 0;
    }
    public void Dispose()
    {
        InventoryData.OnInventoryUpdated -= UpdateInventoryUI;
    }
    
    protected void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
    {
        InventoryUI.ResetAllItems();
        UpdateInventoryItems();
    }
}
