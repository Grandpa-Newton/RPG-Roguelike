using System.Collections.Generic;
using App.Scripts.DungeonScene.Items;
using App.Scripts.MixedScenes.Inventory.Model;
using App.Scripts.MixedScenes.Weapon;
using App.Scripts.MixedScenes.Weapon.MeleeWeapon;


namespace App.Scripts.MixedScenes.Player
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

        public void Initialize(CurrentWeaponsSO currentWeaponsSO, InventorySO inventorySO, 
            List<ItemParameter> parametersToModify, List<ItemParameter> itemCurrentState)
        {
            _currentWeaponsSO = currentWeaponsSO;
            _inventorySO = inventorySO;
            _parametersToModify = parametersToModify;
            _itemCurrentState = itemCurrentState;
        }
        public void SetMeleeWeapon(WeaponItemSO weaponItemSO, List<ItemParameter> itemState)
        {
            if (_currentWeaponsSO.EquipMeleeWeapon != null)
            {
                _inventorySO.AddItem(_currentWeaponsSO.EquipMeleeWeapon, 1, _itemCurrentState);
            }
            
            _currentWeaponsSO.EquipMeleeWeapon = weaponItemSO;
            MeleeWeaponTrigger.Instance.SetMeleeWeaponSO((MeleeWeaponSO)_currentWeaponsSO.EquipMeleeWeapon);
            PlayerCurrentWeaponUI.Instance.IncreaseMeleeWeaponScale();
            PlayerCurrentWeaponUI.Instance.SetMeleeWeaponIcon(weaponItemSO.ItemImage);
            _itemCurrentState = new List<ItemParameter>(itemState); 
            ModifyParameters();
        }
        public void SetRangeWeapon(WeaponItemSO weaponItemSO, List<ItemParameter> itemState)
        {
            if (_currentWeaponsSO.EquipRangeWeapon != null)
            {
                _inventorySO.AddItem(_currentWeaponsSO.EquipRangeWeapon, 1, _itemCurrentState);
            }
            
            _currentWeaponsSO.EquipRangeWeapon = weaponItemSO;
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
