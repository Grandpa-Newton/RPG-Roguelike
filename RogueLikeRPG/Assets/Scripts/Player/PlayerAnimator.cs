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
        float xMouse = PlayerController.Instance.GetMouseDirection().x;
        float yMouse = PlayerController.Instance.GetMouseDirection().y;
        
        Vector2 movementMouse = new Vector2(xMouse, yMouse).normalized;

        if (xMouse != 0 || yMouse != 0)
        {
            _playerAnimator.SetFloat("Horizontal", movementMouse.x);
            _playerAnimator.SetFloat("Vertical", movementMouse.y);
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