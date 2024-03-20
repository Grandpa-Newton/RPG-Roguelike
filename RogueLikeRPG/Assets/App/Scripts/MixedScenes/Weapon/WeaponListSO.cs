using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.MixedScenes.Weapon
{
    [CreateAssetMenu(fileName = "WeaponsList_", menuName = "Weapons/Weapons List")]
    public class ChestContentSO : ScriptableObject
    {
        public List<WeaponItemSO> weapons;
        [MinMaxSlider(10, 30)] public Vector2Int coinsToSpawn;
        [MinMaxSlider(1,5)] public Vector2 radiusToSpawn;
        [Range(0.1f,1f)]public float timeToSpawnNextCoin;
    }
}
