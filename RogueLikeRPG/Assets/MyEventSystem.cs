using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyEventSystem : EventSystem
{
    protected override void Start()
    {
        base.Start();
        MapPlayerController.Instance.OnDeselectCells += Instance_OnDeselectCells;
    }

    private void Instance_OnDeselectCells()
    {
        SetSelectedGameObject(null); // убирает выделение всех объектов
    }
}
