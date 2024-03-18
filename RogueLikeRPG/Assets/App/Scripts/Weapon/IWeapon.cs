using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public WeaponItemSO WeaponData { get; }

    public abstract void DealDamage();

    public abstract void SetWeapon(ItemSO weaponSo);
}
