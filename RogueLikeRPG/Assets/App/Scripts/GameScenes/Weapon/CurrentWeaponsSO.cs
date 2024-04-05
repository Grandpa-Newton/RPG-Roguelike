using App.Scripts.GameScenes.Weapon.MeleeWeapon;
using App.Scripts.GameScenes.Weapon.RangeWeapon;
using UnityEngine;
using UnityEngine.Serialization;

namespace App.Scripts.GameScenes.Weapon
{
    [CreateAssetMenu(fileName = "CurrentWeapons_", menuName = "Weapons/Current Weapons")] 
    public class CurrentWeaponsSO : ScriptableObject
    {
        public MeleeWeaponSO EquippedMeleeWeapon; 
        public RangeWeaponSO EquippedRangeWeapon;
    }
}
