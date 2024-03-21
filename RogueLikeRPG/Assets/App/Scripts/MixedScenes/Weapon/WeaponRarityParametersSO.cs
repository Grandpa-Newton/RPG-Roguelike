using System.Collections.Generic;
using Inventory.Model;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace App.Scripts.MixedScenes.Weapon
{
    [CreateAssetMenu(fileName = "WeaponRarityParameters_", menuName = "Weapons/Weapon Rarity Parameters")]
    public class WeaponRarityParametersSO : SerializedScriptableObject
    { 
        [ShowInInspector][OdinSerialize] public Dictionary<RarityEnum, Light2D> avaibleLights = new Dictionary<RarityEnum, Light2D>();
        [ShowInInspector][OdinSerialize] public Dictionary<RarityEnum, float> costPercentagesOfFullPrice = new Dictionary<RarityEnum, float>();
    }
}
