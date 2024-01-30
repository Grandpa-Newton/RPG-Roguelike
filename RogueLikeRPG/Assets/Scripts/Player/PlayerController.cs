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

    public event Action OnPlayerMovement;
    
    public GameObject gfxObject;
    [SerializeField] private float _speed;
    [SerializeField] Rigidbody2D _rb;

    private Vector2 _moveDirection;
    private PlayerInputActions _playerInputActions;

    private NavMeshAgent _navMeshAgent;

    [Header("Key")] 
    [SerializeField] private GameObject keyIcon;
    [SerializeField] private GameObject wallEffect;

    private bool keyButtonPushed = false;

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
            OnPlayerMovement?.Invoke();
        }

        _rb.velocity = _moveDirection * _speed;
    }

    public Vector2 GetMoveDirection()
    {
        return _moveDirection;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Key"))
        {
            keyIcon.SetActive(true);
            Destroy(other.gameObject);
        }
    }

    public void OnKeyButtonDown()
    {
        keyButtonPushed = !keyButtonPushed;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Door") && keyButtonPushed && keyIcon.activeInHierarchy)
        {
            Instantiate(wallEffect,other.transform.position, Quaternion.identity);
            keyIcon.SetActive(false);
            other.gameObject.SetActive(false);
            keyButtonPushed = false;
        }
    }
}