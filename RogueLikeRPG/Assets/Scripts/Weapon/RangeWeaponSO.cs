using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "RangeWeapon_", menuName = "Weapons/Range Weapon")]
public class RangeWeaponSO : WeaponSO
{
    public BulletSO bulletSO;
    public float bulletSpeed;
    public uint magSize;
    public float reloadTime;
}
