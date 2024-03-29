using System.Collections.Generic;
using App.Scripts.DungeonScene.Items;
using App.Scripts.MixedScenes.Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.MixedScenes.Weapon.RangeWeapon
{
    [CreateAssetMenu(fileName = "RangeWeapon_", menuName = "Weapons/Range Weapon")]
    public class RangeWeaponSO : WeaponItemSO
    {
        private PlayerWeapon _playerWeapon;
        
        [VerticalGroup("WeaponStats")]
        [Range(1, 50)]public uint magSize;
        [VerticalGroup("WeaponStats")]
        public float reloadTime;
        [Title("Bullet Stats"),VerticalGroup("BulletStats")]
        public BulletSO bulletSO;
        [VerticalGroup("BulletStats")]
        public float bulletSpeed;

        public void Initialize(PlayerWeapon playerWeapon)
        {
            _playerWeapon = playerWeapon;
        }

        public override bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            SwitchWeaponBetweenRangeAndMelee.Instance.SetActiveRangeWeapon();
            PlayerWeapon.Instance.SetRangeWeapon(this,itemState ?? DefaultParametersList);
            RangeWeapon.Instance.SetWeapon(this);
            return false;
        }
    }
}
