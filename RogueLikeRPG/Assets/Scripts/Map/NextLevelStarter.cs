using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Cinemachine;
using Unity.VisualScripting;

public class NextLevelStarter : MonoBehaviour
{
    public event Action OnLevelEnd;
    
    public Transform Player;
    [SerializeField] private GameObject panel;
    private PlayerController _playerController;
    [SerializeField] private StartShowDeLoadPanel _startShowDeLoadPanel;
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
        Debug.Log("123");
        MapPlayerController.Instance.OnInteractCell -= Instance_OnInteractCell; // ???
        _startShowDeLoadPanel.ExitScene();
        SceneManager.LoadScene(cell.CellData.SceneToLoad); // мб лучше сделать sceneToLoad не пабликом, а вызывать как-то событием / методом
        // (теперь через SO сделано - не уверен)
        _playerController.enabled = true;
        /* LoadTransition.Instance.gameObject.SetActive(true);
        LoadTransition.Instance.nextLevelStarter = this; */

        //NextLevelManager.Instance.LoadTransition.gameObject.SetActive(true);
        //NextLevelManager.Instance.LoadTransition.NextLevelStarter = this;
        
        
    }

    public void StartNextLevel()
    {
        
    }
}