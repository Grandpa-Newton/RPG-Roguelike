using App.Scripts.DungeonScene.Items;
using App.Scripts.MixedScenes.Inventory.Model;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TraderInventoryGenerator : SerializedMonoBehaviour
{
    [SerializeField] private InventorySO traderInventory;

    //[ShowInInspector][OdinSerialize] public Dictionary<ItemSO, float> itemsAppearingProbabilities = new Dictionary<ItemSO, float>();

    [SerializeField] private List<ItemAppearingProbability> itemsAppearingProbabilities = new List<ItemAppearingProbability>();
    private void Awake()
    {
        traderInventory.Initialize();

        System.Random random = new System.Random();

        foreach (var item in itemsAppearingProbabilities)
        {
            float probability = (float)random.NextDouble();

            if(probability < item.Probability)
            {
                int quantity = random.Next(1, item.MaxValue + 1);
                traderInventory.AddItem(item.Item, quantity);
            }
        }
    }

    [Serializable]
    public class ItemAppearingProbability // отдельный класс для того, чтобы сделать список списков
    {
        public ItemSO Item;
        public float Probability;
        public int MaxValue;
    }
}
