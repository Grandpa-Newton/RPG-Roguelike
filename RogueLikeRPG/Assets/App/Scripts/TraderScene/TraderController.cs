using System;
using System.Collections;
using System.Collections.Generic;
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

    private void Update()
    {
        //TraderInventoryController.Instance.ShowOrHideInventory();
    }

    private void OnDestroy()
    {
        TraderInventoryController.Instance.Dispose();
    }
}