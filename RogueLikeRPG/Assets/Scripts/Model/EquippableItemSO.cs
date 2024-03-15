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
            IWeapon weapon;
            if (SwitchWeaponBetweenRangeAndMelee.Instance.GetWeaponState())
            {
                 weapon = GameObject.Find("MeleeWeapon").GetComponent<MeleeWeapon>();
                 if (weaponSystem != null)
                 {
                     weaponSystem.SetMeleeWeapon(this, itemState == null ? DefaultParametersList : itemState);
                     weapon.SetWeapon(playerWeapon);
                 }
            }
            else
            {
                 weapon = GameObject.Find("RangeWeapon").GetComponent<RangeWeapon>();
                 if (weaponSystem != null)
                 {
                     weaponSystem.SetRangeWeapon(this, itemState == null ? DefaultParametersList : itemState);
                     weapon.SetWeapon(playerWeapon);
                 }
            }


            return false;
        }
    }
}