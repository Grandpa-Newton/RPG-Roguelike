using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _playerAnimator;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        PlayerController.Instance.OnHorizontalMovement += OnFlipSprite;
    }

    private void OnFlipSprite(float horizontalMovement)
    {
        if (horizontalMovement < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
    }

    private void Update()
    {
        //MoveAnimate();
    }

    private void MoveAnimate()
    {
        if (PlayerController.Instance.GetMoveDirection().x != 0 || PlayerController.Instance.GetMoveDirection().y != 0)
        {
            _playerAnimator.SetBool("IsMoving", true);
        }
        else
        {
            _playerAnimator.SetBool("IsMoving", false);
        }
    }
}