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
    
    protected void HandleDescriptionRequest(int itemIndex)
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
    public void Dispose()
    {
        _inventoryData.OnInventoryUpdated -= UpdateInventoryUI;
    }
    
    private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
    {
        _inventoryUI.ResetAllItems();
        foreach (var item in inventoryState)
        {
            _inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
        }
    }
}
