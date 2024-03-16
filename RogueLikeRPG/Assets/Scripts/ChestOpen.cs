using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChestOpen : MonoBehaviour
{
    private Animator _animator;
    public event Action OnChestOpen;
    [SerializeField] private GameObject dropbleItem;
    [SerializeField] private Canvas uiCanvas;
    private bool isChestOpened = false;
    private bool isInsideTrigger = false;

    void Start()
    {
        _animator = GetComponent<Animator>();
        uiCanvas.gameObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() && !isChestOpened)
        {
            isInsideTrigger = true;
            uiCanvas.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() && !isChestOpened)
        {
            isInsideTrigger = false;
            uiCanvas.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (isInsideTrigger && Input.GetKeyDown(KeyCode.E) && !isChestOpened)
        {
            isChestOpened = true;
            _animator.SetBool("IsOpen", isChestOpened);
            uiCanvas.gameObject.SetActive(false);
            GameObject spawnedItem = Instantiate(dropbleItem, transform.position, Quaternion.identity);
            spawnedItem.transform.DOMove(new Vector3(-0.5f, 2.5f, 0f), 0.5f);
        }
    }

    
}