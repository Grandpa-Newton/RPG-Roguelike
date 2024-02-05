 using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    public GameObject gfxObject;
    [SerializeField] private GameObject crossHair;


    public event Action OnPlayerMovement;

    [SerializeField] private float _speed;
    [SerializeField] Rigidbody2D _rb;

    private Vector2 _moveDirection;
    private PlayerInputActions _playerInputActions;

    private bool _isWalking = false;
    private bool _canMove = true;

    private Vector2 inputVector;
    private Vector2 inputMouseVector;
    private Vector2 mousePosition;
    private Vector2 mousePos;
    [SerializeField] private GameObject sword;

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

    private void Start()
    {
        Instantiate(crossHair,transform.position,Quaternion.identity);

    }

    private void Update()
    {
        RotateSwordToMouseDirection();
    }
    
    private void RotateSwordToMouseDirection()
    {
        // Получаем позицию мыши в мировых координатах
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Вычисляем направление от игрока к мыши
        Vector2 direction = (mousePosition).normalized;

        // Поворачиваем дочерний объект (GFX) в направлении мыши
        gfxObject.transform.up = direction;
    }
    
    public void FixedUpdate()
    {
        Move();
        
    }

    private Vector2 worldMousePos;
    private void Move()
    {
        // Movement
        inputVector = _playerInputActions.Player.Movement.ReadValue<Vector2>();
        // Mouse/Stick
        inputMouseVector = _playerInputActions.Player.PointerPosition.ReadValue<Vector2>();
        
        Debug.Log(inputMouseVector);
        
         worldMousePos = 
            (Camera.main.ScreenToViewportPoint(inputMouseVector) - new Vector3(0.5f, 0.5f, 0f)) * 2;
        Debug.Log(worldMousePos);   
        

        if ((inputVector.x == 0 && inputVector.y == 0))
        {
            _isWalking = false;
        }
        else if (inputVector.x != 0 || inputVector.y != 0)
        {
            _isWalking = true;
        }

        OnPlayerMovement?.Invoke();

        _rb.velocity = new Vector2(inputVector.x, inputVector.y).normalized * _speed;
    }





    public Vector2 GetMoveDirection()
    {
        return inputVector;
    }
    public Vector2 GetMouseDirection()
    {
        return worldMousePos;
    }
}