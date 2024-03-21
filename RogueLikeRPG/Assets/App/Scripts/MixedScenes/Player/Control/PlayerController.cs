using System;
using System.Collections;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

namespace App.Scripts.MixedScenes.Player.Control
{
    public class PlayerController : MonoBehaviour
    { 
        private Rigidbody2D _rigidbody2D;
        private PlayerInputActions _playerInputActions;
        private Camera _camera;

        [SerializeField] private float speed = 5;
        //private float accelerationMaxTime = 1.25f;

        //[SerializeField] private AnimationCurve accelerationCurve;

        private bool _isMoving;
        private float _timeButtonHeld;
        private const float ScreenCenterOffset = 0.5f;

        //Events
        public event Action<Vector2, Vector2> OnPlayerMovement;
        public event Action<Vector2, Vector2> OnPlayerMouseMovement;

        // Vectors
        private Vector2 _inputMouseVector;
        private Vector2 _previousMousePos;
        private Vector2 _worldMousePos;
        private Vector2 _inputVector;

        public float dashDistance = 2000; // Расстояние рывка
        public float dashDuration = 0.2f; 
    
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
        }

        private void Start()
        {
            _playerInputActions = InputManager.Instance.PlayerInputActions;

            CinemachineVirtualCamera vcam = FindObjectOfType<CinemachineVirtualCamera>();
        
            if (vcam != null)
            {
                vcam.Follow = transform;
                vcam.LookAt = transform;
            }
            else
            {
                Debug.LogError("Не найдена виртуальная камера Cinemachine");
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                Vector2 dashDirection = _rigidbody2D.velocity.normalized; // Направление рывка - это нормализованный вектор скорости игрока
                Vector2 dashTarget = (Vector2)transform.position + dashDirection * (dashDistance * Time.deltaTime);
                transform.DOMove(dashTarget, dashDuration);
            }
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
            if (_inputVector.magnitude < 0.01f || _inputVector != Vector2.zero)
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

            if (_isMoving)
                _timeButtonHeld += Time.deltaTime;
            else
                _timeButtonHeld = 0;


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


        private void SetVelocity()
        {
            _rigidbody2D.velocity = _inputVector.normalized * CalculateSpeed();
        }

        private void EnablePlayerComponents()
        {
            //Camera
            _camera = Camera.main;
            // Rigid body
            _rigidbody2D = GetComponent<Rigidbody2D>();
            // Player Input Actions
        }
    }
}

// [SerializeField] private VisualEffect vfxRenderer;
/*private void VFXRenderer()
{
    vfxRenderer.SetVector3("ColliderPos", transform.position);
}*/