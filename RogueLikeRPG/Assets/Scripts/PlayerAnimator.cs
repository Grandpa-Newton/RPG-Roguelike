using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator playerAnimator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        PlayerController.Instance.OnHorizontalMovement += OnFlipSprite;
    }

    private void OnFlipSprite(float horizontalMovement)
    {
        if (horizontalMovement < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    private void Update()
    {
        MoveAnimate();
    }

    private void MoveAnimate()
    {
        if (PlayerController.Instance.GetMoveDirection().x != 0 || PlayerController.Instance.GetMoveDirection().y != 0)
        {
            playerAnimator.SetBool("IsMoving", true);
        }
        else
        {
            playerAnimator.SetBool("IsMoving", false);
        }
    }
}