using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomContent : MonoBehaviour
{
   [Header("Walls")] 
   [SerializeField] private GameObject[] _walls;
   //[SerializeField] private GameObject _wallEffect;
   [SerializeField] private GameObject _door;

   [Header("Enemies")] 
   [SerializeField] private GameObject[] _enemyTypes;
   [SerializeField] private Transform[] _enemySpawners;

   [Header("PowerUps")] 
   [SerializeField] private GameObject _health;
   [SerializeField] private GameObject _shield;

   [HideInInspector] public List<GameObject> Enemies;

   private RoomVariants _roomVariants;
   private bool _spawned;
   private bool _wallsDestroyed;

   private void Start()
   {
      _roomVariants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      Debug.Log("Enter2d");
      if (other.CompareTag("Player") && !_spawned)
      {
         _spawned = true;

         foreach (Transform enemySpawner in _enemySpawners)
         {
            int rand = Random.Range(1, 11);
            if (rand <= 8)
            {
               GameObject enemyType = _enemyTypes[Random.Range(0, _enemyTypes.Length)];
               GameObject enemy = Instantiate(enemyType, enemySpawner.position, Quaternion.identity) as GameObject;
               enemy.transform.parent = transform;
               Enemies.Add(enemy);
            }
            else
            {
               //придумать другие варианты
               GameObject enemyType = _enemyTypes[Random.Range(0, _enemyTypes.Length)];
               GameObject enemy = Instantiate(enemyType, enemySpawner.position, Quaternion.identity);
               enemy.transform.parent = transform;
               Enemies.Add(enemy);
            }
         }

         StartCoroutine(CheckEnemies());
      }
   }

   IEnumerator CheckEnemies()
   {
      yield return new WaitForSeconds(1f);
      yield return new WaitUntil(() => Enemies.Count == 0);
         Debug.Log("Destroy walls started");
      DestroyWalls();
      
   }

   public void OnClearEnemies()
   {
      Debug.Log(Enemies.Count);
      Enemies.Clear();
   }
   private void DestroyWalls()
   {
      foreach (GameObject wall in _walls)
      {
         if (wall != null && wall.transform.childCount == 0)
         {
            //Instantiate(wallEffect);
            Destroy(wall);
         }
      }

      _wallsDestroyed = true;
   }

   private void OnTriggerStay2D(Collider2D other)
   {
      if (_wallsDestroyed && other.CompareTag("Wall"))
      {
         Destroy(other.gameObject);
      }
   }
}
