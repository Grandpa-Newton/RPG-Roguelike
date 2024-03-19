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
    private TrailRenderer _trailRenderer;
    private RangeWeaponSO _rangeWeaponSo;
    //private ParticleSystem bulletParticleTrail;
    
    private RangeWeapon _rangeWeapon;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _trailRenderer = gameObject.GetComponent<TrailRenderer>(); 
        
        
        //_rangeWeapon.OnProportiesSet += RangeWeaponOnProportiesSet;
    }

    private void RangeWeaponOnProportiesSet()
    {
        _spriteRenderer.sprite = _bulletSo.departingBulletSprite;
        _trailRenderer = _bulletSo.trailRenderer;
        //SetTrailRenderer(_bulletSo.trailRenderer);
    }

    /*private void SetTrailRenderer(TrailRenderer trailSettings)
    {
        _trailRenderer = gameObject.AddComponent<TrailRenderer>();
        // _trailRenderer = trailSettings;
        _trailRenderer.time = trailSettings.time;
        _trailRenderer.startWidth = trailSettings.startWidth;
        _trailRenderer.endWidth = trailSettings.endWidth;
        _trailRenderer.colorGradient = trailSettings.colorGradient;
        _trailRenderer.sortingOrder = trailSettings.sortingOrder;
        _trailRenderer.materials[0] = trailSettings.materials[0];
    }*/

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
        this._bulletSo = bulletSO;
        _spriteRenderer.sprite = bulletSO.departingBulletSprite;
        _trailRenderer = bulletSO.trailRenderer;
        
        //bulletParticleTrail = Instantiate(bulletSO.bulletTrailParticle, transform.position, Quaternion.identity);
        //bulletParticleTrail.transform.parent = transform;
        //bulletParticleTrail.Play();
    }

    public void SetRangeWeaponSO(RangeWeaponSO rangeWeaponSO)
    {
        _rangeWeaponSo = rangeWeaponSO;
    }
    public void SetRangeWeapon(RangeWeapon rangeWeapon)
    {
        _rangeWeapon = rangeWeapon;
        _rangeWeapon.OnProportiesSet += RangeWeaponOnProportiesSet;
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
    private void OnDestroy()
    {
        if (_rangeWeapon != null)
        {
            _rangeWeapon.OnProportiesSet -= RangeWeaponOnProportiesSet;
        }
    }


}