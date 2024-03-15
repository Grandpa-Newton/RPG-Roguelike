using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;
using UnityEngine.Serialization;

public class AgentWeapon : MonoBehaviour
    {
        [Header("Current Weapons")]
        [SerializeField] private WeaponItemSO equipMeleeWeapon;
        [SerializeField] private WeaponItemSO equipRangeWeapon;
        
        [SerializeField] private List<ItemParameter> parametersToModify;
        [SerializeField] private List<ItemParameter> itemCurrentState;

        [Header("Inventory")]
        [SerializeField] private InventorySO inventoryData;
        
        public void SetMeleeWeapon(WeaponItemSO weaponItemSO, List<ItemParameter> itemState)
        {
            if (equipMeleeWeapon != null)
            {
                inventoryData.AddItem(equipMeleeWeapon, 1, itemCurrentState);
            }
            
            this.equipMeleeWeapon = weaponItemSO;
            this.itemCurrentState = new List<ItemParameter>(itemState); 
            ModifyParameters();
        }
        public void SetRangeWeapon(WeaponItemSO weaponItemSO, List<ItemParameter> itemState)
        {
            if (equipRangeWeapon != null)
            {
                inventoryData.AddItem(equipRangeWeapon, 1, itemCurrentState);
            }
            
            this.equipRangeWeapon = weaponItemSO;
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
