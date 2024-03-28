using System.Collections;
using App.Scripts.DungeonScene.Enemy;
using App.Scripts.DungeonScene.Items;
using App.Scripts.MixedScenes.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace App.Scripts.MixedScenes.Weapon.MeleeWeapon
{
    public class MeleeWeapon : Weapon
    {
        [SerializeField] private MeleeWeaponSO meleeWeaponData;
        [SerializeField] private Animator _animator;
        private PlayerInputActions _playerInputActions;
        [SerializeField] private CurrentWeaponsSO _currentWeaponsSO;

        private float _timeToNextAttack = 0;
        private Vector2 _defaultPosition;
        private Vector2 _currentPointPosition;

        public MeleeWeapon(MeleeWeaponSO data)
        {
            meleeWeaponData = data;
        }

        private void Awake()
        {
            //_playerInputActions = InputManager.Instance.PlayerInputActions;
        }

        private void Start()
        {
            _playerInputActions = InputManager.Instance.PlayerInputActions;

            if (_currentWeaponsSO.EquipMeleeWeapon)
            {
                meleeWeaponData = (MeleeWeaponSO)_currentWeaponsSO.EquipMeleeWeapon;
                SetWeapon(meleeWeaponData);
            }
        }

        private void Update()
        {
            DealDamage();
        }

        public override void DealDamage()
        {
            if (!meleeWeaponData)
            {
                return;
            }

            _timeToNextAttack += Time.deltaTime;
            if (_playerInputActions.Player.Attack.IsPressed() && _timeToNextAttack > meleeWeaponData.attackRate)
            {
                _animator.SetTrigger("Shoot");
                _timeToNextAttack = 0;
                StartCoroutine(WaitToNextAttack());
                // _rb.AddForce(_aimTransform.position * _forceMultiplier, ForceMode2D.Impulse);
                _animator.SetTrigger("Idle");
            }
        }

        IEnumerator WaitToNextAttack()
        {
            yield return new WaitForSeconds(meleeWeaponData.attackRate);
        }

        public override void SetWeapon(ItemSO meleeWeaponSo)
        {
            if (!meleeWeaponSo)
            {
                Debug.LogError("MeleeWeapon IS NULL");
                return;
            }

            meleeWeaponData = (MeleeWeaponSO)meleeWeaponSo;
            SwitchWeaponBetweenRaM.Instance.PlayerHandsVisible(true);
            GetComponent<SpriteRenderer>().sprite = meleeWeaponData.ItemImage;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(meleeWeaponData.damage);
            }
        }
    }
}