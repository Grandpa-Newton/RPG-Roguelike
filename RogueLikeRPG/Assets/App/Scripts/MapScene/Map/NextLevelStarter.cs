using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using App.Scripts.AllScenes.UI;
using App.Scripts.GameScenes.Player;
using App.Scripts.MapScene.Cells;
using App.Scripts.MixedScenes.Player.Control;
using UnityEngine.SceneManagement;
using Cinemachine;
using Unity.VisualScripting;

public class NextLevelStarter : MonoBehaviour
{
    public Transform Player;
    [SerializeField] private GameObject panel;
    private PlayerController _player;
    [SerializeField] private StartShowDeLoadPanel _startShowDeLoadPanel;
    private void Awake()
    {
        _player = Player.GetComponent<PlayerController>();
    }

    public void InCurrentCell() // когда игрок дошёл до новой клетки (мб поменять название)
    {
        // MapPlayerController.Instance.OnInteractCell += Instance_OnInteractCell;
        // MapInputManager.Instance.OnInteractPressed += Instance_OnInteractPressed;
        BaseCell cell = gameObject.GetComponent<BaseCell>();
        MapInputManager.Instance.TurnOnSelectCell(cell.CellData.SceneToLoad);
    }

    private void Instance_OnInteractPressed()
    {

    }

    IEnumerator WaitAMinute()
    {
        yield return new WaitForSeconds(1f);
    }
    private void Instance_OnInteractCell()
    {
        BaseCell cell = gameObject.GetComponent<BaseCell>();
        // InputManager.Instance.OnInteractPressed
        Debug.Log("123");
        MapPlayerController.Instance.OnInteractCell -= Instance_OnInteractCell; // ???
        // MapInputManager.Instance.OnInteractPressed.Invoke(cell.CellData.SceneToLoad);
        //StartCoroutine(WaitAMinute());
        //SceneManager.LoadScene(cell.CellData.SceneToLoad); // мб лучше сделать sceneToLoad не пабликом, а вызывать как-то событием / методом
        // (теперь через SO сделано - не уверен)
        _player.enabled = true;
        /* LoadTransition.Instance.gameObject.SetActive(true);
        LoadTransition.Instance.nextLevelStarter = this; */

        //NextLevelManager.Instance.LoadTransition.gameObject.SetActive(true);
        //NextLevelManager.Instance.LoadTransition.NextLevelStarter = this;
        
        
    }

    public void StartNextLevel()
    {
        
    }
}