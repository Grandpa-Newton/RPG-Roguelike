using App.Scripts.DungeonScene.Enemy;
using App.Scripts.DungeonScene.Items;
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

        private MeleeWeaponSO _meleeWeaponSO;
        private CurrentWeaponsSO _currentWeaponsSO;
        private SpriteRenderer _spriteRenderer;

        private Animator _animator;
        private PlayerInputActions _playerInputActions;

        private float _timeToNextAttack = 0;
        private Vector2 _defaultPosition;
        private Vector2 _currentPointPosition;


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

            _timeToNextAttack += deltaTime;
            if (_playerInputActions.Player.Attack.IsPressed() && _timeToNextAttack > _meleeWeaponSO.attackRate)
            {
                _animator.SetTrigger("Shoot");
                _timeToNextAttack = 0;
                WaitToNextAttack();
                _animator.SetTrigger("Idle");
            }
        }

        private void WaitToNextAttack()
        {
            _timeToNextAttack = _meleeWeaponSO.attackRate;
        }

        public override void SetWeapon(ItemSO meleeWeaponSo)
        {
            if (!meleeWeaponSo)
            {
                Debug.LogError("MeleeWeapon IS NULL");
                return;
            }

            _meleeWeaponSO = (MeleeWeaponSO)meleeWeaponSo;
            SwitchWeaponBetweenRangeAndMelee.Instance.PlayerHandsVisible(true);
            _spriteRenderer.sprite = _meleeWeaponSO.ItemImage;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(_meleeWeaponSO.damage);
            }
        }
    }
}