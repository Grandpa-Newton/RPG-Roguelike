using System;
using App.Scripts.DungeonScene.Items;
using UnityEngine;

namespace App.Scripts.MixedScenes.Weapon.RangeWeapon
{
    public class RangeWeapon : Weapon
    {
        [SerializeField] private RangeWeaponSO rangeWeaponData;

        [SerializeField] private SwitchWeaponBetweenRangeAndMelee switchWeaponBetweenRangeAndMelee;

        private float _timeToNextShot;
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
            rangeWeaponData = data;
        }

        private void Update()
        {
            DealDamage();
        }

        public override void DealDamage()
        {
            if (!rangeWeaponData)
            {
                switchWeaponBetweenRangeAndMelee.PlayerHandsVisible(false);
                return;
            }

            _timeToNextShot += Time.deltaTime;
            if (_playerInputActions.Player.Attack.IsPressed() && _timeToNextShot > rangeWeaponData.attackRate)
            {
                Bullet bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
                if (bullet)
                {
                    audioSource.PlayOneShot(rangeWeaponData.weaponAttackSound);
                    SetBulletParameters(bullet);

                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(_aimTransform.right * rangeWeaponData.bulletSpeed, ForceMode2D.Impulse);
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
            
            rangeWeaponData = (RangeWeaponSO)rangeWeaponSo;
            switchWeaponBetweenRangeAndMelee.PlayerHandsVisible(true);
            GetComponent<SpriteRenderer>().sprite = rangeWeaponData.ItemImage;
            GetComponent<AudioSource>().PlayOneShot(rangeWeaponData.weaponEquipSound);
        }

        private void SetBulletParameters(Bullet bullet)
        {
            bullet.SetRangeWeaponSO(rangeWeaponData);
            bullet.SetBulletSO(rangeWeaponData.bulletSO);
            bullet.SetRangeWeapon(this);
            bullet.transform.SetParent(bulletsContainer, true);

            OnProportiesSet?.Invoke();
        }
    }
}