using App.Scripts.MixedScenes.Weapon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CurrentWeapons_", menuName = "Weapons/Current Weapons")] 
public class CurrentWeaponsSO : ScriptableObject
{
    public WeaponItemSO EquipMeleeWeapon;
    public WeaponItemSO EquipRangeWeapon;
}
