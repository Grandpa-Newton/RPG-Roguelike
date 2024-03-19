using System;
using System.Collections;
using System.Collections.Generic;
using App.Scripts.MixedScenes.Player.Control;
using UnityEngine;

public class MapInputManager : MonoBehaviour
{
    public event Action<string> OnInteractPressed;
    public static MapInputManager Instance { get; private set; }

    public PlayerInputActions PlayerInputActions { get; private set; }

    private string _sceneToLoad;

    private void Awake()
    {
        if(!Instance)
        {

            Instance = this;
        }
        else
        {
            Debug.LogError("There is cannot be more than one MapInputManager Instance");
        }


        PlayerInputActions = new PlayerInputActions();
        PlayerInputActions.Map.Enable();
        PlayerInputActions.Map.Interact.Disable();
    }

    private void Start()
    {

        // PlayerInputActions.Map.SelectCell.performed += SelectCell_performed;
        PlayerInputActions.Map.GetCells.performed += GetCells_performed;
        PlayerInputActions.Map.ConfirmCell.performed += ConfirmCell_performed;
        PlayerInputActions.Map.ConfirmCell.Disable();

        MapPlayerController.Instance.OnDeselectCells += Instance_OnDeselectCells;
        MapPlayerController.Instance.OnStartingSelectCell += Instance_OnSelectingCell;
        MapPlayerController.Instance.OnSelectingCell += Instance_OnSelectingCell1;
    }

    private void Instance_OnSelectingCell1()
    {
        PlayerInputActions.Map.ConfirmCell.Disable();

        PlayerInputActions.Map.GetCells.Disable();
    }

    private void Instance_OnSelectingCell()
    {
        PlayerInputActions.Map.ConfirmCell.Enable();
    }

    private void Instance_OnDeselectCells()
    {
        PlayerInputActions.Map.ConfirmCell.Disable();
    }

    private void ConfirmCell_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        MapPlayerController.Instance.ConfirmCell_performed();
    }

    private void GetCells_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        MapPlayerController.Instance.GetCells_performed();
    }

    public void TurnOnSelectCell(string sceneToLoad) // фегня какая-то
    {
        _sceneToLoad = sceneToLoad; 
        PlayerInputActions.Map.Interact.Enable();
        PlayerInputActions.Map.Interact.performed += SelectCell_performed;
        
    }

    private void SelectCell_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractPressed?.Invoke(_sceneToLoad); // мб тут проверка на empty
    }
}
