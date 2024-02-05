using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimator : MonoBehaviour
{
    private Animator _playerAnimator;
    private SpriteRenderer _spriteRenderer;

    private bool isWalking = false;
    
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
    
        Vector2 direction = new Vector2(xDir, yDir).normalized;
        Vector2 movementMouse = new Vector2(xMouse, yMouse).normalized;

        // Устанавливаем направление персонажа в зависимости от положения мыши
        _playerAnimator.SetFloat("Horizontal", movementMouse.x);
        _playerAnimator.SetFloat("Vertical", movementMouse.y);

        if (xDir != 0 || yDir != 0)
        {
            if (!isWalking)
            {
                isWalking = true;
                _playerAnimator.SetBool("IsMoving", isWalking);
            }
        }
        else
        {
            if (isWalking)
            {
                isWalking = false;
                _playerAnimator.SetBool("IsMoving", isWalking);
            }
        }
    }


}