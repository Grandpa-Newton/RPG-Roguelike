using System;
using App.Scripts.GameScenes.Player.Components;
using App.Scripts.GameScenes.Player.UI;
using App.Scripts.GameScenes.Weapon.MeleeWeapon;
using App.Scripts.GameScenes.Weapon.RangeWeapon;
using UnityEngine;

namespace App.Scripts.GameScenes.Player
{
    public class WeaponSwitcher
    {
        private static WeaponSwitcher _instance;
        public static WeaponSwitcher Instance => _instance ??= new WeaponSwitcher();

        private const float SwapReloadTime = 0.5f;
        private Transform _meleeWeapon;
        private Transform _rangeWeapon;

        private Transform[] _hands;

        private GameObject _currentPickedWeapon;

        private bool _isMeleeWeapon = true;
        private bool _isRolling;
        private float _timerToSwapWeapon;

        public event Action<bool> OnPlayerSwapWeapon;

        public void Initialize(Transform meleeWeapon, Transform rangeWeapon, Transform[] hands)
        {
            _meleeWeapon = meleeWeapon;
            _rangeWeapon = rangeWeapon;
            _hands = hands;
            PlayerAnimator.Instance.OnPlayerRolling += SetRollState;
            PlayerController.Instance.OnPlayerSwapWeapon += SwapWeapon;
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
                PlayerCurrentWeaponUI.Instance.SetWeaponIcon(meleeWeaponSO);
                //PlayerCurrentWeaponUI.Instance.SetMeleeWeaponIcon(meleeWeaponSO.ItemImage);
                _meleeWeapon.gameObject.SetActive(true);
                _rangeWeapon.gameObject.SetActive(false);
                _currentPickedWeapon = _meleeWeapon.gameObject;
                PlayerCurrentWeaponUI.Instance.AdjustWeaponScale(meleeWeaponSO);
            }

            if (rangeWeaponSO)
            {
                PlayerCurrentWeaponUI.Instance.SetWeaponIcon(rangeWeaponSO);
                if (!meleeWeaponSO)
                {
                    _isMeleeWeapon = false;
                    OnPlayerSwapWeapon?.Invoke(_isMeleeWeapon);
                    _rangeWeapon.gameObject.SetActive(true);
                    _meleeWeapon.gameObject.SetActive(false);
                    _currentPickedWeapon = _rangeWeapon.gameObject;
                    PlayerCurrentWeaponUI.Instance.AdjustWeaponScale(rangeWeaponSO);
                }
            }
        }

        private void SwapWeapon()
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
                    PlayerCurrentWeaponUI.Instance.AdjustWeaponScale(meleeWeaponSO);
                    SetActiveMeleeWeapon();
                }
                else
                {
                    PlayerCurrentWeaponUI.Instance.AdjustWeaponScale(rangeWeaponSO);
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
            PlayerAnimator.Instance.OnPlayerRolling -= SetRollState;
            PlayerController.Instance.OnPlayerSwapWeapon -= SwapWeapon;
        }
    }
}