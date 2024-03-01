using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Cinemachine;
using Unity.VisualScripting;

public class NextLevelStarter : MonoBehaviour
{
    public Transform Player;
    private PlayerController _playerController;
    
    private void Awake()
    {
        _playerController = Player.GetComponent<PlayerController>();
    }

    public void InCurrentCell() // когда игрок дошёл до новой клетки (мб поменять название)
    {
        MapPlayerController.Instance.OnInteractCell += Instance_OnInteractCell;
    }

    private void Instance_OnInteractCell()
    {
        BaseCell cell = gameObject.GetComponent<BaseCell>();

        MapPlayerController.Instance.OnInteractCell -= Instance_OnInteractCell; // ???
        SceneManager.LoadScene(cell.CellData.SceneToLoad); // мб лучше сделать sceneToLoad не пабликом, а вызывать как-то событием / методом
                                                           // (теперь через SO сделано - не уверен)
        _playerController.enabled = true;
    }
}