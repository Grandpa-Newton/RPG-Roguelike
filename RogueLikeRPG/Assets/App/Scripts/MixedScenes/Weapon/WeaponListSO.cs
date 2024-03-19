using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.MixedScenes.Weapon
{
    [CreateAssetMenu(fileName = "WeaponsList_", menuName = "Weapons/Weapons List")]
    public class ChestContentSO : ScriptableObject
    {
        public List<WeaponItemSO> weapons;
        public int coinsToSpawn;
        public float radiusToSpawn;
        [Range(0.1f,1f)]public float timeToSpawnNextCoin;
    }
}
