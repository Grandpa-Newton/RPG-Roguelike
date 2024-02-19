using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimator : MonoBehaviour
{
    private PlayerController _playerController;
    
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

    public void Initialize(PlayerController playerController)
    {
        _playerController = playerController;
        _playerController.OnPlayerMovement += Player_OnPlayerMovement;
        _playerAnimator = _playerController.GetComponent<Animator>();
    }

    private void Player_OnPlayerMovement(Vector2 movementInputVector, Vector2 worldMouseVectorPosition)
    {
        Vector2 movementMouse = new Vector2(worldMouseVectorPosition.x, worldMouseVectorPosition.y).normalized;

        _playerAnimator.SetFloat(Horizontal, movementMouse.x);
        _playerAnimator.SetFloat(Vertical, movementMouse.y);

        if (movementInputVector.x != 0 || movementInputVector.y != 0)
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

    private void OnDestroy()
    {
        _playerController.OnPlayerMovement -= Player_OnPlayerMovement;
    }
}