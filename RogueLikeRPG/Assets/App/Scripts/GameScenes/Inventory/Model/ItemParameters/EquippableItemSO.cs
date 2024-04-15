using System.Collections;
using System.Collections.Generic;
using App.Scripts.DungeonScene.Items;
using App.Scripts.GameScenes.Inventory.Model.ItemParameters;
using UnityEngine;

namespace App.Scripts.MixedScenes.Inventory.Model.ItemParameters
{
    [CreateAssetMenu]
    public class EquippableItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        
        public string ActionName => "Equip";

        public enum ItemType
        {
            MeleeWeapon,
            RangeWeapon,
            Armour,
        }

        public ItemType itemType;
        [field: SerializeField] public AudioClip itemActionSound { get; private set; }

        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            /*AgentWeapon weaponSystem = character.GetComponent<AgentWeapon>();
            IWeapon weapon;
            if (itemType == ItemType.MeleeWeapon)
            {
                SwitchWeaponBetweenRangeAndMelee.Instance.SetActiveMeleeWeapon();
                 weapon = GameObject.Find("MeleeWeapon").GetComponent<MeleeWeapon>();
                 if (weaponSystem != null)
                 {
                     weaponSystem.SetMeleeWeapon(this, itemState == null ? DefaultParametersList : itemState);
                     weapon.SetWeapon(this);
                 }
            }
            else if(itemType == ItemType.RangeWeapon)
            {
                SwitchWeaponBetweenRangeAndMelee.Instance.SetActiveRangeWeapon();
                 weapon = GameObject.Find("RangeWeapon").GetComponent<RangeWeapon>();
                 if (weaponSystem != null)
                 {
                     weaponSystem.SetRangeWeapon(this, itemState == null ? DefaultParametersList : itemState);
                     weapon.SetWeapon(this);
                 }
            }
            else if (itemType == ItemType.Armour)
            {
                //fdsl;fjsdlkj
            }
            */


            return false;
        }
    }
}