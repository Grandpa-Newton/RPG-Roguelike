using System.Collections.Generic;
using App.Scripts.MixedScenes.Player;
using Inventory.Model;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.MixedScenes.Weapon.RangeWeapon
{
    [CreateAssetMenu(fileName = "RangeWeapon_", menuName = "Weapons/Range Weapon")]
    public class RangeWeaponSO : WeaponItemSO
    {
        [VerticalGroup("WeaponStats")]
        [Range(1, 50)]public uint magSize;
        [VerticalGroup("WeaponStats")]
        public float reloadTime;
        [Title("Bullet Stats"),VerticalGroup("BulletStats")]
        public BulletSO bulletSO;
        [VerticalGroup("BulletStats")]
        public float bulletSpeed;


        public override bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            PlayerWeapon weaponSystem = character.GetComponent<PlayerWeapon>();
            Weapon weapon = null;
        
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
