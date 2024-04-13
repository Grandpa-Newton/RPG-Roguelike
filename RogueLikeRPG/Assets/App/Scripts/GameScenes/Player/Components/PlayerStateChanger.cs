using System;
using UnityEngine;

namespace App.Scripts.GameScenes.Player.Components
{
    public class PlayerStateChanger
    {
        private static PlayerStateChanger _instance;
        public static PlayerStateChanger Instance => _instance ??= new PlayerStateChanger();
        private enum PlayerState
        {
            Idle,
            Move,
            Roll,
        }

        private bool _isRolling;
    
        private PlayerState _playerState;
        private PlayerInputActions _playerInputActions;

        public void Initialize(PlayerInputActions playerInputActions)
        {
            _playerInputActions = playerInputActions;
            _playerState = PlayerState.Idle;
            PlayerAnimator.Instance.OnPlayerRolling += GetPlayerRollState;
            PlayerController.Instance.OnPlayerUpdatePlayerState += UpdatePlayerState;
        }
    
        private void GetPlayerRollState(bool isRolling)
        {
            _isRolling = isRolling;
        }
    
        public void ChangePlayerState()
        {
            switch (_playerState)
            {
                case PlayerState.Idle:
                    PlayerMovement.Instance.Idle();
                    break;
                case PlayerState.Move:
                    PlayerMovement.Instance.Move();
                    break;
                case PlayerState.Roll:
                    PlayerMovement.Instance.MakeRoll(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UpdatePlayerState()
        {
            Vector2 playerMovementVector = _playerInputActions.Player.Movement.ReadValue<Vector2>();

            PlayerMovement.Instance.GetPlayerInputs();

            PlayerAnimator.Instance.RollEndAction();


            if (_isRolling)
            {
                _playerState = PlayerState.Roll;
            }
            else if (playerMovementVector != Vector2.zero)
            {
                _playerState = PlayerState.Move;
            }
            else
            {
                _playerState = PlayerState.Idle;
            }
        }

        public void Dispose()
        {
            PlayerAnimator.Instance.OnPlayerRolling -= GetPlayerRollState;
            PlayerController.Instance.OnPlayerUpdatePlayerState -= UpdatePlayerState;
        }
    }
}

