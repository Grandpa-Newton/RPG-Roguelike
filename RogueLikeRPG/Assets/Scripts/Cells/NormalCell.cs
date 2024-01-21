using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCell : MonoBehaviour, IBaseCell
{
    [SerializeField] private bool isActive = false;

    private Renderer _renderer;

    public bool IsActive
    {
        get => isActive;
        set => isActive = value;
    }

    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        // Сделать этот метод событием вызываемом при каждой загрузке карты
        IsCellActive();
    }

   public void Interact()
    {
        if (!IsActive)
        {
            Debug.Log("Cell is not active!");
        }
        else
        {
            Debug.Log("YES! VICTORY!");
        }
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