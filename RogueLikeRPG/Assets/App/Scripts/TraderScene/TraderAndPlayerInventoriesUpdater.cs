using System;
using System.Collections.Generic;
using App.Scripts.GameScenes.Inventory.Controller;
using App.Scripts.GameScenes.Player;
using App.Scripts.GameScenes.Player.EditableValues;
using App.Scripts.GameScenes.Player.UI;
using App.Scripts.MixedScenes.Inventory.Controller;
using App.Scripts.MixedScenes.Inventory.Model;
using App.Scripts.MixedScenes.Inventory.UI;
using App.Scripts.TraderScene;
using TMPro;
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

    [SerializeField] private Canvas interactHelper;

    [SerializeField] private TMP_Text currentMoneyTextField;
    [SerializeField] private ChangeableValueSO traderMoneySO;
    
    private bool _isStartTrading;
    private bool _isInteracting;
    
    public event Action<bool> OnPlayerTrading;
    public event Action<bool> OnInventoryOpen;

    private void Awake()
    {
        Instance = this;
        TraderInventoryUI.Instance.Initialize(traderInventoryUI, traderInventoryData, initialItems, dropClip,
            audioSource);
        MoneyUIFactory.Create(currentMoneyTextField, traderMoneySO);
    }
    
    private void Update()
    {
        TradingProcess();
    }

    private void TradingProcess()
    {
        if (_isStartTrading) return;


        if (_isInteracting && Input.GetKeyDown(KeyCode.E))
        {
            StartTrading();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>())
        {
            StartInteracting();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>())
        {
            EndInteractingAndTrading();
        }
    }

    private void StartInteracting()
    {
        interactHelper.gameObject.SetActive(true);
        _isInteracting = true;
    }

    private void StartTrading()
    {
        _isStartTrading = true;
        interactHelper.gameObject.SetActive(false);

        traderInventoryUI.Show();
        playerInventoryUI.Show();

        OnInventoryOpen?.Invoke(true);
        OnPlayerTrading?.Invoke(true);
    }

    private void EndInteractingAndTrading()
    {
        _isStartTrading = false;
        _isInteracting = false;
        
        traderInventoryUI.Hide();
        playerInventoryUI.Hide();

        OnPlayerTrading?.Invoke(false);
        OnInventoryOpen?.Invoke(false);
    }

    private void OnDestroy()
    {
        TraderInventoryUI.Instance.Dispose();
    }
}