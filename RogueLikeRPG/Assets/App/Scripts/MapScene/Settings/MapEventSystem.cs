using System.Collections;
using System.Collections.Generic;
using App.Scripts.MixedScenes.Player.Control;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapEventSystem : EventSystem
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
