using System.Collections.Generic;
using System.Text;
using App.Scripts.DungeonScene.Items;
using App.Scripts.MixedScenes.Inventory.Model;
using App.Scripts.MixedScenes.Inventory.UI;
using UnityEngine;

public class AbstractInventoryController
{
    protected UIInventoryPage _inventoryUI;
    protected InventorySO _inventoryData;

    protected List<InventoryItem> _initialItems = new();
    
    protected AudioClip _dropClip;
    protected AudioSource _audioSource;
    
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
        UpdateInventoryItems();
    }

    private void UpdateInventoryItems()
    {
        foreach (var item in _inventoryData.GetCurrentInventoryState())
        {
            _inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
        }   
    }
    protected virtual void PrepareUI()
    {
        _inventoryUI.Show();
        _inventoryUI.Hide();
        _inventoryUI.InitializeInventoryUI(_inventoryData.Size);
        _inventoryUI.OnDescriptionRequested += HandleDescriptionRequest; 
        _inventoryUI.OnSwapItems += HandleSwapItems; //!!!
        _inventoryUI.OnStartDragging += HandleDragging; //!!!
        _inventoryUI.OnItemActionRequested += HandleItemActionRequested;
    }
    private void PrepareInventoryData()
    {
        _inventoryData.OnInventoryUpdated += UpdateInventoryUI;
    }
    
    protected void HandleDescriptionRequest(int itemIndex)
    {
        InventoryItem inventoryItem = _inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
        {
            _inventoryUI.ResetSelection();
            return;
        }

        ItemSO item = inventoryItem.item;
        _inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.itemName, item.description);
    }
    protected void HandleSwapItems(int itemIndex1, int itemIndex2)
    {
        _inventoryData.SwapItems(itemIndex1, itemIndex2);
    }
    protected void HandleDragging(int itemIndex)
    {
        InventoryItem inventoryItem = _inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
            return;
        _inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
    }
    protected virtual void HandleItemActionRequested(int itemIndex)
    {
        Debug.LogError("Abstract HandleItemActionRequested run");
    }
    public bool TryAddItem(ItemSO item)
    {
        int reminder = _inventoryData.AddItem(item, 1); 
        return reminder == 0;
    }
    public void Dispose()
    {
        _inventoryData.OnInventoryUpdated -= UpdateInventoryUI;
    }
    
    protected void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
    {
        _inventoryUI.ResetAllItems();
        UpdateInventoryItems();
    }
}
