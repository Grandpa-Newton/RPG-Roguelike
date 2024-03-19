using System;
using System.Collections;
using System.Collections.Generic;
using App.Scripts.MapScene.Cells;
using App.Scripts.MixedScenes.Player.Control;
using UnityEngine;
using UnityEngine.EventSystems;

public class CellsSelectHandler : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public Action OnChangeSelectingCell;
    private CellType _previousType;
    public void OnDeselect(BaseEventData eventData)
    {
        gameObject.GetComponent<BaseCell>().CellType = CellType.Active;
    }

    public void OnSelect(BaseEventData eventData)
    {
        gameObject.GetComponent<BaseCell>().CellType = CellType.Selecting;
        MapPlayerController.Instance.SelectingCell = gameObject;
        BaseCell currentCell = gameObject.GetComponent<BaseCell>();
        _previousType = currentCell.CellType;
        currentCell.CellType = CellType.Selecting;
        OnChangeSelectingCell?.Invoke();
    }
}