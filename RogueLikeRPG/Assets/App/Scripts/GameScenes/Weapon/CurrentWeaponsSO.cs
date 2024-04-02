using UnityEngine;

namespace App.Scripts.GameScenes.Weapon
{
    [CreateAssetMenu(fileName = "CurrentWeapons_", menuName = "Weapons/Current Weapons")] 
    public class CurrentWeaponsSO : ScriptableObject
    {
        public WeaponItemSO EquipMeleeWeapon;
        public WeaponItemSO EquipRangeWeapon;
    }
}
