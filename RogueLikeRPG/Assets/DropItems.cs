using System;
using System.Collections;
using System.Collections.Generic;
using App.Scripts.DungeonScene.Enemy;
using App.Scripts.GameScenes.Weapon;
using App.Scripts.MixedScenes.PickUpSystem;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropItems : MonoBehaviour
{
   [SerializeField] private EnemyContentSO enemyContent;
   [SerializeField] private ItemPickable coin;
   private Enemy _enemy;
   private WeaponRarityStarsUI weaponRarityStarsUI;

   private void Awake()
   {
      _enemy = GetComponent<Enemy>();
      _enemy.OnEnemyDie += SpawnItems_OnEnemyDie;
   }
   
   private void SpawnItems_OnEnemyDie()
   {
      Debug.Log("StartCoroutine");
      StartCoroutine(SpawnObjects());
      SpawnWeapon();
   }

   private IEnumerator SpawnObjects()
   {
      Debug.Log("ContinueCoroutine");
      int coinsToSpawn = Random.Range(enemyContent.coinsToSpawn.x, enemyContent.coinsToSpawn.y);
         Debug.Log(coinsToSpawn);
      
      for (int i = 0; i < coinsToSpawn; i++)
      {
         yield return StartCoroutine(WaitToSpawnNextCoin());
         ItemPickable spawnedCoin = Instantiate(coin, transform.position, Quaternion.identity);
         Debug.Log(spawnedCoin);
         spawnedCoin.InitializeItem();

         float angle = Random.Range(0, 360);
         Vector3 direction = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0);

         float radius = Random.Range(enemyContent.radiusToSpawn.x, enemyContent.radiusToSpawn.y);
         Vector3 targetPosition = transform.position + direction * radius;

         spawnedCoin.transform.DOMove(targetPosition, 0.5f);
      }
      Destroy(_enemy.gameObject);
   }
   private void SpawnWeapon()
   {
      if (Random.Range(1, 10) == 1)
      {
         return;
      }
      int randomIndex = Random.Range(0, enemyContent.weapons.Count);
      WeaponItemSO weapon = enemyContent.weapons[randomIndex];

      ItemPickable spawnedItem = Instantiate(weapon.weaponPickablePrefab, transform.position, Quaternion.identity);
      weaponRarityStarsUI = spawnedItem.GetComponent<WeaponRarityStarsUI>();
      weaponRarityStarsUI.SetActiveWeaponStarsByRarity(weapon);
      spawnedItem.InitializeWeapon(weapon);
      spawnedItem.transform.DOMove(transform.position + new Vector3(0,0.5f,0), 0.5f);
   }
   IEnumerator WaitToSpawnNextCoin()
   {
      Debug.Log(enemyContent.timeToSpawnNextCoin);
      yield return new WaitForSeconds(enemyContent.timeToSpawnNextCoin);
   }
}
