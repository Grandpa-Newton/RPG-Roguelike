using System.Collections.Generic;
using App.Scripts.GameScenes.Inventory.Controller;
using App.Scripts.GameScenes.Inventory.Model;
using App.Scripts.GameScenes.Player.EditableValues;
using App.Scripts.GameScenes.Player.UI;
using App.Scripts.MixedScenes.Inventory.Model;
using App.Scripts.MixedScenes.Inventory.UI;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.GameScenes.Player
{
    public class PlayerUIInitializer : MonoBehaviour
    {
        private static PlayerUIInitializer _instance;
        public static PlayerUIInitializer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<PlayerUIInitializer>();
                }
                return _instance;
            }
        }

        [Title("Inventory")] 
        [SerializeField] private UIInventoryPage uiInventoryPage;
        [SerializeField] private InventorySO inventorySO;
        [SerializeField] private List<InventoryItem> inventoryItems;
        [SerializeField] private AudioClip inventoryOpenClip;
        [SerializeField] private AudioSource inventoryAudioSource;

        [Title("Current Weapon UI")] 
        [SerializeField] private CanvasGroup meleeWeaponUI;
        [SerializeField] private CanvasGroup rangeWeaponUI;

        [SerializeField] private Image meleeWeaponIcon;
        [SerializeField] private Image rangeWeaponIcon;
        
        [Title("Inventory Money UI")]
        [SerializeField] private TMP_Text currentMoneyTextField;
        [SerializeField] private ChangeableValueSO playerMoneySO; 
        
        public void InitializeUIComponents()
        {
            PlayerInventoryUI.Instance.Initialize(uiInventoryPage, inventorySO, inventoryItems, inventoryOpenClip, inventoryAudioSource);
            PlayerCurrentWeaponUI.Instance.Initialize(meleeWeaponUI, rangeWeaponUI, meleeWeaponIcon, rangeWeaponIcon);
            MoneyUIFactory.Create(currentMoneyTextField, playerMoneySO);
        }
    }
}