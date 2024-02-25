using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : MonoBehaviour, IWeapon
{
    private RangeWeaponSO _rangeWeaponData;
    public WeaponSO WeaponData => _rangeWeaponData;
    
    private float _timeToNextShot = 0;
    private PlayerInputActions _playerInputActions;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Transform bulletsContainer;
    private Transform _aimTransform;
    
    public RangeWeapon(RangeWeaponSO data)
    {
        _rangeWeaponData = data;
    }

    
    
    public void DealDamage()
    {
        _timeToNextShot += Time.deltaTime;
        if (_playerInputActions.Player.Attack.IsPressed() && _timeToNextShot > _rangeWeaponData.attackRate)
        {

            Bullet bullet = Instantiate(_rangeWeaponData.bullet, shootPoint.position, shootPoint.rotation);
            if (bullet)
            {
                bullet.transform.SetParent(bulletsContainer, true);
                _bulletSo = bullet.GetComponent<Bullet>().GetBulletSO();
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(_aimTransform.right * _bulletSo.speed, ForceMode2D.Impulse);
                
                bullet.SetRangeWeaponSO(_rangeWeaponData);
            }
            _timeToNextShot = 0f;
        }
    }
}