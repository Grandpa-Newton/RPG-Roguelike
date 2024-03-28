using System;
using App.Scripts.MixedScenes.Weapon;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

// ReSharper disable Unity.InefficientPropertyAccess

namespace App.Scripts.MixedScenes.Player.Control
{
    public class Player : MonoBehaviour
    {
        private CinemachineVirtualCamera _virtualCamera;
        private Health _health;
        
        public PlayerHealth playerHealth { get; private set; }
        [FormerlySerializedAs("floatValue")] [SerializeField] private CharacteristicValueSO characteristicValue;
        
        private Rigidbody2D _rigidbody2D;
        private PlayerInputActions _playerInputActions;
        private Camera _camera;

        [SerializeField] private float speed = 5;

        private bool _isMoving;
        private bool _isRolling;
        private float _rollSpeed;
        
        private const float ScreenCenterOffset = 0.5f;
        private const float MinimalMagnitudeToMove = 0.01f;

        //Events
        public event Action<Vector2, Vector2> OnPlayerMovement;
        public event Action<Vector2, Vector2> OnPlayerMouseMovement;

        // Vectors
        private Vector2 _inputMouseVector;
        private Vector2 _previousMousePos;
        private Vector2 _worldMousePos;
        private Vector2 _inputVector;
        private Vector2 _rollDirection;
      //  [SerializeField] private FloatValueSO floatValue;
    
        void Awake() {
            if (FindObjectsOfType(GetType()).Length > 1)
            {
                Debug.LogError("There cannot be more than one Player Controller Instance");
                Destroy(gameObject);
            }
            else
            {
                EnablePlayerComponents();
            }
            
            playerHealth = new PlayerHealth(characteristicValue);
            
            _health = GetComponent<Health>();
            PlayerAnimatorOld.OnPlayerRolling += SetRollingState;
            _health.OnHealthReduce += OnPlayerHealthReduce;
        }

        private void Start()
        {
            _playerInputActions = InputManager.Instance.PlayerInputActions;
            
            _virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        
            if (_virtualCamera != null)
            {
                _virtualCamera.Follow = transform;
                _virtualCamera.LookAt = transform;
            }
            else
            {
                Debug.LogError("Не найдена виртуальная камера Cinemachine");
            }
        }

        private void Update()
        {
            UpdateInputsInformationAndInvokeEvent();
        }

        private void UpdateInputsInformationAndInvokeEvent()
        {
            _inputVector = GetMovementInputVector();
            _worldMousePos = GetPointerInputVector();

            if (_worldMousePos != _previousMousePos)
            {
                OnPlayerMouseMovement?.Invoke(_inputVector, _worldMousePos);
                _previousMousePos = _worldMousePos;
            }
            if (_inputVector.magnitude < MinimalMagnitudeToMove || _inputVector != Vector2.zero)
            {
                OnPlayerMovement?.Invoke(_inputVector, _worldMousePos);
            }
        
        }

        private void FixedUpdate()
        {
            SetVelocity();
        }

        private Vector2 GetPointerInputVector()
        {
            _inputMouseVector = _playerInputActions.Player.PointerPosition.ReadValue<Vector2>();
            _worldMousePos = (_camera.ScreenToViewportPoint(_inputMouseVector) -
                              new Vector3(ScreenCenterOffset, ScreenCenterOffset, 0f)) * 2;

            return _worldMousePos;
        }

        private Vector2 GetMovementInputVector()
        {
            Vector2 input = _playerInputActions.Player.Movement.ReadValue<Vector2>();
            _isMoving = input.magnitude > 0;
            return input;
        }

        private float CalculateSpeed()
        {
            if (_isMoving)
            {
                return speed;
            }

            return 0;
        }

        private void SetRollingState(bool isRolling)
        {
            _isRolling = isRolling;
            if (_isRolling)
            {
                
                Vector2 velocity = _rigidbody2D.velocity;
                _rollDirection = velocity.normalized;
                _rollSpeed = velocity.magnitude * 1.2f;
            }
        }

        private void SetVelocity()
        {
            if (!_isRolling)
            {
                
                _rigidbody2D.velocity = _inputVector.normalized * CalculateSpeed();
            }
            else
            {
                _rigidbody2D.velocity = _rollDirection * _rollSpeed;
            }
        }
        private void EnablePlayerComponents()
        {
            _camera = Camera.main;
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnPlayerHealthReduce()
        {
            Debug.Log("Shook!");
        }

        private void OnDestroy()
        {
            _health.OnHealthReduce -= OnPlayerHealthReduce;
            PlayerAnimatorOld.OnPlayerRolling -= SetRollingState;
        }
    }
}
