using System.Collections.Generic;
using App.Scripts.DungeonScene.Items;
using App.Scripts.MixedScenes.Inventory.Model.ItemParameters;
using App.Scripts.MixedScenes.PickUpSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.MixedScenes.Weapon
{
    public abstract class WeaponItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        public string ActionName => "Equip";
        
        [Title("Pickable Item")]
        [LabelText("Item Prefab")]
        public ItemPickable weaponPickablePrefab;
        
        [Title("Weapon Stats"), VerticalGroup("WeaponStats")]
        [Range(0.01f, 10)] public float attackRate;
        [Range(0, 100)] public float damage;
        
        [Title("Audio")]
        [LabelText("Shoot Sound")]public AudioClip weaponAttackSound;
        [LabelText("Equip Sound")]public AudioClip weaponEquipSound;
        public AudioClip itemActionSound { get; }
        

        public abstract bool PerformAction(GameObject character, List<ItemParameter> itemState = null);
    }

    public enum WeaponRarityEnum
    {
        Common = 1,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }
    
}
