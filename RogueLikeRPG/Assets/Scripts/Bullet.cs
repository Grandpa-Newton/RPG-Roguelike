using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletSO bulletSO;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = bulletSO.bulletSprite;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(bulletSO.damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}