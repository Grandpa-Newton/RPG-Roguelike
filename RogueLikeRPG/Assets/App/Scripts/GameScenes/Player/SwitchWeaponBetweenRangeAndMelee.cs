using System;
using App.Scripts.GameScenes.Player.Components;
using App.Scripts.GameScenes.Player.UI;
using App.Scripts.GameScenes.Weapon.MeleeWeapon;
using App.Scripts.GameScenes.Weapon.RangeWeapon;
using UnityEngine;

namespace App.Scripts.GameScenes.Player
{
    public class SwitchWeaponBetweenRangeAndMelee
    {
        private static SwitchWeaponBetweenRangeAndMelee _instance;
        public static SwitchWeaponBetweenRangeAndMelee Instance => _instance ??= new SwitchWeaponBetweenRangeAndMelee();

        private const float SwapReloadTime = 0.5f;
        private Transform _meleeWeapon;
        private Transform _rangeWeapon;

        private Transform[] _hands;

        private GameObject _currentPickedWeapon;

        private bool _isMeleeWeapon = true;
        private bool _isRolling;
        private float _timerToSwapWeapon;

        public event Action<bool> OnPlayerSwapWeapon;

        public bool CheckCurrentPickedMeleeWeapon()
        {
            return _isMeleeWeapon;
        }

        public void Initialize(Transform meleeWeapon, Transform rangeWeapon, Transform[] hands)
        {
            _meleeWeapon = meleeWeapon;
            _rangeWeapon = rangeWeapon;
            _hands = hands;
            PlayerAnimator.OnPlayerRolling += SetRollState;
        }

        private void SetRollState(bool isRolling)
        {
            _isRolling = isRolling;
        }

        public void CheckAvailableWeapons()
        {
            MeleeWeaponSO meleeWeaponSO =
                PlayerCurrentWeapon.Instance.CurrentMeleeAndRangeWeaponsSO.EquippedMeleeWeapon;
            RangeWeaponSO rangeWeaponSO =
                PlayerCurrentWeapon.Instance.CurrentMeleeAndRangeWeaponsSO.EquippedRangeWeapon;

            if (!meleeWeaponSO && !rangeWeaponSO)
            {
                PlayerHandsVisible(false);
                return;
            }

            if (meleeWeaponSO)
            {
                _isMeleeWeapon = true;
                OnPlayerSwapWeapon?.Invoke(_isMeleeWeapon);
                PlayerCurrentWeaponUI.Instance.SetMeleeWeaponIcon(meleeWeaponSO.ItemImage);
                _meleeWeapon.gameObject.SetActive(true);
                _rangeWeapon.gameObject.SetActive(false);
                _currentPickedWeapon = _meleeWeapon.gameObject;
                PlayerCurrentWeaponUI.Instance.IncreaseMeleeWeaponScale();
            }

            if (rangeWeaponSO)
            {
                PlayerCurrentWeaponUI.Instance.SetRangeWeaponIcon(rangeWeaponSO.ItemImage);
                if (!meleeWeaponSO)
                {
                    _isMeleeWeapon = false;
                    OnPlayerSwapWeapon?.Invoke(_isMeleeWeapon);
                    _rangeWeapon.gameObject.SetActive(true);
                    _meleeWeapon.gameObject.SetActive(false);
                    _currentPickedWeapon = _rangeWeapon.gameObject;
                    PlayerCurrentWeaponUI.Instance.IncreaseRangeWeaponScale();
                }
            }
        }

        public void SwapWeapon()
        {
            MeleeWeaponSO meleeWeaponSO =
                PlayerCurrentWeapon.Instance.CurrentMeleeAndRangeWeaponsSO.EquippedMeleeWeapon;
            RangeWeaponSO rangeWeaponSO =
                PlayerCurrentWeapon.Instance.CurrentMeleeAndRangeWeaponsSO.EquippedRangeWeapon;

            if (!meleeWeaponSO || !rangeWeaponSO)
            {
                return;
            }

            _timerToSwapWeapon += Time.deltaTime;
            if (Input.GetAxis("Mouse ScrollWheel") != 0 && !_isRolling && _timerToSwapWeapon > SwapReloadTime)
            {
                _isMeleeWeapon = !_isMeleeWeapon;
                OnPlayerSwapWeapon?.Invoke(_isMeleeWeapon);
                if (_isMeleeWeapon)
                {
                    PlayerCurrentWeaponUI.Instance.IncreaseMeleeWeaponScale();
                    SetActiveMeleeWeapon();
                }
                else
                {
                    PlayerCurrentWeaponUI.Instance.IncreaseRangeWeaponScale();
                    SetActiveRangeWeapon();
                }

                _timerToSwapWeapon = 0f;
            }
        }

        public void SetActiveMeleeWeapon()
        {
            MeleeWeaponSO meleeWeaponSO =
                PlayerCurrentWeapon.Instance.CurrentMeleeAndRangeWeaponsSO.EquippedMeleeWeapon;
            _meleeWeapon.gameObject.SetActive(true);
            _rangeWeapon.gameObject.SetActive(false);

            _currentPickedWeapon = _meleeWeapon.gameObject;

            if (!meleeWeaponSO)
            {
                PlayerHandsVisible(true);
            }
        }

        public void SetActiveRangeWeapon()
        {
            RangeWeaponSO rangeWeaponSO =
                PlayerCurrentWeapon.Instance.CurrentMeleeAndRangeWeaponsSO.EquippedRangeWeapon;

            _rangeWeapon.gameObject.SetActive(true);
            _meleeWeapon.gameObject.SetActive(false);

            _currentPickedWeapon = _rangeWeapon.gameObject;

            if (!rangeWeaponSO)
            {
                PlayerHandsVisible(true);
            }
        }

        public void WeaponAndHandsDisable()
        {
            if (!_currentPickedWeapon) return;

            _currentPickedWeapon.SetActive(false);
            PlayerHandsVisible(false);
        }

        public void WeaponAndHandsEnable()
        {
            if (!_currentPickedWeapon) return;

            _currentPickedWeapon.SetActive(true);
            PlayerHandsVisible(true);
        }

        public void PlayerHandsVisible(bool isActive)
        {
            foreach (var hand in _hands)
            {
                hand.gameObject.SetActive(isActive);
            }
        }

        public void Dispose()
        {
            PlayerAnimator.OnPlayerRolling -= SetRollState;
        }
    }
}