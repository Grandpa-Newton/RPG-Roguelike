using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon 
{
    public WeaponSO WeaponData { get; }

    public void DealDamage();
}