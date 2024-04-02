using App.Scripts.GameScenes.Player.Components;
using App.Scripts.GameScenes.Player.UI;
using App.Scripts.GameScenes.Weapon;
using UnityEngine;

namespace App.Scripts.GameScenes.Player
{
    public class SwitchWeaponBetweenRangeAndMelee
    {
        private static SwitchWeaponBetweenRangeAndMelee _instance;

        public static SwitchWeaponBetweenRangeAndMelee Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SwitchWeaponBetweenRangeAndMelee();
                }

                return _instance;
            }
        }

        private const float SwapReloadTime = 0.5f;
        private Transform _meleeWeapon;
        private Transform _rangeWeapon;

        private Transform[] _hands;

        private GameObject _currentPickedWeapon;
        private CurrentWeaponsSO _currentWeaponsSO;

        private bool _isMeleeWeapon = true;
        private bool _isRolling;
        private float _timerToSwapWeapon = 0f;

        private PlayerController _playerController;
    
        public bool CheckCurrentPickedMeleeWeapon()
        {
            return _isMeleeWeapon;
        }
        public void Initialize(PlayerController playerController, Transform meleeWeapon, Transform rangeWeapon,Transform[] hands, CurrentWeaponsSO currentWeaponsSO)
        {
            _meleeWeapon = meleeWeapon;
            _rangeWeapon = rangeWeapon;
            _hands = hands;
            _currentWeaponsSO = currentWeaponsSO;
            _playerController = playerController;
            PlayerAnimator.OnPlayerRolling += SetRollState;
        
        }
        private void SetRollState(bool isRolling)
        {
            _isRolling = isRolling;
        }
        public void CheckAvailableWeapons()
        {
            if (!_currentWeaponsSO.EquipRangeWeapon && !_currentWeaponsSO.EquipMeleeWeapon)
            {
                PlayerHandsVisible(false);
                return;
            }

            if (_currentWeaponsSO.EquipMeleeWeapon)
            {
                PlayerCurrentWeaponUI.Instance.SetMeleeWeaponIcon(_currentWeaponsSO.EquipMeleeWeapon.ItemImage);
                _meleeWeapon.gameObject.SetActive(true);
                _rangeWeapon.gameObject.SetActive(false);
                _currentPickedWeapon = _meleeWeapon.gameObject;
                PlayerCurrentWeaponUI.Instance.IncreaseMeleeWeaponScale();
            }

            if (_currentWeaponsSO.EquipRangeWeapon)
            {
                PlayerCurrentWeaponUI.Instance.SetRangeWeaponIcon(_currentWeaponsSO.EquipRangeWeapon.ItemImage);
                if (!_currentWeaponsSO.EquipMeleeWeapon)
                {
                    _rangeWeapon.gameObject.SetActive(true);
                    _meleeWeapon.gameObject.SetActive(false);
                    _currentPickedWeapon = _rangeWeapon.gameObject;
                    PlayerCurrentWeaponUI.Instance.IncreaseRangeWeaponScale();
                }
            }
        }
        public void SwapWeapon()
        {
            if (!_currentWeaponsSO.EquipMeleeWeapon || !_currentWeaponsSO.EquipRangeWeapon)
            {
                return;
            }

            _timerToSwapWeapon += Time.deltaTime;
            if (Input.GetAxis("Mouse ScrollWheel") != 0 && !_isRolling && _timerToSwapWeapon > SwapReloadTime)
            {
                _isMeleeWeapon = !_isMeleeWeapon;
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
            _meleeWeapon.gameObject.SetActive(true);
            _rangeWeapon.gameObject.SetActive(false);
            
            _currentPickedWeapon = _meleeWeapon.gameObject;
            
            if (!_currentWeaponsSO.EquipMeleeWeapon)
            {
                PlayerHandsVisible(true);
            }
        }

        public void SetActiveRangeWeapon()
        {
            _rangeWeapon.gameObject.SetActive(true);
            _meleeWeapon.gameObject.SetActive(false);
            
            _currentPickedWeapon = _rangeWeapon.gameObject;
            
            if (!_currentWeaponsSO.EquipRangeWeapon)
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