using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    public GameObject gfxObject;

    public event Action OnPlayerMovement;

    [SerializeField] private float _speed;
    [SerializeField] Rigidbody2D _rb;

    private Vector2 _moveDirection;
    private PlayerInputActions _playerInputActions;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is no more than one Player instance");
        }

        Instance = this;

        // Rigidbpdy
        _rb = GetComponent<Rigidbody2D>();

        // Player Input Actions
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
    }

    public void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 inputVector = _playerInputActions.Player.Movement.ReadValue<Vector2>();
        _moveDirection = new Vector2(inputVector.x, inputVector.y).normalized;

        OnPlayerMovement?.Invoke();

        _rb.velocity = _moveDirection * _speed;
    }

    public Vector2 GetMoveDirection()
    {
        return _moveDirection;
    }
}