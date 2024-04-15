using System.Collections.Generic;
using App.Scripts.DungeonScene.Items;
using App.Scripts.GameScenes.Player;
using App.Scripts.GameScenes.Player.Components;
using UnityEngine;

namespace App.Scripts.GameScenes.Weapon.MeleeWeapon
{
    [CreateAssetMenu(fileName = "MeleeWeapon_", menuName = "Weapons/Melee Weapon")]
    public class MeleeWeaponSO : WeaponItemSO
    {
        public override bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            PlayerCurrentWeapon.Instance.SetPlayerCurrentWeapon(this);
            PlayerWeaponSwitcher.Instance.SetActiveWeapon(true);

            PlayerWeapon.Instance.SetMeleeWeapon(this, itemState ?? DefaultParametersList);
            MeleeWeapon.Instance.SetWeapon(this);

            return false;
        }
    }
}