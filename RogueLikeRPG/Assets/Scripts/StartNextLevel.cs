using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Cinemachine;
using Unity.VisualScripting;

public class StartNextLevel : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _nextLevelCell; 
    private const string NEXT_SCENE_TO_LOAD = "TestScene";
    private PlayerController _playerController;
    private CinemachineVirtualCamera virtualCamera;

    private bool _playerInNextLevelCell = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(_player.gameObject);  
        _playerController = _player.GetComponent<PlayerController>();
    }

    // Update is called once per frame

    void Update()
    {
        if (_playerInNextLevelCell && Input.GetKeyDown(KeyCode.E))
        {
            // DontDestroyOnLoad(GameObject.Find("MapLoader")); // наверное, по-другому реализовать
            SceneManager.LoadScene(NEXT_SCENE_TO_LOAD);
            _playerController.enabled = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        float distance = Vector2.Distance(_player.transform.position, _nextLevelCell.transform.position);
        if (other.transform == _player && distance < _nextLevelCell.localScale.x / 2)
        {
            _playerInNextLevelCell = true;
        }
        else
        {
            _playerInNextLevelCell = false;
        }
    }

}