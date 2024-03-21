using System;
using System.Collections;
using System.Collections.Generic;
using App.Scripts.MixedScenes.Inventory.UI.ItemParameters;
using App.Scripts.MixedScenes.Weapon;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;
using static UnityEngine.Rendering.DebugUI;

namespace Inventory.Model
{
    public abstract class ItemSO : ScriptableObject, ISalable
    {
        public int ID => GetInstanceID();
        
        [Title("Information")] 
        [SerializeField] public string itemName;
        [SerializeField] [TextArea] public string description;

        [Title("Item Rarity"), LabelText("Item Rarity"), VerticalGroup("SetParameters")]
        public RarityEnum itemRarity;
        [field: SerializeField, VerticalGroup("SetParameters"), LabelText("Buy Cost")]
        public int ItemBuyCost { get; set; }
        public int ItemSellCost
        {
            get { return _itemSellCost; }
            set { _itemSellCost = value; }
        }
        [ReadOnly, SerializeField][VerticalGroup("SetParameters")] private int _itemSellCost;
        

        [Button, VerticalGroup("SetParameters")]
        private void SetItemParameterByRarity()
        {
            switch (itemRarity)
            {
                case(RarityEnum.Common):
                    ItemSellCost = (int)(ItemBuyCost * weaponLights.costPercentagesOfFullPrice[RarityEnum.Common]);
                    itemLight = weaponLights.avaibleLights[RarityEnum.Common];
                    break;
                case(RarityEnum.Uncommon):
                    ItemSellCost = (int)(ItemBuyCost * weaponLights.costPercentagesOfFullPrice[RarityEnum.Uncommon]);
                    itemLight = weaponLights.avaibleLights[RarityEnum.Uncommon];
                    break;
                case(RarityEnum.Rare):
                    ItemSellCost = (int)(ItemBuyCost * weaponLights.costPercentagesOfFullPrice[RarityEnum.Rare]);
                    itemLight = weaponLights.avaibleLights[RarityEnum.Rare];
                    break;
                case(RarityEnum.Epic):
                    ItemSellCost = (int)(ItemBuyCost * weaponLights.costPercentagesOfFullPrice[RarityEnum.Epic]);
                    itemLight = weaponLights.avaibleLights[RarityEnum.Epic];
                    break;
                case(RarityEnum.Legendary):
                    ItemSellCost = (int)(ItemBuyCost * weaponLights.costPercentagesOfFullPrice[RarityEnum.Legendary]);
                    itemLight = weaponLights.avaibleLights[RarityEnum.Legendary];
                    break;
                default:
                    ItemSellCost = (int)(ItemBuyCost * 0.1);
                    break;
            }  
           
        }

        [Title("Weapon Lights")] public WeaponRarityParametersSO weaponLights;
        [Title("Item Components")][ReadOnly]
        public Light2D itemLight;
        
        [Title("Stackable Params"), VerticalGroup("Stack")]
        [field: SerializeField][VerticalGroup("Stack")] public bool IsStackable { get; set; }
        [field: SerializeField] public int MaxStackSize { get; set; } = 1;
        [field: SerializeField] public Sprite ItemImage { get; set; }
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