using System;
using App.Scripts.MixedScenes.Player.Interface;
using UnityEngine;

namespace App.Scripts.MixedScenes.Player
{
    public class PlayerCharacteristics : IHealth, IMove
    {
        private Rigidbody2D _rigidbody2D;
        private Vector2 _moveDirection;
        private Vector2 _rollDirection;
        public int maxHealth { get; private set; }
        public CharacteristicValueSO playerHealth { get; private set; }

        public float moveSpeed { get; private set; }
        public float rollSpeed { get; private set; }

        public bool isMoving { get; private set; }
        public bool isRolling { get; private set; }

        public event Action OnPlayerHealthReduce;
        public event Action OnPlayerIncreaseHealth;
        public event Action OnPlayerDie;

        public PlayerCharacteristics(Rigidbody2D rigidbody2D)
        {
            _rigidbody2D = rigidbody2D;
            //InitializeHealth();
        }

        public void IncreaseHealth(int healthToIncrease)
        {
            OnPlayerIncreaseHealth?.Invoke();
            int health = Mathf.RoundToInt(playerHealth.CurrentValue * maxHealth);
            int val = health + healthToIncrease;
            playerHealth.CurrentValue = val > maxHealth ? 1 : val / maxHealth;
        }

        private PlayerInputActions _playerInputActions;

        private Vector2 GetMovementInputVector(PlayerInputActions playerInputActions)
        {
            _playerInputActions = playerInputActions;
            Vector2 input = _playerInputActions.Player.Movement.ReadValue<Vector2>();
            isMoving = input.magnitude > 0;
            return input;
        }

        public void IncreaseMaxHealth(int maxHealthToIncrease)
        {
            maxHealth = maxHealthToIncrease;
        }

        public void ReduceHealth(int healthToReduce)
        {
            OnPlayerHealthReduce?.Invoke();
            playerHealth.CurrentValue -= healthToReduce / maxHealth;
            if (playerHealth.CurrentValue <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            OnPlayerDie?.Invoke();
            playerHealth.CurrentValue = 1;
        }

        public void InitializeHealth()
        {
            if (!playerHealth.IsInitialized)
            {
                playerHealth.CurrentValue = 1;
                playerHealth.IsInitialized = true;
            }
        }

        public void Move()
        {
            if (!isRolling)
            {
                _rigidbody2D.velocity = _moveDirection.normalized * CalculateSpeed();
            }
            else
            {
                _rigidbody2D.velocity = _rollDirection * rollSpeed;
            }
        }

        public void Roll(bool isRoll)
        {
            isRolling = isRoll;
            if (isRolling)
            {
                Vector2 velocity = _rigidbody2D.velocity;
                _rollDirection = velocity.normalized;
                rollSpeed = velocity.magnitude * 1.2f;
            }
        }

        private float CalculateSpeed()
        {
            if (isMoving)
            {
                return moveSpeed;
            }

            return 0;
        }
    }
}