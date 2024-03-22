using App.Scripts.MixedScenes.Player.Control;
using UnityEngine;

namespace App.Scripts.MixedScenes.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        private PlayerController _playerController;

        public delegate void RollingAction(bool isRolling);

        public static event RollingAction OnPlayerRolling;

        private Animator _playerAnimator;

        private bool _isWalking;
        private bool _isRolling;
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int IsRolling = Animator.StringToHash("IsRolling");

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
            _playerAnimator = GetComponent<Animator>();
            _playerController.OnPlayerMovement += Player_OnPlayerMovement;
            _playerController.OnPlayerMouseMovement += Player_OnPlayerMouseMovement;
        }

        private void Player_OnPlayerMouseMovement(Vector2 movementInputVector, Vector2 worldMouseVectorPosition)
        {
            if (!_isRolling)
            {
                Vector2 movementMouse = new Vector2(worldMouseVectorPosition.x, worldMouseVectorPosition.y).normalized;

                _playerAnimator.SetFloat(Horizontal, movementMouse.x);
                _playerAnimator.SetFloat(Vertical, movementMouse.y);
            }
        }

        private void Player_OnPlayerMovement(Vector2 movementInputVector, Vector2 worldMouseVectorPosition)
        {
            if (Input.GetKeyDown(KeyCode.Space) && _isWalking)
            {
                _playerAnimator.SetBool(IsRolling, true);
                _isRolling = true;
                OnPlayerRolling?.Invoke(_isRolling);

                _playerAnimator.SetFloat(Horizontal, movementInputVector.x);
                _playerAnimator.SetFloat(Vertical, movementInputVector.y);
            }

            if (!_isRolling)
            {
                if (movementInputVector.x != 0 || movementInputVector.y != 0)
                {
                    if (!_isWalking)
                    {
                        _isWalking = true;
                        _playerAnimator.SetBool(IsMoving, _isWalking);
                        Debug.Log("Is Walking!");
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

        private void Update()
        {
            if (_playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Roll") &&
                _playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0)
            {
                _playerAnimator.SetBool(IsRolling, false);
                _isRolling = false;
                OnPlayerRolling?.Invoke(_isRolling);
            }
        }

        private void OnDestroy()
        {
            _playerController.OnPlayerMovement -= Player_OnPlayerMovement;
        }
    }
}