using System;
using App.Scripts.MixedScenes.Weapon.RangeWeapon;
using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    [SerializeField] private Transform bulletsContainer;
    [SerializeField] private Transform shootPoint;
   
    public event Action OnProportiesSet;
    
    public Bullet CreateBullet(Bullet bulletPrefab, RangeWeaponSO rangeWeaponSO, AudioSource audioSource, Transform aimTransform)
    {
        Bullet bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        if (!bullet) return bullet;
        
        audioSource.PlayOneShot(rangeWeaponSO.weaponAttackSound);
        SetBulletParameters(bullet, rangeWeaponSO);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(aimTransform.right * rangeWeaponSO.bulletSpeed, ForceMode2D.Impulse);
        return bullet;
    }
    
    private void SetBulletParameters(Bullet bullet, RangeWeaponSO rangeWeaponSO)
    {
        bullet.SetRangeWeaponSO(rangeWeaponSO);
        bullet.SetBulletSO(rangeWeaponSO.bulletSO);
        bullet.transform.SetParent(bulletsContainer, true);

        OnProportiesSet?.Invoke();
    }
}
