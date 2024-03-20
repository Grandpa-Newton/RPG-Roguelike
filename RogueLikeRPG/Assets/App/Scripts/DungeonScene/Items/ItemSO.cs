using System;
using System.Collections;
using System.Collections.Generic;
using App.Scripts.MixedScenes.Inventory.UI.ItemParameters;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.Rendering.DebugUI;

namespace Inventory.Model
{
    public abstract class ItemSO : ScriptableObject, ISalable
    {
        [Title("Information")] 
        [SerializeField] public string itemName;
        [SerializeField] [TextArea] public string Description;

        [field: SerializeField, HorizontalGroup("Trading"), LabelText("Buy Cost")]
        public int ItemBuyCost { get; set; }
        public int ItemSellCost
        {
            get { return _itemSellCost; }
            set { _itemSellCost = value; }
        }
        [ReadOnly, SerializeField] private int _itemSellCost;


        [Button, HorizontalGroup("Trading", Width = 0.6f)]
        private void CalculateSellCost()
        {
            ItemSellCost = (int)(ItemBuyCost * 0.75);
        }

        [field: SerializeField] public bool IsStackable { get; set; }
        public int ID => GetInstanceID();

        [field: SerializeField] public int MaxStackSize { get; set; } = 1;

        [Title("Item Components")]
        public Light2D ItemLight;
        [field: SerializeField] public Sprite ItemImage { get; set; }


        [field: SerializeField] public List<ItemParameter> DefaultParametersList { get; set; }


        
    }


    [Serializable]
    public struct ItemParameter : IEquatable<ItemParameter>
    {
        public ItemParameterSO itemParameter;
        public float value;

        public bool Equals(ItemParameter other)
        {
            return other.itemParameter == itemParameter;
        }
    }
}