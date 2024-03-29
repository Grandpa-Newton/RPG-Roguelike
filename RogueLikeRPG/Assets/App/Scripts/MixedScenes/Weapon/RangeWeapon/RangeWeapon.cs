using System;
using App.Scripts.DungeonScene.Items;
using App.Scripts.MixedScenes.Player;
using UnityEngine;

namespace App.Scripts.MixedScenes.Weapon.RangeWeapon
{
    public class RangeWeapon : Weapon
    {
        private static RangeWeapon _instance;

        public static RangeWeapon Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RangeWeapon();
                }

                return _instance;
            }
        }

        private RangeWeaponSO _rangeWeaponSO;
        private CurrentWeaponsSO _currentWeaponsSO;
        private SpriteRenderer _spriteRenderer;

        private PlayerInputActions _playerInputActions;
        private AudioSource _audioSource;

        private Transform shootPoint;
        private Transform bulletsContainer;
        private Transform _aimTransform;
        
        private float _timeToNextShot;
        private Bullet bulletPrefab;

        public event Action OnProportiesSet;

        public void Initialize(CurrentWeaponsSO currentWeaponsSO, PlayerInputActions playerInputActions,SpriteRenderer spriteRenderer, Transform aimTransform, AudioSource audioSource)
        {
            _currentWeaponsSO = currentWeaponsSO;
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
        /*public RangeWeapon(RangeWeaponSO data)
        {
            rangeWeaponData = data;
        }*/

        /*private void Start()
        {
            _playerInputActions = InputManager.Instance.PlayerInputActions;

            if (_currentWeaponsSO.EquipRangeWeapon)
            {
                _rangeWeaponSO = (RangeWeaponSO)_currentWeaponsSO.EquipRangeWeapon;
                SetWeapon(_rangeWeaponSO);
            }
        }*/


        /*private void Update()
        {
            DealDamage();
        }*/

        public override void DealDamage(float deltaTime)
        {
            if (!_rangeWeaponSO)
            {
                return;
            }

            _timeToNextShot += Time.deltaTime;
            if (_playerInputActions.Player.Attack.IsPressed() && _timeToNextShot > _rangeWeaponSO.attackRate)
            {
                Debug.Log("Bang!");
                 /*Bullet bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
                if (bullet)
                {
                    _audioSource.PlayOneShot(_rangeWeaponSO.weaponAttackSound);
                    SetBulletParameters(bullet);

                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(_aimTransform.right * _rangeWeaponSO.bulletSpeed, ForceMode2D.Impulse);
                }*/

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

            SwitchWeaponBetweenRaM.Instance.PlayerHandsVisible(true);
            _spriteRenderer.sprite = _rangeWeaponSO.ItemImage;
            _audioSource.PlayOneShot(_rangeWeaponSO.weaponEquipSound);
        }

        /*public override void DealDamage(float deltaTime)
        {
            if (!_rangeWeaponSO)
            {
                return;
            }

            _timeToNextShot += Time.deltaTime;
            if (_playerInputActions.Player.Attack.IsPressed() && _timeToNextShot > _rangeWeaponSO.attackRate)
            {
                Bullet bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
                if (bullet)
                {
                    audioSource.PlayOneShot(_rangeWeaponSO.weaponAttackSound);
                    SetBulletParameters(bullet);

                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(_aimTransform.right * _rangeWeaponSO.bulletSpeed, ForceMode2D.Impulse);
                }

                _timeToNextShot = 0f;
            }
        }
        */
        /*public  void SetWeapon(ItemSO rangeWeaponSo)
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

            rangeWeaponData = rangeWeapon;

            SwitchWeaponBetweenRaM.Instance.PlayerHandsVisible(true);
            GetComponent<SpriteRenderer>().sprite = rangeWeaponData.ItemImage;
            GetComponent<AudioSource>().PlayOneShot(rangeWeaponData.weaponEquipSound);
        }*/

        private void SetBulletParameters(Bullet bullet, Transform bulletContainer)
        {
            bullet.SetRangeWeaponSO(_rangeWeaponSO);
            bullet.SetBulletSO(_rangeWeaponSO.bulletSO);
            bullet.SetRangeWeapon(this);
            bullet.transform.SetParent(bulletsContainer, true);

            OnProportiesSet?.Invoke();
        }
    }
}