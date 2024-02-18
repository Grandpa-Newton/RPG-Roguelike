using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletSO bulletSO;
    private SpriteRenderer _spriteRenderer;
    private RangeWeaponSO _rangeWeaponSo;

    public event Action OnTakeDamage;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = bulletSO.departingBulletSprite;
    }

    private void Start()
    {
        StartCoroutine(DestroyBullet());
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(bulletSO.lifeTime);
        if (gameObject != null)
            Destroy(gameObject);
    }

    public BulletSO GetBulletSO()
    {
        return bulletSO;
    }

    public void SetRangeWeaponSO(RangeWeaponSO rangeWeaponSO)
    {
        this._rangeWeaponSo = rangeWeaponSO;
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