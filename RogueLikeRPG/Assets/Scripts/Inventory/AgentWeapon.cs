using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;
using UnityEngine.Serialization;

public class AgentWeapon : MonoBehaviour
    {
        [SerializeField] private EquippableItemSO equpWeapon;
        [SerializeField] private InventorySO inventoryData;
        [SerializeField] private List<ItemParameter> parametersToModify;
        [SerializeField] private List<ItemParameter> itemCurrentState;

        public void SetWeapon(EquippableItemSO weaponItemSO, List<ItemParameter> itemState)
        {
            if (equpWeapon != null)
            {
                inventoryData.AddItem(equpWeapon, 1, itemCurrentState);
            }

            this.equpWeapon = weaponItemSO;
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
