using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Singleton
    public static PlayerController Instance { get; private set; }

    public event Action<float> OnPlayerMovement;

    [SerializeField] private float _speed;
    [SerializeField] Rigidbody2D _rb;

    private Vector2 _moveDirection;
    private PlayerInputActions _playerInputActions;

    private NavMeshAgent _navMeshAgent;

    [Header("Mouse Movement")] [SerializeField]
    private ParticleSystem _clickEffect;

    [SerializeField] private LayerMask _clickableLayerMask;

    private float lookRotationSpeed = 8f;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is no more than one Player instance");
        }

        Instance = this;

        // Rigidbpdy
        _rb = GetComponent<Rigidbody2D>();
        _navMeshAgent = GetComponent<NavMeshAgent>();


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

        if (_moveDirection.x != 0 || _moveDirection.y != 0)
        {
            OnPlayerMovement?.Invoke(_moveDirection.x);
        }

        _rb.velocity = _moveDirection * _speed;
    }

    public Vector2 GetMoveDirection()
    {
        return _moveDirection;
    }
}