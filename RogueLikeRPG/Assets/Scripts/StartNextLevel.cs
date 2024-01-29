using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Cinemachine;
using Unity.VisualScripting;

public class StartNextLevel : MonoBehaviour
{
    public Transform Player;
    private PlayerController _playerController;
    private MapPlayerController _mapPlayerController;

    // private bool _playerInNextLevelCell = false;
    // private bool _isCurrentCell = false;
    
    private void Start()
    {
        _playerController = Player.GetComponent<PlayerController>();
        _mapPlayerController = Player.GetComponent<MapPlayerController>();
    }

    /*private void Instance_OnActiveCell()
    {
        _isCurrentCell = true;
        MapPlayerController.Instance.OnCurrentCell += MapPlayerController_OnCurrentCell;
    }*/

    public void InCurrentCell() // когда игрок дошёл до новой клетки
    {
        // _playerInNextLevelCell = true;
        MapPlayerController.Instance.OnInteractCell += Instance_OnInteractCell;
    }

    private void Instance_OnInteractCell()
    {
        DontDestroyOnLoad(Player.gameObject);
        BaseCell cell = gameObject.GetComponent<BaseCell>();
        // _isCurrentCell = false;
        // _playerInNextLevelCell = false;
        SceneManager.LoadScene(cell.SCENE_TO_LOAD);
        _playerController.enabled = true;
        _mapPlayerController.enabled = false;
    }

    private void Update()
    {
    }
}