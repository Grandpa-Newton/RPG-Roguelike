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

    private Vector2 moveDirection;
    private PlayerInputActions playerInputActions;
    
    private float LastHorizontalVector;
    private float LastVerticalVector;
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
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        
    }

    public void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        moveDirection = new Vector2(inputVector.x, inputVector.y).normalized;
        
        if(moveDirection.x != 0)
        {
            LastHorizontalVector = moveDirection.x;
            OnHorizontalMovement?.Invoke(moveDirection.x);
        }
        if(moveDirection.y != 0)
        {
            LastVerticalVector = moveDirection.y;
        }    

        _rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * _speed;
    }

    public Vector2 GetMoveDirection()
    {
        return moveDirection;
    }
}
