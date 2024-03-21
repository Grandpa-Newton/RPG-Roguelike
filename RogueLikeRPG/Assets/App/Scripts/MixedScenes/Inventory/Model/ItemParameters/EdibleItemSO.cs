using System;
using System.Collections.Generic;
using App.Scripts.DungeonScene.Items;
using App.Scripts.MixedScenes.Inventory.Model.StatsModifiers;
using UnityEngine;

namespace App.Scripts.MixedScenes.Inventory.Model.ItemParameters
{
    [CreateAssetMenu]
    public class EdibleItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        [SerializeField] private List<ModifierData> modifierData = new List<ModifierData>();
        public string ActionName => "Consume";
        [field: SerializeField] public AudioClip itemActionSound { get; private set; }
        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            foreach (var data in modifierData)
            {
                data.statModifier.AffectCharacter(character, data.value);
            }
            return true;
        }
    }
    public interface IDestroyableItem
    {
        
    }
    
    public interface IItemAction
    {
        public string ActionName { get; }
        public AudioClip itemActionSound { get; }
        bool PerformAction(GameObject character, List<ItemParameter> itemState);
    }

    [Serializable]
    public class ModifierData
    {
        public CharacterStatModifierSO statModifier;
        public float value;

    }
}