using App.Scripts.DungeonScene.Items;
using App.Scripts.MixedScenes.Inventory.Model;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TraderInventoryGenerator : SerializedMonoBehaviour
{
    [SerializeField] private InventorySO traderInventory;

    [ShowInInspector][OdinSerialize] public Dictionary<ItemSO, float> itemsAppearingProbabilities = new Dictionary<ItemSO, float>();
    private void Awake()
    {
        traderInventory.Initialize();

        System.Random random = new System.Random();

        foreach (var item in itemsAppearingProbabilities)
        {
            float probability = (float)random.NextDouble();

            if(probability < item.Value)
            {
                int quantity = random.Next(1, 4);
                traderInventory.AddItem(item.Key, quantity);
            }
        }
    }
}
