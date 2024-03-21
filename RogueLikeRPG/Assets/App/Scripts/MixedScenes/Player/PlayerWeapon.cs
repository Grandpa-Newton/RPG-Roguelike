using System.Collections.Generic;
using App.Scripts.MixedScenes.Inventory.Model;
using App.Scripts.MixedScenes.Weapon;
using Inventory.Model;
using UnityEngine;

namespace App.Scripts.MixedScenes.Player
{
    public class PlayerWeapon : MonoBehaviour
    {
        [Header("Current Weapons")]
        [SerializeField] private CurrentWeaponsSO currentWeaponsSO;
        
        [SerializeField] private List<ItemParameter> parametersToModify;
        [SerializeField] private List<ItemParameter> itemCurrentState;

        [Header("Inventory")]
        [SerializeField] private InventorySO inventoryData;
        
        public void SetMeleeWeapon(WeaponItemSO weaponItemSO, List<ItemParameter> itemState)
        {
            if (currentWeaponsSO.EquipMeleeWeapon != null)
            {
                inventoryData.AddItem(currentWeaponsSO.EquipMeleeWeapon, 1, itemCurrentState);
            }
            
            currentWeaponsSO.EquipMeleeWeapon = weaponItemSO;
            this.itemCurrentState = new List<ItemParameter>(itemState); 
            ModifyParameters();
        }
        public void SetRangeWeapon(WeaponItemSO weaponItemSO, List<ItemParameter> itemState)
        {
            if (currentWeaponsSO.EquipRangeWeapon != null)
            {
                inventoryData.AddItem(currentWeaponsSO.EquipRangeWeapon, 1, itemCurrentState);
            }
            
            this.currentWeaponsSO.EquipRangeWeapon = weaponItemSO;
            this.itemCurrentState = new List<ItemParameter>(itemState); 
            ModifyParameters();
        }
        private void ModifyParameters()
        {
            foreach (var parameter in parametersToModify)
            {
                if (itemCurrentState.Contains(parameter))
                {
                    int index = itemCurrentState.IndexOf(parameter);
                    float newValue = itemCurrentState[index].value + parameter.value;
                    itemCurrentState[index] = new ItemParameter
                    {
                        itemParameter = parameter.itemParameter,
                        value = newValue,
                    };
                }
            }
        }
    }
}
