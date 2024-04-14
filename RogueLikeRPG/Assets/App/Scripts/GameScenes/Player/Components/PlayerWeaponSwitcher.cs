using System;
using App.Scripts.GameScenes.Player.UI;
using App.Scripts.GameScenes.Weapon;
using App.Scripts.GameScenes.Weapon.MeleeWeapon;
using App.Scripts.GameScenes.Weapon.RangeWeapon;
using UnityEngine;

namespace App.Scripts.GameScenes.Player.Components
{
    public class PlayerWeaponSwitcher
    {
        private static PlayerWeaponSwitcher _instance;
        public static PlayerWeaponSwitcher Instance => _instance ??= new PlayerWeaponSwitcher();

        private const float SwapReloadTime = 0.5f;
        private Transform _meleeWeapon;
        private Transform _rangeWeapon;

        private Transform[] _hands;

        private GameObject _currentPickedWeapon;

        private bool _isMeleeWeapon = true;
        private bool _isRolling;
        private float _timerToSwapWeapon;

        public event Action<bool> OnSwapWeapon;

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
                Debug.Log("WARNING!!!");
                PlayerHandsVisible(false);
                return;
            }

            if (meleeWeaponSO)
            {
                Debug.Log("NE MELEE WARNING!!!");
                _isMeleeWeapon = true;
                OnSwapWeapon?.Invoke(_isMeleeWeapon);
                PlayerCurrentWeaponUI.Instance.SetWeaponIcon(meleeWeaponSO);
                PlayerHandsVisible(true);
                _meleeWeapon.gameObject.SetActive(true);
                _rangeWeapon.gameObject.SetActive(false);
                _currentPickedWeapon = _meleeWeapon.gameObject;
                PlayerCurrentWeaponUI.Instance.AdjustWeaponScale(meleeWeaponSO);
            }

            if (rangeWeaponSO)
            {
                    Debug.Log("NE RANGE WARNING!!!");
                PlayerCurrentWeaponUI.Instance.SetWeaponIcon(rangeWeaponSO);
                if (!meleeWeaponSO)
                {

                    _isMeleeWeapon = false;
                    OnSwapWeapon?.Invoke(_isMeleeWeapon);
                    PlayerHandsVisible(true);
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
                OnSwapWeapon?.Invoke(_isMeleeWeapon);
                if (_isMeleeWeapon)
                {
                    PlayerCurrentWeaponUI.Instance.AdjustWeaponScale(meleeWeaponSO);
                    SetActiveWeapon(_isMeleeWeapon);
                }
                else
                {
                    PlayerCurrentWeaponUI.Instance.AdjustWeaponScale(rangeWeaponSO);
                    SetActiveWeapon(_isMeleeWeapon);
                }

                _timerToSwapWeapon = 0f;
            }
        }
        
        public void SetActiveWeapon(bool isMeleeWeapon)
        {
            WeaponItemSO weaponSO = isMeleeWeapon 
                ? PlayerCurrentWeapon.Instance.CurrentMeleeAndRangeWeaponsSO.EquippedMeleeWeapon 
                : PlayerCurrentWeapon.Instance.CurrentMeleeAndRangeWeaponsSO.EquippedRangeWeapon;

            GameObject weaponToActivate = isMeleeWeapon ? _meleeWeapon.gameObject : _rangeWeapon.gameObject;
            GameObject weaponToDeactivate = isMeleeWeapon ? _rangeWeapon.gameObject : _meleeWeapon.gameObject;

            weaponToActivate.SetActive(true);
            weaponToDeactivate.SetActive(false);

            _currentPickedWeapon = weaponToActivate;
                
            PlayerHandsVisible(true);
            
        }
        
        public void WeaponAndHandsVisibility(bool isActive)
        {
            if (!_currentPickedWeapon) return;

            _currentPickedWeapon.SetActive(isActive);
            PlayerHandsVisible(isActive);
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