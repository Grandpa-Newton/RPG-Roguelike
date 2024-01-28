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
    [SerializeField] private GameObject _wallEffect;

    [SerializeField] public GameObject _door;

    [Header("Enemies")] 
    [SerializeField] private GameObject[] _enemyTypes;
    [SerializeField] private Transform[] _enemySpawners;

    [Header("PowerUps")] 
    [SerializeField] private GameObject _health;
    [SerializeField] private GameObject _shield;

    [SerializeField] public List<GameObject> Enemies;

    private RoomVariants _roomVariants;
    private bool _spawned;
    private bool _wallsDestroyed;

    private void Awake()
    {
        _roomVariants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
    }

    private void Start()
    {
        _roomVariants.rooms.Add(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
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
        PlayerController.Instance.OnKeyButtonDown();
    }

    public void DestroyWalls()
    {
        foreach (GameObject wall in _walls)
        {
            if (wall != null && wall.transform.childCount != 0)
            {
                Instantiate(_wallEffect, wall.transform.position, Quaternion.identity);
                Destroy(wall);
                Debug.Log("I destroyer");
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