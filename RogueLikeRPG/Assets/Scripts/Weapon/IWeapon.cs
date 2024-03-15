using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

public interface IWeapon 
{
    public WeaponItemSO WeaponData { get; }

    public void DealDamage();

    public void SetWeapon(ItemSO weaponSo);
}
