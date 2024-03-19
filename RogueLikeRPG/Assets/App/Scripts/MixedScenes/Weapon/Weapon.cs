using Inventory.Model;
using UnityEngine;

namespace App.Scripts.MixedScenes.Weapon
{
    public abstract class Weapon : MonoBehaviour
    {
        public abstract void DealDamage();

        public abstract void SetWeapon(ItemSO weaponSo);
    }
}
