using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : IWeapon
{
    private MeleeWeaponSO _meleeWeaponData;
    public  WeaponSO WeaponData => _meleeWeaponData;
    
    public MeleeWeapon(MeleeWeaponSO data)
    {
        _meleeWeaponData = data;
    }

    public void DealDamage()
    {
        throw new System.NotImplementedException();
    }
}
