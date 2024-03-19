using System.Collections.Generic;
using App.Scripts.MixedScenes.Inventory.Model.ItemParameters;
using App.Scripts.MixedScenes.PickUpSystem;
using Inventory.Model;
using UnityEngine;

namespace App.Scripts.MixedScenes.Weapon
{
    [CreateAssetMenu(fileName = "Weapon_", menuName = "Weapons")]
    public abstract class WeaponItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        //public WeaponSO playerWeapon;
    
        [Range(0.01f, 10)] public float attackRate;
        [Range(0, 100)] public float damage;
    
        public AudioClip weaponAttackSound;
        public string ActionName => "Equip";
        public AudioClip itemActionSound { get; }
    
        public ItemPickable weaponPrefab;
        public abstract bool PerformAction(GameObject character, List<ItemParameter> itemState = null);

    }
}