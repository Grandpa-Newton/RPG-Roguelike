using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;
    public class AgentWeapon : MonoBehaviour
    {
        [SerializeField] private EquippableItemSO weapon;
        [SerializeField] private InventorySO inventoryData;
        [SerializeField] private List<ItemParameter> parametersToModify;
        [SerializeField] private List<ItemParameter> itemCurrentState;

        public void SetWeapon(EquippableItemSO weaponItemSO, List<ItemParameter> itemState)
        {
            if (weapon != null)
            {
                inventoryData.AddItem(weapon, 1, itemCurrentState);
            }

            this.weapon = weaponItemSO;
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
