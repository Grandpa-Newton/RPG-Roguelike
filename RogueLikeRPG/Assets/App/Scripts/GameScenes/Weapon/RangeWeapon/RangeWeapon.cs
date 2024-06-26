using App.Scripts.DungeonScene.Items;
using App.Scripts.GameScenes.GameActions;
using App.Scripts.GameScenes.Player.Components;
using App.Scripts.GameScenes.Weapon.Bullet;
using UnityEngine;

namespace App.Scripts.GameScenes.Weapon.RangeWeapon
{
    public class RangeWeapon : Weapon
    {
        private static RangeWeapon _instance;

        public static RangeWeapon Instance => _instance ??= new RangeWeapon();

        private RangeWeaponSO _rangeWeaponSO;
        private SpriteRenderer _spriteRenderer;

        private PlayerInputActions _playerInputActions;
        private AudioSource _audioSource;

        private Transform _shootPoint;
        private Transform _bulletsContainer;
        private Transform _aimTransform;
        
        private float _timeToNextShot;
        private Bullet.Bullet _bulletPrefab;

        public void Initialize(Bullet.Bullet bulletPrefab, PlayerInputActions playerInputActions,SpriteRenderer spriteRenderer, Transform aimTransform, AudioSource audioSource)
        {
            _bulletPrefab = bulletPrefab;
            _playerInputActions = playerInputActions;
            _spriteRenderer = spriteRenderer;
            _aimTransform = aimTransform;
            _audioSource = audioSource;
            TryEquipRangeWeapon();
        }

        private void TryEquipRangeWeapon()
        {
            if (!PlayerCurrentWeapon.Instance.CurrentMeleeAndRangeWeaponsSO.EquippedRangeWeapon) return;
            
            _rangeWeaponSO = PlayerCurrentWeapon.Instance.CurrentMeleeAndRangeWeaponsSO.EquippedRangeWeapon;
            SetWeapon(_rangeWeaponSO);
        }
        
        public void Shoot(BulletFactory bulletFactory)
        {
            if (!_rangeWeaponSO)
            {
                return;
            }

            _timeToNextShot += Time.deltaTime;
            if (_playerInputActions.Player.Attack.IsPressed() && _timeToNextShot > _rangeWeaponSO.attackRate)
            {
                Debug.Log("Bang!");
                bulletFactory.CreateBullet(_bulletPrefab, _rangeWeaponSO, _audioSource, _aimTransform);
                CinemachineShake.Instance.ShakeCamera();
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
            PlayerWeaponSwitcher.Instance.PlayerHandsVisible(true);
            _spriteRenderer.sprite = _rangeWeaponSO.ItemImage;

            _audioSource.PlayOneShot(_rangeWeaponSO.weaponEquipSound);
        }
    }
}