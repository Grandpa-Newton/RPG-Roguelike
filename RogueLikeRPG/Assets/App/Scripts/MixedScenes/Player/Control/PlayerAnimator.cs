using System.Collections;
using System.Collections.Generic;
using App.Scripts.MixedScenes.Weapon;
using UnityEngine;

public class PlayerAnimator
{
    private PlayerController _playerController;
    private PlayerMovement _playerMovement;

    private Animator _animator;

    public delegate void RollingAction(bool isRolling);

    public static event RollingAction OnPlayerRolling;

    private bool _isWalking;
    private bool _isRolling;
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int IsRolling = Animator.StringToHash("IsRolling");

    public PlayerAnimator(PlayerController playerController, Animator animator, PlayerMovement playerMovement)
    {
        _playerController = playerController;
        _animator = animator;
        _playerMovement = playerMovement;
        _playerMovement.OnPlayerMovement += Player_OnPlayerMovement;
        _playerMovement.OnPlayerMouseMovement += Player_OnPlayerMouseMovement;
    }


    private void Player_OnPlayerMovement(Vector2 movementInputVector, Vector2 worldMouseVectorPosition)
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isWalking)
        {
            SwitchWeaponBetweenRangeAndMelee.Instance.WeaponAndHandsDisable();
            _isRolling = true;
            _animator.SetBool(IsRolling, _isRolling);
            OnPlayerRolling?.Invoke(_isRolling);

            _animator.SetFloat(Horizontal, movementInputVector.x);
            _animator.SetFloat(Vertical, movementInputVector.y);
        }

        if (!_isRolling)
        {
            if (movementInputVector.x != 0 || movementInputVector.y != 0)
            {
                if (_isWalking) return;
                
                _isWalking = true;
                _animator.SetBool(IsMoving, _isWalking);
            }
            else
            {
                if (!_isWalking) return;
                
                _isWalking = false;
                _animator.SetBool(IsMoving, _isWalking);
            }
        }
    }

    private void Player_OnPlayerMouseMovement(Vector2 movementInputVector, Vector2 worldMouseVectorPosition)
    {
        if (_isRolling) return;
        
        Vector2 movementMouse = new Vector2(worldMouseVectorPosition.x, worldMouseVectorPosition.y).normalized;

        _animator.SetFloat(Horizontal, movementMouse.x);
        _animator.SetFloat(Vertical, movementMouse.y);
    }

    public void CheckRollEnd()
    {
        var animator = _animator.GetCurrentAnimatorStateInfo(0);
        if (!animator.IsName("Roll") || !(animator.normalizedTime >= 1.0)) return;
        
        _isRolling = false;
        _animator.SetBool(IsRolling, _isRolling);
        SwitchWeaponBetweenRangeAndMelee.Instance.WeaponAndHandsEnable();
        OnPlayerRolling?.Invoke(_isRolling);
        
    }

    public void Dispose()
    {
        _playerMovement.OnPlayerMovement -= Player_OnPlayerMovement;
        _playerMovement.OnPlayerMouseMovement -= Player_OnPlayerMouseMovement;
    }
}