using System;
using App.Scripts.DungeonScene.Enemy;
using App.Scripts.DungeonScene.Items;
using Unity.VisualScripting;
using UnityEngine;

namespace App.Scripts.MixedScenes.Weapon.MeleeWeapon
{
    public class MeleeWeapon : Weapon
    {
        private static MeleeWeapon _instance;

        public static MeleeWeapon Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MeleeWeapon();
                }

                return _instance;
            }
        }

        public event Action<Enemy> OnHitEnemy;
        
        private MeleeWeaponSO _meleeWeaponSO;
        private CurrentWeaponsSO _currentWeaponsSO;
        private SpriteRenderer _spriteRenderer;

        private Animator _animator;
        private PlayerInputActions _playerInputActions;

        private Vector2 _defaultPosition;
        private Vector2 _currentPointPosition;
        
        
        private float _attackTimer;
        private bool _isAttacking;

        private static readonly int Shoot = Animator.StringToHash("Shoot");
        private static readonly int Idle = Animator.StringToHash("Idle");

        public void Initialize(CurrentWeaponsSO currentWeaponsSO,
            PlayerInputActions playerInputActions, SpriteRenderer spriteRenderer, Animator animator)
        {
            _currentWeaponsSO = currentWeaponsSO;
            _playerInputActions = playerInputActions;
            _spriteRenderer = spriteRenderer;
            _animator = animator;

            TryEquipMeleeWeapon();
        }


        private void TryEquipMeleeWeapon()
        {
            if (!_currentWeaponsSO.EquipMeleeWeapon) return;

            _meleeWeaponSO = (MeleeWeaponSO)_currentWeaponsSO.EquipMeleeWeapon;
            SetWeapon(_meleeWeaponSO);
        }
        
      

        public override void DealDamage(float deltaTime)
        {
            if (!_meleeWeaponSO)
            {
                return;
            }

            _attackTimer += deltaTime;
            if (_playerInputActions.Player.Attack.IsPressed() && !_isAttacking)
            {
                _isAttacking = true;
                _animator.SetTrigger(Shoot);
                _attackTimer = 0;
            }

            if (_isAttacking && _attackTimer > _meleeWeaponSO.attackRate)
            {
                _isAttacking = false;
                _animator.SetTrigger(Idle);
            }
        }

        public override void SetWeapon(ItemSO meleeWeaponSo)
        {
            if (!meleeWeaponSo)
            {
                Debug.LogError("MeleeWeapon IS NULL");
                return;
            }

            _meleeWeaponSO = (MeleeWeaponSO)meleeWeaponSo;
            MeleeWeaponTrigger.Instance.SetMeleeWeaponSO(_meleeWeaponSO);
            SwitchWeaponBetweenRangeAndMelee.Instance.PlayerHandsVisible(true);
            _spriteRenderer.sprite = _meleeWeaponSO.ItemImage;
        }
    }
}