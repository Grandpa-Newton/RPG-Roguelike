using System.Collections;
using System.Collections.Generic;
using App.Scripts.GameScenes.Weapon;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyContent_", menuName = "Content/Enemy")]
public class EnemyContentSO : ScriptableObject
{
    public List<WeaponItemSO> weapons;
    [MinMaxSlider(1, 10)] public Vector2Int coinsToSpawn;
    [MinMaxSlider(0,5)] public Vector2 radiusToSpawn;
    [Range(0.1f,1f)] public float timeToSpawnNextCoin;
}
