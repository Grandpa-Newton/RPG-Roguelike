using System.Collections.Generic;
using App.Scripts.MixedScenes.Player;
using Inventory.Model;
using UnityEngine;

namespace App.Scripts.MixedScenes.Weapon.MeleeWeapon
{
    [CreateAssetMenu(fileName = "MeleeWeapon_", menuName = "Weapons/Melee Weapon")]
    public class MeleeWeaponSO : WeaponItemSO
    {
        public override bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            PlayerWeapon weaponSystem = character.GetComponent<PlayerWeapon>();
            Weapon weapon = null;
        
            SwitchWeaponBetweenRangeAndMelee.Instance.SetActiveMeleeWeapon();
            weapon = GameObject.Find("MeleeWeapon").GetComponent<MeleeWeapon>();
            if (weaponSystem != null)
            {
                weaponSystem.SetMeleeWeapon(this, itemState == null ? DefaultParametersList : itemState);
                weapon.SetWeapon(this);
            }

            return false;
        }
    }
}
