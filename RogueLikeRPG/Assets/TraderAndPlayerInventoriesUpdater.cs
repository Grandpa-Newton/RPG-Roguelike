using System.Collections.Generic;
using App.Scripts.AllScenes.Interfaces;
using App.Scripts.GameScenes.Player;
using App.Scripts.MixedScenes.Inventory.Controller;
using App.Scripts.MixedScenes.Inventory.Model;
using App.Scripts.MixedScenes.Inventory.UI;
using App.Scripts.TraderScene;
using UnityEngine;

public class TraderAndPlayerInventoriesUpdater : MonoBehaviour
{
    [SerializeField] private UIInventoryPage traderInventoryUI;
    [SerializeField] private UIInventoryPage playerInventoryUI;
    [SerializeField] private InventorySO traderInventoryData;
    [SerializeField] private InventorySO playerInventoryData;
    [SerializeField] private List<InventoryItem> initialItems;
    [SerializeField] private AudioClip dropClip;
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        TraderInventoryController.Instance.Initialize(traderInventoryUI, traderInventoryData, initialItems, dropClip, audioSource);
        PlayerInventoryController.Instance.Initialize(playerInventoryUI, playerInventoryData, initialItems, dropClip, audioSource);
    }

    private void OnDestroy()
    {
        TraderInventoryController.Instance.Dispose();
        PlayerInventoryController.Instance.Dispose();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() && !traderInventoryUI.isActiveAndEnabled)
        {
            PlayerInventoryController.Instance.SetTraderObject(gameObject);
            Debug.Log("Trader found!");
            Interact();
            traderInventoryUI.Show();
            playerInventoryUI.Show();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() && traderInventoryUI.isActiveAndEnabled)
        {
            traderInventoryUI.Hide();
            playerInventoryUI.Hide();
            PlayerInventoryController.Instance.SetTraderObject(null);
            Debug.Log("Trader not found!");
        }
    }
    private void Interact()
    {
        if (traderInventoryUI.isActiveAndEnabled == false)
        {
            traderInventoryUI.Show();
            playerInventoryUI.Show();
            foreach (var item in traderInventoryData.GetCurrentInventoryState())
            {
                traderInventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
            foreach (var item in playerInventoryData.GetCurrentInventoryState())
            {
                playerInventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
        }
        else
        {
            traderInventoryUI.Hide();
            playerInventoryUI.Hide();
        }
    }
}