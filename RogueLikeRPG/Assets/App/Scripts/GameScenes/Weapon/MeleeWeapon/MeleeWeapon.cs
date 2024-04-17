using System;
using App.Scripts.DungeonScene.Enemy;
using App.Scripts.DungeonScene.Items;
using App.Scripts.GameScenes.Player;
using App.Scripts.GameScenes.Player.Components;
using UnityEngine;

namespace App.Scripts.GameScenes.Weapon.MeleeWeapon
{
    public class MeleeWeapon : Weapon
    {
        private static MeleeWeapon _instance;
        public static MeleeWeapon Instance => _instance ??= new MeleeWeapon();

        private Animator _animator;
        private AudioSource _audioSource;
        private MeleeWeaponSO _meleeWeaponSO;
        private SpriteRenderer _spriteRenderer;
        private PlayerInputActions _playerInputActions;

        private Transform _circleOrigin;
        private float _radius = 0.55f;

        private Vector2 _currentPointPosition;
        private Vector2 _defaultPosition;

        private float _attackTimer;
        private bool _isAttacking;

        private static readonly int Shoot = Animator.StringToHash("Attack");
        private static readonly int Idle = Animator.StringToHash("Idle");

        public event Action<bool> OnPlayerAttack;

        public void Initialize(Transform circleOrigin, PlayerInputActions playerInputActions,
            SpriteRenderer spriteRenderer, Animator animator, AudioSource audioSource)
        {
            _circleOrigin = circleOrigin;
            _playerInputActions = playerInputActions;
            _spriteRenderer = spriteRenderer;
            _animator = animator;
            _audioSource = audioSource;
            TryEquipMeleeWeapon();
        }

        private void TryEquipMeleeWeapon()
        {
            if (!PlayerCurrentWeapon.Instance.CurrentMeleeAndRangeWeaponsSO.EquippedMeleeWeapon) return;

            _meleeWeaponSO = PlayerCurrentWeapon.Instance.CurrentMeleeAndRangeWeaponsSO.EquippedMeleeWeapon;
            SetWeapon(_meleeWeaponSO);
        }

        public void Attack()
        {
            if (!_meleeWeaponSO)
            {
                return;
            }

            _attackTimer += Time.deltaTime;
            // Debug.Log(_attackTimer + " sec.");
            if (_playerInputActions.Player.Attack.IsPressed() && !_isAttacking)
            {
                Debug.Log("Bonk@!");
                _isAttacking = true;
                OnPlayerAttack?.Invoke(_isAttacking);
                _animator.SetTrigger(Shoot);
                _attackTimer = 0;
            }

            if (_isAttacking && _attackTimer > _meleeWeaponSO.attackRate)
            {
                _isAttacking = false;
                OnPlayerAttack?.Invoke(_isAttacking);
                // _animator.SetTrigger(Idle);
            }
        }

        public override void SetWeapon(ItemSO meleeWeaponSo)
        {
            if (!meleeWeaponSo)
            {
                Debug.LogError("MeleeWeapon IS NULL");
                return;
            }

            MeleeWeaponSO meleeWeapon = meleeWeaponSo as MeleeWeaponSO;
            if (!meleeWeapon)
            {
                Debug.LogError("ItemSO is not a RangeWeaponSO");
                return;
            }

            _meleeWeaponSO = meleeWeapon;
            PlayerWeaponSwitcher.Instance.PlayerHandsVisible(true);
            _spriteRenderer.sprite = _meleeWeaponSO.ItemImage;

            MeleeWeaponTrigger.Instance.SetMeleeWeaponSO(_meleeWeaponSO);

            _audioSource.PlayOneShot(_meleeWeaponSO.weaponEquipSound);
        }
    }
}