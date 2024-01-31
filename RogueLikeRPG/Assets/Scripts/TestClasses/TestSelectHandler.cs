using DefaultNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestSelectHandler : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private CellType _previousType;
    public void OnDeselect(BaseEventData eventData)
    {
        gameObject.GetComponent<BaseCell>().CellType = CellType.Active;
    }

    public void OnSelect(BaseEventData eventData)
    {
        gameObject.GetComponent<BaseCell>().CellType = DefaultNamespace.CellType.Selecting;
        BaseCell currentCell = gameObject.GetComponent<BaseCell>();
        _previousType = currentCell.CellType;
        currentCell.CellType = DefaultNamespace.CellType.Selecting;
    }
}