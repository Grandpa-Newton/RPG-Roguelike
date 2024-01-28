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
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _playerAnimator = PlayerController.Instance.gfxObject.GetComponent<Animator>();
        PlayerController.Instance.OnPlayerMovement += Player_OnPlayerMovement;
    }

    private void Player_OnPlayerMovement()
    {
        float xDir = PlayerController.Instance.GetMoveDirection().x;
        float yDir = PlayerController.Instance.GetMoveDirection().y;
        
        Vector2 movement = new Vector2(xDir, yDir);

        _playerAnimator.SetFloat("Horizontal", movement.x);
        _playerAnimator.SetFloat("Vertical", movement.y);
    }
}