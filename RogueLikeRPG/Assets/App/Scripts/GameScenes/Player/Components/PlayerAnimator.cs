using System;
using UnityEngine;

namespace App.Scripts.GameScenes.Player.Components
{
    public class PlayerAnimator
    {
        private static PlayerAnimator _instance;
        public static PlayerAnimator Instance => _instance ??= new PlayerAnimator();

        private Animator _animator;
        
        private bool _isWalking;
        private bool _isRolling;
        
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int IsRolling = Animator.StringToHash("IsRolling");
        private static readonly int IsDead = Animator.StringToHash("IsDead");

        public event Action<bool> OnPlayerRolling;
        
        public void Initialize( Animator animator)
        {
            _animator = animator;
            PlayerMovement.Instance.OnPlayerMovement += Player_OnPlayerMovement;
            PlayerMovement.Instance.OnPlayerMouseMovement += Player_OnPlayerMouseMovement;
            PlayerHealth.Instance.OnPlayerDie += Player_OnPlayerDie;
        }

        private void Player_OnPlayerDie()
        {
            _animator.SetTrigger(IsDead);
        }

        private void Player_OnPlayerMovement(Vector2 movementInputVector, Vector2 worldMouseVectorPosition)
        {
            if (Input.GetKeyDown(KeyCode.Space) && _isWalking)
            {
                _isRolling = true;
                PlayerWeaponSwitcher.Instance.WeaponAndHandsVisibility(!_isRolling);
                _animator.SetBool(IsRolling, _isRolling);
                _animator.SetFloat(Horizontal, movementInputVector.x);
                _animator.SetFloat(Vertical, movementInputVector.y);
                OnPlayerRolling?.Invoke(_isRolling);
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

        public void RollEndAction()
        {
            var animator = _animator.GetCurrentAnimatorStateInfo(0);
            if (!animator.IsName("Roll") || !(animator.normalizedTime >= 1.0)) return;
        
            _isRolling = false;
            _animator.SetBool(IsRolling, _isRolling);
            PlayerWeaponSwitcher.Instance.WeaponAndHandsVisibility(!_isRolling);
            OnPlayerRolling?.Invoke(_isRolling);
        
        }

        public void Dispose()
        {
            PlayerMovement.Instance.OnPlayerMovement -= Player_OnPlayerMovement;
            PlayerMovement.Instance.OnPlayerMouseMovement -= Player_OnPlayerMouseMovement;
        }
    }
}