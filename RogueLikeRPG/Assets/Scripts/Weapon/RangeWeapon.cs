using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : MonoBehaviour
{
    [SerializeField] private RangeWeaponSO rangeWeaponSO;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private RangeWeaponSO next_weapon;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = rangeWeaponSO.weaponSprite;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //rangeWeaponSO.damage = next_weapon.damage;
            //rangeWeaponSO.fireRate = next_weapon.fireRate;
            //rangeWeaponSO.weaponSprite = next_weapon.weaponSprite;
            Debug.Log(rangeWeaponSO.damage);
            Debug.Log(rangeWeaponSO.fireRate);
            Debug.Log(rangeWeaponSO.weaponSprite);
            Debug.Log(rangeWeaponSO.bullet);
        }
    }

    public RangeWeaponSO GetRangeWeaponSO()
    {
        return rangeWeaponSO;
    }
}