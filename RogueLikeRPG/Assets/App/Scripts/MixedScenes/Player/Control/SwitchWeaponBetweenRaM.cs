using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeaponBetweenRaM
{
    private static SwitchWeaponBetweenRaM _instance;

    public static SwitchWeaponBetweenRaM Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SwitchWeaponBetweenRaM();
            }

            return _instance;
        }
    }

    private const float SwapReloadTime = 0.5f;
    private Transform _meleeWeapon;
    private Transform _rangeWeapon;

    private GameObject[] _hands;

    private GameObject _currentPickedWeapon;
    private CurrentWeaponsSO _currentWeaponsSO;

    private bool _isMeleeWeapon = true;
    private bool _isRolling;
    private float _timerToSwapWeapon = 0f;

    public void Initialize(Transform meleeWeapon, Transform rangeWeapon,GameObject[] hands, CurrentWeaponsSO currentWeaponsSO)
    {
        _meleeWeapon = meleeWeapon;
        _rangeWeapon = rangeWeapon;
        _hands = hands;
        _currentWeaponsSO = currentWeaponsSO;
        
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
            hand.SetActive(isActive);
        }
    }

    public void Dispose()
    {
        PlayerAnimator.OnPlayerRolling -= SetRollState;
    }
}