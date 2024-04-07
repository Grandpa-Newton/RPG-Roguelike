using System.Collections;
using App.Scripts.AllScenes.Interfaces;
using App.Scripts.GameScenes.Weapon.RangeWeapon;
using UnityEngine;

namespace App.Scripts.GameScenes.Weapon.Bullet
{
    public class Bullet : MonoBehaviour
    {
        private BulletSO _bulletSo;
        private SpriteRenderer _spriteRenderer;
        private RangeWeaponSO _rangeWeaponSo;
        

        private RangeWeapon.RangeWeapon _rangeWeapon;
        [SerializeField] private LayerMask layerMask;

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
            if ((layerMask.value & (1 << other.gameObject.layer)) != 0)
            {
                Instantiate(_bulletSo.explosiveParticle, transform.position, Quaternion.identity);
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
    }
}