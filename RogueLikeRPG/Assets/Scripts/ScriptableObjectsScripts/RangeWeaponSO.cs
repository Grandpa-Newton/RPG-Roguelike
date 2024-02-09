using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RangeWeapon_", menuName = "RangeWeaponSO")]
public class RangeWeaponSO : ScriptableObject
{
    [Range(0.1f, 1)] public float fireRate;
    [Range(0, 100)] public float damage;
    public Bullet bullet;
    public Sprite weaponSprite;

    public RangeWeaponSO(RangeWeaponSO next_weapon)
    {
        fireRate = next_weapon.fireRate;
        damage = next_weapon.damage;
        bullet = next_weapon.bullet;
        weaponSprite = next_weapon.weaponSprite;

    }
    
}
