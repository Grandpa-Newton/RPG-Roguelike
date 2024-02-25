using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RangeWeapon_", menuName = "Weapons/Range Weapon")]
public class RangeWeaponSO : WeaponSO
{
    public BulletSO bullet;
    public uint magSize;
    public float reloadTime;
}
