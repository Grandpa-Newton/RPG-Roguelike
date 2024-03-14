using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class EquippableItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        public string ActionName => "Equip";
        [field: SerializeField] public AudioClip actionSFX { get; private set; }
        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            AgentWeapon weaponSystem = character.GetComponent<AgentWeapon>();
            Debug.Log(weaponSystem + ": weapon system");
            if (weaponSystem != null)
            {
                weaponSystem.SetWeapon(this, itemState == null ? DefaultParametersList : itemState);
                return true;
            }

            return false;
        }
    }
    
}