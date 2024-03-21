using System.Collections.Generic;
using App.Scripts.DungeonScene.Items;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace App.Scripts.MixedScenes.Weapon
{
    [CreateAssetMenu(fileName = "ItemRarityParameters_", menuName = "Weapons/Item Rarity Parameters")]
    public class ItemRarityParametersSO : SerializedScriptableObject
    { 
        [ShowInInspector][OdinSerialize] public Dictionary<RarityEnum, Light2D> availableLights = new Dictionary<RarityEnum, Light2D>();
        [ShowInInspector][OdinSerialize] public Dictionary<RarityEnum, float> costPercentagesOfFullPrice = new Dictionary<RarityEnum, float>();
    }
}
