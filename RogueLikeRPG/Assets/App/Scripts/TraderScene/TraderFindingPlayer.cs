using System;
using System.Collections;
using System.Collections.Generic;
using App.Scripts.AllScenes.Interfaces;
using App.Scripts.GameScenes.Player;
using App.Scripts.MixedScenes.Inventory.Controller;
using App.Scripts.MixedScenes.Inventory.Model;
using App.Scripts.MixedScenes.Inventory.UI;
using App.Scripts.TraderScene;
using UnityEngine;

public class TraderFindingPlayer : MonoBehaviour, IInteractable
{
    [SerializeField] private UIInventoryPage _inventoryUI;
    [SerializeField] private InventorySO _inventoryData;
    private PlayerController _playerController;

    public void Initialize(UIInventoryPage inventoryUI, InventorySO inventoryData, PlayerController playerController)
    {
        _inventoryUI = inventoryUI;
        _inventoryData = inventoryData;
        _playerController = playerController;
    }

    private IInteractable _traderInteractable;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() && !_inventoryUI.isActiveAndEnabled)
        {
            PlayerInventoryController.Instance.SetTraderObject(gameObject);
            Debug.Log("Trader found!");
            Interact(gameObject);
            _inventoryUI.Show();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() && _inventoryUI.isActiveAndEnabled)
        {
            _inventoryUI.Hide();
            PlayerInventoryController.Instance.SetTraderObject(null);
            Debug.Log("Trader not found!");
        }
    }

    public void Interact(GameObject player) // мб реализовать в отдельном скрипте
    {
        if (_inventoryUI.isActiveAndEnabled == false)
        {
            // this.player = player;
            _inventoryUI.Show();

            foreach (var item in _inventoryData.GetCurrentInventoryState())
            {
                _inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
        }
        else
        {
            //this.player = null;
            _inventoryUI.Hide();
        }
    }

    private IInteractable GetInteractableObject()
    {
        float interactRange = 2f;

        Collider2D[] colliders =
            Physics2D.OverlapBoxAll(transform.position, new Vector2(interactRange, interactRange), 0f);

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out IInteractable interactableObject))
            {
                //_traderGameObject = collider.gameObject;
                return interactableObject;
            }
        }

        return null;
    }
}