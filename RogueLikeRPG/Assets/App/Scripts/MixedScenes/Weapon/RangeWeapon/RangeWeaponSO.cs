using System.Collections.Generic;
using App.Scripts.MixedScenes.Player;
using Inventory.Model;
using UnityEngine;

namespace App.Scripts.MixedScenes.Weapon.RangeWeapon
{
    [CreateAssetMenu(fileName = "RangeWeapon_", menuName = "Weapons/Range Weapon")]
    public class RangeWeaponSO : WeaponItemSO
    {
        public BulletSO bulletSO;
        public float bulletSpeed;
        public uint magSize;
        public float reloadTime;


        public override bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            PlayerWeapon weaponSystem = character.GetComponent<PlayerWeapon>();
            global::App.Scripts.MixedScenes.Weapon.Weapon weapon;
        
            SwitchWeaponBetweenRangeAndMelee.Instance.SetActiveRangeWeapon();
            weapon = GameObject.Find("RangeWeapon").GetComponent<RangeWeapon>();
            if (weaponSystem != null)
            {
                weaponSystem.SetRangeWeapon(this, itemState == null ? DefaultParametersList : itemState);
                weapon.SetWeapon(this);
            }
            return false;
        }
    }
}
