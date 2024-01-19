using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator playerAnimator;
    private InputSystem inputSystem;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        inputSystem = GetComponent<InputSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(inputSystem.MoveDir.x != 0 || inputSystem.MoveDir.y != 0)
        {
            playerAnimator.SetBool("IsMoving", true);

            CheckSpriteDirection();
        }
        else
        {
            playerAnimator.SetBool("IsMoving", false);
        }
    }

    private void CheckSpriteDirection()
    {
        if(inputSystem.LastHorizontalVector < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
