using System.Collections.Generic;
using App.Scripts.GameScenes.Player;
using App.Scripts.MixedScenes.Inventory.Controller;
using App.Scripts.MixedScenes.Inventory.Model;
using App.Scripts.MixedScenes.Inventory.UI;
using App.Scripts.TraderScene;
using UnityEngine;

public class TraderController : MonoBehaviour
{
    [SerializeField] private UIInventoryPage inventoryUI;
    [SerializeField] private InventorySO inventoryData;
    [SerializeField] private List<InventoryItem> initialItems;
    [SerializeField] private AudioClip dropClip;
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        TraderInventoryController.Instance.Initialize(inventoryUI, inventoryData, initialItems, dropClip, audioSource);
    }

    private void OnDestroy()
    {
        TraderInventoryController.Instance.Dispose();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() && !inventoryUI.isActiveAndEnabled)
        {
            InventoryController.Instance.SetTraderObject(gameObject);
            Debug.Log("Trader found!");
            Interact();
            inventoryUI.Show();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() && inventoryUI.isActiveAndEnabled)
        {
            inventoryUI.Hide();
            InventoryController.Instance.SetTraderObject(null);
            Debug.Log("Trader not found!");
        }
    }
    private void Interact()
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