using System;
using System.Collections;
using System.Collections.Generic;
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
            PlayerInputActions = new PlayerInputActions();
            PlayerInputActions.Map.Enable();
            PlayerInputActions.Map.Interact.Disable();
            // PlayerInputActions.Map.SelectCell.performed += SelectCell_performed;
            
        }
        else
        {
            Debug.LogError("There is cannot be more than one MapInputManager Instance");
        }
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
