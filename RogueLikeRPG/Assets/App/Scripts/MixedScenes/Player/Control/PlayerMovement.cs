using System;
using System.Collections;
using System.Collections.Generic;
using App.Scripts.MixedScenes.Player.Interface;
using UnityEngine;

public class PlayerMovement : IMove
{
    private Rigidbody2D _rigidbody2D;
    private PlayerInputActions _playerInputActions;
    private Camera _camera;
    
    private Vector2 _moveDirection;
    private Vector2 _rollDirection;
    private Vector2 _mouseDirection;
    private Vector2 _mouseWorldPosition;
    private Vector2 _previousMousePos;
    
    public event Action<Vector2, Vector2> OnPlayerMovement;
    public event Action<Vector2, Vector2> OnPlayerMouseMovement;
    
    private const float ScreenCenterOffset = 0.5f;
    private const float MinimalMagnitudeToMove = 0.01f;
    
    public float moveSpeed { get; private set; }
    public float rollSpeed { get; private set; }

    public bool isMoving { get; private set; }
    public bool isRolling { get; private set; }
    
    public PlayerMovement(Rigidbody2D rigidbody2D, PlayerInputActions playerInputActions, Camera camera)
    {
        _rigidbody2D = rigidbody2D;
        _playerInputActions = playerInputActions;
        _camera = camera;
    }

    public void Move(Vector2 moveDirection)
    {
        if (!isRolling)
        {
            _rigidbody2D.velocity = moveDirection.normalized * CalculateSpeed();
        }
        else
        {
            _rigidbody2D.velocity = _rollDirection * rollSpeed;
        }
    }

    public void Roll(Vector2 rollDirection, bool isRoll)
    {
        _rollDirection = rollDirection;
        isRolling = isRoll;
        if (isRolling)
        {
            _rigidbody2D.velocity = _rollDirection * rollSpeed;
        }
    }
    
    public void UpdateInputsInformationAndInvokeEvent()
    {
        _moveDirection = GetMovementInputVector(_playerInputActions);
        _mouseWorldPosition = GetPointerInputVector();

        if (_mouseWorldPosition != _previousMousePos)
        {
            OnPlayerMouseMovement?.Invoke(_moveDirection, _mouseWorldPosition);
            _previousMousePos = _mouseWorldPosition;
        }
        if (_moveDirection.magnitude < MinimalMagnitudeToMove || _moveDirection != Vector2.zero)
        {
            OnPlayerMovement?.Invoke(_moveDirection, _mouseWorldPosition);
        }
        
    }
    
    private Vector2 GetMovementInputVector(PlayerInputActions playerInputActions)
    {
        _playerInputActions = playerInputActions;
        Vector2 movementVector = _playerInputActions.Player.Movement.ReadValue<Vector2>();
        
        isMoving = movementVector.magnitude > 0;
        
        return movementVector;
    }  
    private Vector2 GetPointerInputVector()
    {
        _mouseDirection = _playerInputActions.Player.PointerPosition.ReadValue<Vector2>();
        _mouseWorldPosition = (_camera.ScreenToViewportPoint(_mouseDirection) -
                               new Vector3(ScreenCenterOffset, ScreenCenterOffset, 0f)) * 2;

        return _mouseWorldPosition;
    }
    private float CalculateSpeed()
    {
        if (isMoving)
        {
            return moveSpeed;
        }

        return 0;
    }
}
