using System;
using System.Collections.Generic;
using App.Scripts.DungeonScene.Items;
using App.Scripts.MixedScenes.Inventory.Model;
using App.Scripts.MixedScenes.Weapon;
using UnityEngine;

namespace App.Scripts.MixedScenes.Player
{
    public class PlayerWeapon
    {
        private CurrentWeaponsSO _currentWeaponsSO;
        private InventorySO _inventorySO;
        
        private List<ItemParameter> _parametersToModify;
        private List<ItemParameter> _itemCurrentState;

        public PlayerWeapon(CurrentWeaponsSO currentWeaponsSO, InventorySO inventorySO, 
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
