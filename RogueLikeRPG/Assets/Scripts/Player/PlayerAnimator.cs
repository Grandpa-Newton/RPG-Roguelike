using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimator : MonoBehaviour
{
    private Animator _playerAnimator;
    private SpriteRenderer _spriteRenderer;

    private bool _isWalking = false;
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _playerAnimator = PlayerController.Instance.GetComponent<Animator>();
        PlayerController.Instance.OnPlayerMovement += Player_OnPlayerMovement;
    }

    private void Player_OnPlayerMovement()
    {
        float xDir = PlayerController.Instance.GetMoveDirection().x;
        float yDir = PlayerController.Instance.GetMoveDirection().y;
    
        float xMouse = PlayerController.Instance.GetMouseDirection().x;
        float yMouse = PlayerController.Instance.GetMouseDirection().y;
    
        Vector2 movementMouse = new Vector2(xMouse, yMouse).normalized;

        // Устанавливаем направление персонажа в зависимости от положения мыши
        _playerAnimator.SetFloat(Horizontal, movementMouse.x);
        _playerAnimator.SetFloat(Vertical, movementMouse.y);

        if (xDir != 0 || yDir != 0)
        {
            if (!_isWalking)
            {
                _isWalking = true;
                _playerAnimator.SetBool(IsMoving, _isWalking);
            }
        }
        else
        {
            if (_isWalking)
            {
                _isWalking = false;
                _playerAnimator.SetBool(IsMoving, _isWalking);
            }
        }
    }


}