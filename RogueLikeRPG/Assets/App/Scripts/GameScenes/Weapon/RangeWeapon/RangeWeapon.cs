using App.Scripts.DungeonScene.Items;
using App.Scripts.GameScenes.Player;
using App.Scripts.GameScenes.Weapon.Bullet;
using UnityEngine;

namespace App.Scripts.GameScenes.Weapon.RangeWeapon
{
    public class RangeWeapon : Weapon
    {
        private static RangeWeapon _instance;

        public static RangeWeapon Instance => _instance ??= new RangeWeapon();

        private RangeWeaponSO _rangeWeaponSO;
        private CurrentWeaponsSO _currentWeaponsSO;
        private SpriteRenderer _spriteRenderer;

        private PlayerInputActions _playerInputActions;
        private AudioSource _audioSource;

        private Transform _shootPoint;
        private Transform _bulletsContainer;
        private Transform _aimTransform;
        
        private float _timeToNextShot;
        private Bullet.Bullet _bulletPrefab;

        public void Initialize(CurrentWeaponsSO currentWeaponsSO,Bullet.Bullet bulletPrefab, PlayerInputActions playerInputActions,SpriteRenderer spriteRenderer, Transform aimTransform, AudioSource audioSource)
        {
            _currentWeaponsSO = currentWeaponsSO;
            _bulletPrefab = bulletPrefab;
            _playerInputActions = playerInputActions;
            _spriteRenderer = spriteRenderer;
            _aimTransform = aimTransform;
            _audioSource = audioSource;
            TryEquipRangeWeapon();
        }

        private void TryEquipRangeWeapon()
        {
            if (!_currentWeaponsSO.EquipRangeWeapon) return;
            
            _rangeWeaponSO = (RangeWeaponSO)_currentWeaponsSO.EquipRangeWeapon;
            SetWeapon(_rangeWeaponSO);
        }

        public void Shoot(BulletFactory bulletFactory)
        {
            if (!_rangeWeaponSO)
            {
                Debug.Log(_rangeWeaponSO == null);
                return;
            }

            _timeToNextShot += Time.deltaTime;
            if (_playerInputActions.Player.Attack.IsPressed() && _timeToNextShot > _rangeWeaponSO.attackRate)
            {
                Debug.Log("Bang!");
                bulletFactory.CreateBullet(_bulletPrefab, _rangeWeaponSO, _audioSource, _aimTransform);
                _timeToNextShot = 0f;
            }
        }
        
        public override void SetWeapon(ItemSO rangeWeaponSo)
        {
            if (!rangeWeaponSo)
            {
                Debug.LogError("RangeWeaponSO IS NULL");
                return;
            }

            RangeWeaponSO rangeWeapon = rangeWeaponSo as RangeWeaponSO;
            if (!rangeWeapon)
            {
                Debug.LogError("ItemSO is not a RangeWeaponSO");
                return;
            }

            _rangeWeaponSO = rangeWeapon;

            SwitchWeaponBetweenRangeAndMelee.Instance.PlayerHandsVisible(true);
            _spriteRenderer.sprite = _rangeWeaponSO.ItemImage;
            _audioSource.PlayOneShot(_rangeWeaponSO.weaponEquipSound);
        }
    }
}