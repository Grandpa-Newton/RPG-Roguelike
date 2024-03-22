using System.Collections.Generic;
using App.Scripts.DungeonScene.Items;
using App.Scripts.MixedScenes.Player;
using UnityEngine;

namespace App.Scripts.MixedScenes.Weapon.MeleeWeapon
{
    [CreateAssetMenu(fileName = "MeleeWeapon_", menuName = "Weapons/Melee Weapon")]
    public class MeleeWeaponSO : WeaponItemSO
    {
        public override bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            PlayerWeapon weaponSystem = character.GetComponent<PlayerWeapon>();
        
            SwitchWeaponBetweenRangeAndMelee.Instance.SetActiveMeleeWeapon();
            Weapon weapon = GameObject.Find("MeleeWeapon").GetComponent<MeleeWeapon>();
            if (weaponSystem != null)
            {
                weaponSystem.SetMeleeWeapon(this, itemState ?? DefaultParametersList);
                weapon.SetWeapon(this);
            }

            return false;
        }
    }
}
