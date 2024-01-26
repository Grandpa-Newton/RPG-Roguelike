using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCell : MonoBehaviour, IBaseCell
{
    [SerializeField] private bool isActive = false;
    [SerializeField] private GameObject isActiveCircle;

    [HideInInspector]
    public string CellId;

    public List<GameObject> NeighborsCells = new(); // клетки, на которые можно попасть из этой клетки

    private Renderer _renderer;

    public bool IsActive // сделать enum: неактивный, активный и текущий (игрок находится на нём)
    {
        get => isActive;
        set
        {
            isActive = value; 
            IsCellActive(); // ЛУЧШЕ СОБЫТИЕМ
        }
    }

    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;

    private void Awake()
    {
        CellId = name + transform.position.ToString();
        _renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        MapPlayerController.Instance.OnActiveCell += NormalCell_OnActiveCell;
        // Сделать этот метод событием вызываемом при каждой загрузке карты
        IsCellActive();
    }
    private void NormalCell_OnActiveCell()
    {
        /*if (isActive)
        {
            isActiveCircle.SetActive(true);
        }
        else
        {
            isActiveCircle.SetActive(false);
        }*/
    }
    public void Interact()
    {
        /*if (!IsActive)
        {
            Debug.Log("Cell is not active!");
        }
        else
        {
            Debug.Log("YES! VICTORY!");
        }*/
        Debug.Log("YES! VICTORY!");

    }

    private void IsCellActive()
    {
        if (isActive)
        {
            _renderer.material.color = activeColor;
        }
        else
        {
            _renderer.material.color = inactiveColor;
        }
    }
}