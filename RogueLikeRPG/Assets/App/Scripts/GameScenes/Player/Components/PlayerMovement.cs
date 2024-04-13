using System;
using App.Scripts.GameScenes.Player.Interfaces;
using Cinemachine;
using UnityEngine;

namespace App.Scripts.GameScenes.Player.Components
{
    public class PlayerMovement : IMove
    {
        private static PlayerMovement _instance;

        public static PlayerMovement Instance => _instance ??= new PlayerMovement();
        
        private Rigidbody2D _rigidbody2D;
        private PlayerInputActions _playerInputActions;
        private Camera _camera;
        private CinemachineVirtualCamera _virtualCamera;

        private Vector2 _moveDirection;
        private Vector2 _rollDirection;
        private Vector2 _mouseDirection;
        private Vector2 _mouseWorldPosition;
        private Vector2 _previousMousePosition;

        public event Action<Vector2, Vector2> OnPlayerMovement;
        public event Action<Vector2, Vector2> OnPlayerMouseMovement;

        private const float ScreenCenterOffset = 0.5f;
        private const float MinimalMagnitudeToMove = 0.01f;

        public float moveSpeed { get; private set; } = 5f;
        public float rollSpeed { get; private set; } = 200f;

        public bool isMoving { get; private set; }
        public bool isRolling { get; private set; }

        public void Initialize(Rigidbody2D rigidbody2D, PlayerInputActions playerInputActions, Camera camera)
        {
            _rigidbody2D = rigidbody2D;
            _playerInputActions = playerInputActions;
            _camera = camera;
            PlayerAnimator.Instance.OnPlayerRolling += MakeRoll;
        }

        public void SetVirtualCamera(CinemachineVirtualCamera virtualCamera, Transform followTarget)
        {
            _virtualCamera = virtualCamera;

            if (_virtualCamera != null)
            {
                _virtualCamera.Follow = followTarget;
                //_virtualCamera.LookAt = followTarget;
            }
            else
            {
                Debug.LogError("Не найдена виртуальная камера Cinemachine");
            }
        }

        public void Idle()
        {
            if (!isRolling)
            { 
                _rigidbody2D.velocity = _moveDirection.normalized * CalculateSpeed();
            }
            else
            {
                _rigidbody2D.velocity = _rollDirection * rollSpeed;
            }
        }

        public void Move()
        {
            if (!isRolling)
            { 
                _rigidbody2D.velocity = _moveDirection.normalized * CalculateSpeed();
            }
            else
            {
                _rigidbody2D.velocity = _rollDirection * rollSpeed;
            }
        }

        public void MakeRoll(bool isRoll)
        {
            isRolling = isRoll;

            Vector2 velocity = _rigidbody2D.velocity;
            _rollDirection = velocity.normalized;
            rollSpeed = velocity.magnitude * 1.2f;
        }

        public void GetPlayerInputs()
        {
            _moveDirection = GetMovementInputVector();
            _mouseWorldPosition = GetPointerInputVector();

            UpdatePlayerSpriteState(_moveDirection, _mouseWorldPosition);
        }

        private void UpdatePlayerSpriteState(Vector2 moveDirection, Vector2 mouseWorldPosition)
        {
            if (mouseWorldPosition != _previousMousePosition)
            {
                OnPlayerMouseMovement?.Invoke(moveDirection, mouseWorldPosition);
                _previousMousePosition = mouseWorldPosition;
            }

            if (moveDirection.magnitude < MinimalMagnitudeToMove || moveDirection != Vector2.zero)
            {
                OnPlayerMovement?.Invoke(moveDirection, mouseWorldPosition);
            }
        }

        private Vector2 GetMovementInputVector()
        {
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

        public void Dispose()
        {
            PlayerAnimator.Instance.OnPlayerRolling -= MakeRoll;
        }
    }
}