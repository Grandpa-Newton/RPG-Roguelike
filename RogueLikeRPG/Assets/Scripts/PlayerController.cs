using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Singleton
    public static PlayerController Instance { get; private set; }

    public event Action<float> OnHorizontalMovement;
    
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

    public void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 inputVector = _playerInputActions.Player.Movement.ReadValue<Vector2>();
        _moveDirection = new Vector2(inputVector.x, inputVector.y).normalized;
        
        if(_moveDirection.x != 0)
        {
            OnHorizontalMovement?.Invoke(_moveDirection.x);
        }
        if(_moveDirection.y != 0)
        {
            // Vertical Movement
        }    

        _rb.velocity = new Vector2(_moveDirection.x, _moveDirection.y) * _speed;
    }

    public Vector2 GetMoveDirection()
    {
        return _moveDirection;
    }
}
