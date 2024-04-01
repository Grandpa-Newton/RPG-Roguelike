using System;
using System.Collections;
using System.Collections.Generic;
using App.Scripts.AllScenes.Interfaces;
using App.Scripts.MixedScenes.Weapon.RangeWeapon;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private BulletSO _bulletSo;
    private SpriteRenderer _spriteRenderer;
    private RangeWeaponSO _rangeWeaponSo;
    
    private RangeWeapon _rangeWeapon;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        StartCoroutine(DestroyBullet());
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(_bulletSo.lifeTime);
        if (gameObject != null)
        {
            //bulletParticleTrail.Stop();
            Destroy(gameObject);
        }
    }

    public BulletSO GetBulletSO()
    {
        return _bulletSo;
    }
    public void SetBulletSO(BulletSO bulletSO)
    {
        _bulletSo = bulletSO;
        _spriteRenderer.sprite = bulletSO.departingBulletSprite;
  
    }

    public void SetRangeWeaponSO(RangeWeaponSO rangeWeaponSO)
    {
        _rangeWeaponSo = rangeWeaponSO;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(_rangeWeaponSo.damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
   


}