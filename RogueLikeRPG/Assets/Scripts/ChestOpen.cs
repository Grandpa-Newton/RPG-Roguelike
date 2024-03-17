using System;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class ChestOpen : MonoBehaviour
{
    [SerializeField] private WeaponListSO listOfDropbleItems;
    [SerializeField] private Canvas uiCanvas;
    
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
        }
    }

    private void StartChestOpeningAnimation()
    {
        _isChestOpened = true;
        _animator.SetBool("IsOpen", _isChestOpened);
        uiCanvas.gameObject.SetActive(false);
    }
    private void SpawnWeapon()
    {
        int randomIndex = Random.Range(0, listOfDropbleItems.weapons.Count);
        WeaponItemSO weapon = listOfDropbleItems.weapons[randomIndex];
            
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