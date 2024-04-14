using System;
using System.Collections.Generic;
using App.Scripts.GameScenes.Weapon;
using App.Scripts.MixedScenes.Inventory.UI.ItemParameters;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace App.Scripts.DungeonScene.Items
{
    public abstract class ItemSO : ScriptableObject, ISalable
    {
        public int ID => GetInstanceID();

        [Title("Information")] [SerializeField]
        public string itemName;

        [SerializeField] [TextArea] public string description;

        [LabelText("RarityParameters SO")] [Title("Item Rarity Parameters")]
        public ItemRarityParametersSO weaponRarityParameters;

        [Title("Item Rarity"), LabelText("Item Rarity"), VerticalGroup("SetParameters")]
        public RarityEnum itemRarity;

        [field: SerializeField, VerticalGroup("SetParameters"), LabelText("Buy Cost")]
        public int ItemBuyCost { get; set; }

        public int ItemSellCost
        {
            get => itemSellCost;
            set => itemSellCost = value;
        }

        [ReadOnly, SerializeField] [VerticalGroup("SetParameters")] private int itemSellCost;


        [Button, VerticalGroup("SetParameters")]
        private void SetItemParameterByRarity()
        {
            double itemSellCost = 0;
            double reminder = 0;
            switch (itemRarity)
            {

                case (RarityEnum.Common):
                    itemSellCost = ItemBuyCost * weaponRarityParameters.costPercentagesOfFullPrice[RarityEnum.Common];
                    itemLight = weaponRarityParameters.availableLights[RarityEnum.Common];
                    break;
                case (RarityEnum.Uncommon):
                    itemSellCost = (ItemBuyCost *
                                         weaponRarityParameters.costPercentagesOfFullPrice[RarityEnum.Uncommon]);
                    itemLight = weaponRarityParameters.availableLights[RarityEnum.Uncommon];
                    break;
                case (RarityEnum.Rare):
                    itemSellCost =
                        (ItemBuyCost * weaponRarityParameters.costPercentagesOfFullPrice[RarityEnum.Rare]);
                    itemLight = weaponRarityParameters.availableLights[RarityEnum.Rare];
                    break;
                case (RarityEnum.Epic):
                    itemSellCost = (ItemBuyCost * weaponRarityParameters.costPercentagesOfFullPrice[RarityEnum.Epic]);
                    itemLight = weaponRarityParameters.availableLights[RarityEnum.Epic];
                    break;
                case (RarityEnum.Legendary):
                    itemSellCost = (ItemBuyCost * weaponRarityParameters.costPercentagesOfFullPrice[RarityEnum.Legendary]);
                    itemLight = weaponRarityParameters.availableLights[RarityEnum.Legendary];
                    break;
                default:
                    itemSellCost = (ItemBuyCost * 0.1);
                    break;
            }

            reminder = itemSellCost - (double)Math.Truncate(itemSellCost);
            ItemSellCost = reminder > 0.95 ? (int)(itemSellCost + 1) : (int)itemSellCost;
        }

        [Title("Item Components")] [ReadOnly] public Light2D itemLight;

        [Title("Stackable Params"), VerticalGroup("Stack")]
        [field: SerializeField]
        [VerticalGroup("Stack")]
        public bool IsStackable { get; set; }

        [field: SerializeField] public int MaxStackSize { get; set; } = 1;
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