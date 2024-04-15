using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using App.Scripts.AllScenes.UI;
using App.Scripts.GameScenes.Player;
using App.Scripts.MapScene.Cells;
using UnityEngine.SceneManagement;
using Cinemachine;
using Unity.VisualScripting;

public class NextLevelStarter : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private StartShowDeLoadPanel _startShowDeLoadPanel;
    
    public void InCurrentCell() // когда игрок дошёл до новой клетки (мб поменять название)
    {
        BaseCell cell = gameObject.GetComponent<BaseCell>();
        MapInputManager.Instance.TurnOnSelectCell(cell.CellData.SceneToLoad);
    }

}