using System;
using System.Collections;
using System.Collections.Generic;
using App.Scripts.MixedScenes.Inventory.UI.ItemParameters;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;
using static UnityEngine.Rendering.DebugUI;

namespace Inventory.Model
{
    public abstract class ItemSO : ScriptableObject, ISalable
    {
        [Title("Information")] 
        [SerializeField] public string itemName;
        [SerializeField] [TextArea] public string description;

        [field: SerializeField, HorizontalGroup("Trading"), LabelText("Buy Cost")]
        public int ItemBuyCost { get; set; }
        public int ItemSellCost
        {
            get { return _itemSellCost; }
            set { _itemSellCost = value; }
        }
        [ReadOnly, SerializeField] private int _itemSellCost;
        [Title("Item Rarity"), LabelText("Rarity"), VerticalGroup("ItemRarity")]
        public RarityEnum itemRarity;

        [Button, HorizontalGroup("Trading", Width = 0.6f)]
        private void CalculateSellCost()
        {
            switch (itemRarity)
            {
                case(RarityEnum.Common):
                    ItemSellCost = (int)(ItemBuyCost * 0.8);
                    break;
                case(RarityEnum.Uncommon):
                    ItemSellCost = (int)(ItemBuyCost * 0.7);
                    break;
                case(RarityEnum.Rare):
                    ItemSellCost = (int)(ItemBuyCost * 0.6);
                    break;
                case(RarityEnum.Epic):
                    break;
                case(RarityEnum.Legendary):
                    ItemSellCost = (int)(ItemBuyCost * 0.4);
                    break;
                default:
                    ItemSellCost = (int)(ItemBuyCost * 0.1);
                    break;
            }  
           
        }
        
        [field: SerializeField] public bool IsStackable { get; set; }
        public int ID => GetInstanceID();

        [field: SerializeField] public int MaxStackSize { get; set; } = 1;

        [Title("Item Components")]
        public Light2D itemLight;
        
        public void SetItemLight()
        {
            switch (itemRarity)
            {
                case(RarityEnum.Common):
                    itemLight.color = Color.gray;
                    break;
                case(RarityEnum.Uncommon):
                    itemLight.color = Color.green;
                    break;
                case(RarityEnum.Rare):
                    itemLight.color = Color.blue;
                    break;
                case(RarityEnum.Epic):
                    itemLight.color = Color.magenta;
                    break;
                case(RarityEnum.Legendary):
                    itemLight.color = Color.yellow;
                    break;
            }  
        }
        [field: SerializeField] public Sprite ItemImage { get; set; }
        

        [field: SerializeField] public List<ItemParameter> DefaultParametersList { get; set; }


        
    }
    
    public enum RarityEnum
    {
        Common = 1,
        Uncommon,
        Rare,
        Epic,
        Legendary
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