using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.Serialization;

public class PlayerAimWeapon : MonoBehaviour
{
    
    private PlayerInputActions _playerInputActions;

    [SerializeField] private RangeWeapon rangeWeapon;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Transform bulletsContainer;
    
    private Transform _aimTransform;
    private Animator _aimAnimator;
    
    //Scriptable Objects
    private BulletSO _bulletSo;
    private RangeWeaponSO _rangeWeaponSo;

    private float _timeToNextShot = 0;
    
    private void Awake()
    {
        _rangeWeaponSo = rangeWeapon.GetRangeWeaponSO();
        _aimTransform = transform.Find("Aim");
        _aimAnimator = _aimTransform.GetComponent<Animator>();

        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        
        
    }

    private void Update()
    {
        HandleAiming();
        HandleShooting();
        Debug.Log(_rangeWeaponSo.fireRate);
    }

    private void HandleAiming()
    {
        Vector3 mousePosition = GetMouseWorldPosition(_playerInputActions);

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        _aimTransform.eulerAngles = new Vector3(0, 0, angle);

        Vector3 localScale = Vector3.one;
        if (angle > 90 || angle < -90)
        {
            localScale.y = -1f;
        }
        else
        {
            localScale.y = +1f;
        }

        _aimTransform.localScale = localScale;
    }
    
    private void HandleShooting()
    {
        _timeToNextShot += Time.deltaTime;
        if (_playerInputActions.Player.Attack.IsPressed() && _timeToNextShot > _rangeWeaponSo.fireRate)
        {
            _aimAnimator.SetTrigger("Shoot");

            Bullet bullet = Instantiate(_rangeWeaponSo.bullet, shootPoint.position, shootPoint.rotation);
            if (bullet)
            {
                bullet.transform.SetParent(bulletsContainer, true);
                _bulletSo = bullet.GetComponent<Bullet>().GetBulletSO();
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(_aimTransform.right * _bulletSo.speed, ForceMode2D.Impulse);
                
                bullet.SetRangeWeaponSO(_rangeWeaponSo);
            }
            _timeToNextShot = 0f;
        }
    }

    private static Vector3 GetMouseWorldPosition(PlayerInputActions playerInputActions)
    {
        Vector2 mousePosition = playerInputActions.Player.PointerPosition.ReadValue<Vector2>();
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0f;
        return worldPosition;
    }

    private static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}