using System.Collections.Generic;
using App.Scripts.GameScenes.Inventory.Controller;
using App.Scripts.GameScenes.Inventory.Model;
using App.Scripts.GameScenes.Player.EditableValues;
using App.Scripts.MixedScenes.Inventory.UI;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace App.Scripts.TraderScene
{
    public class TraderUIInitializer : MonoBehaviour
    {
        private static TraderUIInitializer _instance;
        public static TraderUIInitializer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<TraderUIInitializer>();
                }
                return _instance;
            }
        }
    
        [Title("Inventory")]
        [SerializeField] private UIInventoryPage traderInventoryUI;
        [SerializeField] private InventorySO traderInventoryData;
        [SerializeField] private List<InventoryItem> initialItems;
        [SerializeField] private AudioClip dropClip;
        [SerializeField] private AudioSource audioSource;
    
        [Title("Inventory Money UI")]
        [SerializeField] private TMP_Text currentMoneyTextField;
        [SerializeField] private ChangeableValueSO traderMoneySO;
    
        void Awake()
        {
            InitializeUIComponents();
        }
    
        public void InitializeUIComponents()
        {
            TraderInventoryUI.Instance.Initialize(traderInventoryUI, traderInventoryData, initialItems, dropClip, audioSource);
            MoneyUIFactory.Create(currentMoneyTextField, traderMoneySO);
        }
    }
}
