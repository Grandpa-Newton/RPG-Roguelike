using System;
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
    public static TraderAndPlayerInventoriesUpdater Instance { get; private set; }

    [SerializeField] private UIInventoryPage traderInventoryUI;
    [SerializeField] private UIInventoryPage playerInventoryUI;
    [SerializeField] private InventorySO traderInventoryData;
    [SerializeField] private InventorySO playerInventoryData;
    [SerializeField] private List<InventoryItem> initialItems;
    [SerializeField] private AudioClip dropClip;
    [SerializeField] private AudioSource audioSource;

    public event Action<bool> OnPlayerTrading;
    public event Action<bool> OnInventoryOpen;

    private void Awake()
    {
        Instance = this;
        TraderInventoryController.Instance.Initialize(traderInventoryUI, traderInventoryData, initialItems, dropClip,
            audioSource);
        PlayerInventoryController.Instance.Initialize(playerInventoryUI, playerInventoryData, initialItems, dropClip,
            audioSource);
    }

    private bool _isStartTrading;
    private bool _isInteracting;

    private void Update()
    {
        TradingProcess();
    }

    private void TradingProcess()
    {
        if (!_isStartTrading || !Input.GetKeyDown(KeyCode.E)) return;
        
        if (_isInteracting)
        {
            EndInteract();
        }
        else
        {
            StartInteract();
        }
        _isInteracting = !_isInteracting;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>())
        {
            _isStartTrading = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>())
        {
            _isStartTrading = false;
            EndInteract();
        }
    }

    private void StartInteract()
    {
        Debug.Log("Trader found!");
        
        OnPlayerTrading?.Invoke(true);
        PlayerInventoryController.Instance.SetTraderObject(gameObject);
        
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

    private void EndInteract()
    {
        Debug.Log("Trader not found!");
        
        OnPlayerTrading?.Invoke(false);
        traderInventoryUI.Hide();
        playerInventoryUI.Hide();
        PlayerInventoryController.Instance.SetTraderObject(null);
        OnInventoryOpen?.Invoke(false);
    }
    
    private void OnDestroy()
    {
        TraderInventoryController.Instance.Dispose();
        PlayerInventoryController.Instance.Dispose();
    }
}