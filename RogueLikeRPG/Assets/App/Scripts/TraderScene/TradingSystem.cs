using System;
using App.Scripts.AllScenes;
using App.Scripts.GameScenes.Inventory.Controller;
using App.Scripts.GameScenes.Player;
using App.Scripts.MixedScenes.Inventory.UI;
using UnityEngine;

namespace App.Scripts.TraderScene
{
    public class TradingSystem : MonoBehaviour
    {
        public static TradingSystem Instance { get; private set; }

        [SerializeField] private Canvas interactHelper;
    
        private bool _isStartTrading;
        private bool _isInteracting;

        [SerializeField] private UIInventoryPage uiPlayerInventoryPage;
        [SerializeField] private UIInventoryPage uiTraderInventoryPage;
        
        
        public event Action<bool> OnPlayerTrading;
        public event Action<bool> OnInventoryOpen;

        private void Awake()
        {
            Instance = this;
            SpawnObjectsManager.Instance.OnPlayerComponentsSpawn += FindUIInventoriesPages;
        }

        private void FindUIInventoriesPages()
        {
            uiPlayerInventoryPage = GameObject.Find("PlayerUI(Clone)/PlayerInventory").GetComponent<UIInventoryPage>();
            uiTraderInventoryPage = GameObject.Find("TraderUI/TraderInventory").GetComponent<UIInventoryPage>();
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

            uiTraderInventoryPage.Show();
            uiPlayerInventoryPage.Show();

            OnInventoryOpen?.Invoke(true);
            OnPlayerTrading?.Invoke(true);
        }

        private void EndInteractingAndTrading()
        {
            _isStartTrading = false;
            _isInteracting = false;
        
            uiTraderInventoryPage.Hide();
            uiPlayerInventoryPage.Hide();

            OnPlayerTrading?.Invoke(false);
            OnInventoryOpen?.Invoke(false);
        }

        private void OnDestroy()
        {
            TraderInventoryUI.Instance.Dispose();
        }
    }
}