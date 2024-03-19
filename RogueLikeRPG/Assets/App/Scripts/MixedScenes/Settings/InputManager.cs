using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    
    public PlayerInputActions PlayerInputActions { get; private set; }
    

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            PlayerInputActions = new PlayerInputActions();
            PlayerInputActions.Player.Enable();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}