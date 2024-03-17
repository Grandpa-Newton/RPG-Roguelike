using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ChestOpen : MonoBehaviour
{
    [SerializeField] private ChestContentSO chestContent;
    [SerializeField] private Canvas uiCanvas;
    [SerializeField] private ItemPickable coin;
    private Animator _animator;
    
    private bool _isChestOpened = false;
    private bool _isInsideTrigger = false;

    void Start()
    {
        _animator = GetComponent<Animator>();
        uiCanvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_isInsideTrigger && Input.GetKeyDown(KeyCode.E) && !_isChestOpened)
        {
            StartChestOpeningAnimation();
            SpawnWeapon();
            StartCoroutine(SpawnCoinsOnChestOpen());
        }
    }

    private IEnumerator  SpawnCoinsOnChestOpen()
    {
        int countToSpawn = 0;
        while (countToSpawn < chestContent.coinsToSpawn)
        {
            yield return StartCoroutine(WaitToSpawnNextCoin());
            ItemPickable spawnedCoin = Instantiate(coin, transform.position, Quaternion.identity);
        
            // Генерируем случайный угол
            float angle = Random.Range(0, 360);
            Vector3 direction = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle) , 0);
        
            // Вычисляем конечную позицию
            Vector3 targetPosition = transform.position + direction * chestContent.radiusToSpawn;
        
            // Перемещаем монету к целевой позиции
            spawnedCoin.transform.DOMove(targetPosition, 0.5f);
        
            countToSpawn++;
        }
    }

    IEnumerator WaitToSpawnNextCoin()
    {
        yield return new WaitForSeconds(chestContent.timeToSpawnNextCoin);
    }
    private void StartChestOpeningAnimation()
    {
        _isChestOpened = true;
        _animator.SetBool("IsOpen", _isChestOpened);
        uiCanvas.gameObject.SetActive(false);
    }
    private void SpawnWeapon()
    {
        int randomIndex = Random.Range(0, chestContent.weapons.Count);
        WeaponItemSO weapon = chestContent.weapons[randomIndex];
            
        ItemPickable spawnedItem = Instantiate(weapon.weaponPrefab, transform.position, Quaternion.identity);
        spawnedItem.Initialize(weapon);
        spawnedItem.transform.DOMove(new Vector3(-0.5f, 2.5f, 0f), 0.5f);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() && !_isChestOpened)
        {
            _isInsideTrigger = true;
            uiCanvas.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() && !_isChestOpened)
        {
            _isInsideTrigger = false;
            uiCanvas.gameObject.SetActive(false);
        }
    }
}