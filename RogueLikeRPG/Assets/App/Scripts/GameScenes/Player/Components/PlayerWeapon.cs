using System.Collections.Generic;
using App.Scripts.DungeonScene.Items;
using App.Scripts.GameScenes.Player.UI;
using App.Scripts.GameScenes.Weapon;
using App.Scripts.GameScenes.Weapon.MeleeWeapon;
using App.Scripts.GameScenes.Weapon.RangeWeapon;
using App.Scripts.MixedScenes.Inventory.Model;

namespace App.Scripts.GameScenes.Player.Components
{
    public class PlayerWeapon
    {
        private static PlayerWeapon _instance;

        public static PlayerWeapon Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PlayerWeapon();
                }

                return _instance;
            }
        }
        
        
        private CurrentWeaponsSO _currentWeaponsSO;
        private InventorySO _inventorySO;
        
        private List<ItemParameter> _parametersToModify;
        private List<ItemParameter> _itemCurrentState;

        public void Initialize(InventorySO inventorySO, 
            List<ItemParameter> parametersToModify, List<ItemParameter> itemCurrentState)
        {
            _inventorySO = inventorySO;
            _parametersToModify = parametersToModify;
            _itemCurrentState = itemCurrentState;
        }
        public void SetMeleeWeapon(WeaponItemSO weaponItemSO, List<ItemParameter> itemState)
        {
            if (PlayerCurrentWeapon.Instance.CurrentMeleeAndRangeWeaponsSO.EquippedMeleeWeapon != null)
            {
                _inventorySO.AddItem(PlayerCurrentWeapon.Instance.CurrentMeleeAndRangeWeaponsSO.EquippedMeleeWeapon , 1, _itemCurrentState);
            }
            
            PlayerCurrentWeapon.Instance.CurrentMeleeAndRangeWeaponsSO.EquippedMeleeWeapon = (MeleeWeaponSO)weaponItemSO;
            MeleeWeaponTrigger.Instance.SetMeleeWeaponSO(PlayerCurrentWeapon.Instance.CurrentMeleeAndRangeWeaponsSO.EquippedMeleeWeapon);
            PlayerCurrentWeaponUI.Instance.IncreaseMeleeWeaponScale();
            PlayerCurrentWeaponUI.Instance.SetMeleeWeaponIcon(weaponItemSO.ItemImage);
            _itemCurrentState = new List<ItemParameter>(itemState); 
            ModifyParameters();
        }
        public void SetRangeWeapon(WeaponItemSO weaponItemSO, List<ItemParameter> itemState)
        {
            if (PlayerCurrentWeapon.Instance.CurrentMeleeAndRangeWeaponsSO.EquippedRangeWeapon != null)
            {
                _inventorySO.AddItem(PlayerCurrentWeapon.Instance.CurrentMeleeAndRangeWeaponsSO.EquippedRangeWeapon, 1, _itemCurrentState);
            }
            
            PlayerCurrentWeapon.Instance.CurrentMeleeAndRangeWeaponsSO.EquippedRangeWeapon = (RangeWeaponSO)weaponItemSO;
            PlayerCurrentWeaponUI.Instance.IncreaseRangeWeaponScale();
            PlayerCurrentWeaponUI.Instance.SetRangeWeaponIcon(weaponItemSO.ItemImage);
            _itemCurrentState = new List<ItemParameter>(itemState); 
            ModifyParameters();
        }
        private void ModifyParameters()
        {
            foreach (var parameter in _parametersToModify)
            {
                if (_itemCurrentState.Contains(parameter))
                {
                    int index = _itemCurrentState.IndexOf(parameter);
                    float newValue = _itemCurrentState[index].value + parameter.value;
                    _itemCurrentState[index] = new ItemParameter
                    {
                        itemParameter = parameter.itemParameter,
                        value = newValue,
                    };
                }
            }
        }
    }
}
