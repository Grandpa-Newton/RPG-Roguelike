using DefaultNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCell : MonoBehaviour
{
    [SerializeField] private GameObject isActiveCircle;
    [SerializeField] private bool isActive = false;
    public string SCENE_TO_LOAD { get; set; }

    public virtual void Interact()
    {
        Debug.Log("YES! VICTORY!");
    }

    [HideInInspector]
    public string CellId;

    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;
    [SerializeField] private Color currentColor;

    public List<GameObject> NeighborsCells = new List<GameObject>(); // клетки, на которые можно попасть из этой клетки

    protected Renderer _renderer;

    /*public bool IsActive // сделать enum: неактивный, активный и текущий (игрок находится на нём)
    {
        get => isActive;
        set
        {
            isActive = value;
            IsCellActive(); // ЛУЧШЕ СОБЫТИЕМ
        }
    }*/


    private CellType _cellType = CellType.Inactive;

    public CellType CellType
    {
        get => _cellType;

        set
        {
            _cellType = value;
            ChangeCellType();
        }
    }

    /*protected void IsCellActive()
    {
        if (isActive)
        {
            _renderer.material.color = activeColor;
        }
        else
        {
            _renderer.material.color = inactiveColor;
        }
    }*/

    protected void ChangeCellType()
    {
        switch (CellType)
        {
            case CellType.Inactive:
                _renderer.material.color = inactiveColor;
                break;
            case CellType.Active:
                _renderer.material.color = activeColor;
                break;
            case CellType.Current:
                _renderer.material.color = currentColor;
                break;
        }
    }

    protected void ConfigureObject()
    {
        CellId = name + transform.position.ToString();
        _renderer = GetComponent<Renderer>();
        // Сделать этот метод событием вызываемом при каждой загрузке карты
        ChangeCellType();
        //IsCellActive();
    }
}
