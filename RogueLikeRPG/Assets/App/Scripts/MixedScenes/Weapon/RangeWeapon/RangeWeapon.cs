using System;
using Inventory.Model;
using UnityEngine;

namespace App.Scripts.MixedScenes.Weapon.RangeWeapon
{
    public class RangeWeapon : Weapon
    {
        [SerializeField] private RangeWeaponSO _rangeWeaponData;

        [SerializeField] private SwitchWeaponBetweenRangeAndMelee _switchWeaponBetweenRangeAndMelee;

        private float _timeToNextShot = 0;
        private PlayerInputActions _playerInputActions;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private Transform bulletsContainer;
        [SerializeField] private Bullet bulletPrefab;
        private Transform _aimTransform;

        [SerializeField] private AudioSource audioSource;
        public event Action OnProportiesSet;

        private void Awake()
        {
            _aimTransform = transform.root.Find("Aim");
        }

        private void Start()
        {
            _playerInputActions = InputManager.Instance.PlayerInputActions;
        }

        public RangeWeapon(RangeWeaponSO data)
        {
            _rangeWeaponData = data;
        }

        private void Update()
        {
            DealDamage();
        }

        private void SetActivePlayerHands()
        {
            if (!_rangeWeaponData)
            {
                //set active true;
            }
            else
            {
            }
        }

        public override void DealDamage()
        {
            if (!_rangeWeaponData)
            {
                _switchWeaponBetweenRangeAndMelee.PlayerHandsVisible(false);
                return;
            }

            _timeToNextShot += Time.deltaTime;
            if (_playerInputActions.Player.Attack.IsPressed() && _timeToNextShot > _rangeWeaponData.attackRate)
            {
                Bullet bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
                if (bullet)
                {
                    audioSource.PlayOneShot(_rangeWeaponData.weaponAttackSound);
                    SetBulletParameters(bullet);

                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(_aimTransform.right * _rangeWeaponData.bulletSpeed, ForceMode2D.Impulse);
                }

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
            
            _rangeWeaponData = (RangeWeaponSO)rangeWeaponSo;
            _switchWeaponBetweenRangeAndMelee.PlayerHandsVisible(true);
            GetComponent<SpriteRenderer>().sprite = _rangeWeaponData.ItemImage;
            GetComponent<AudioSource>().PlayOneShot(_rangeWeaponData.weaponEquipSound);
        }

        private void SetBulletParameters(Bullet bullet)
        {
            bullet.SetRangeWeaponSO(_rangeWeaponData);
            bullet.SetBulletSO(_rangeWeaponData.bulletSO);
            bullet.SetRangeWeapon(this);
            bullet.transform.SetParent(bulletsContainer, true);

            OnProportiesSet?.Invoke();
        }
    }
}