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
    
    private void Start()
    {
        _playerController = Player.GetComponent<PlayerController>();
        _mapPlayerController = Player.GetComponent<MapPlayerController>();
    }

    public void InCurrentCell() // ����� ����� ����� �� ����� ������
    {
        MapPlayerController.Instance.OnInteractCell += Instance_OnInteractCell;
    }

    private void Instance_OnInteractCell()
    {
        // DontDestroyOnLoad(Player.gameObject);
        BaseCell cell = gameObject.GetComponent<BaseCell>();

        MapPlayerController.Instance.OnInteractCell -= Instance_OnInteractCell; // ???
        SceneManager.LoadScene(cell.SCENE_TO_LOAD);
        _playerController.enabled = true;
        _mapPlayerController.enabled = false;
    }

    private void Update()
    {
    }
}