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
            SwitchWeaponBetweenRaM.Instance.SetActiveMeleeWeapon();

            PlayerWeapon.Instance.SetMeleeWeapon(this, itemState ?? DefaultParametersList);
            MeleeWeapon.Instance.SetWeapon(this);

            return false;
        }
    }
}