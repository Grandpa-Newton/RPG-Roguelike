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
        [field: SerializeField] public bool IsStackable { get; set; }
        public int ID => GetInstanceID();

        [field: SerializeField] public int MaxStackSize { get; set; } = 1;
        [field: SerializeField] public string Name { get; set; }

        [field: SerializeField]
        [field: TextArea]
        public string Description { get; set; }

        public Light2D ItemLight;
        [field: SerializeField] public Sprite ItemImage { get; set; }



        [field: SerializeField]
        public List<ItemParameter> DefaultParametersList { get; set; }


        [field: SerializeField, HorizontalGroup("ayaya"), LabelText("Buy Cost")]
        public int ItemBuyCost
        {
            get; set;
        }

        [ReadOnly, SerializeField]
        private int _itemSellCost;


        [Button, HorizontalGroup("ayaya", Width = 0.6f)]
        private void CalculateSellCost()
        {
            ItemSellCost = (int)(ItemBuyCost * 0.75);
        }


        public int ItemSellCost
        {
            get
            {
                return _itemSellCost;
            }
            set
            {
                _itemSellCost = value;
            }
        }
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