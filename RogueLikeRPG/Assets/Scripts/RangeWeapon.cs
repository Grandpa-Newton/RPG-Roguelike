using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : MonoBehaviour
{
    [SerializeField] private RangeWeaponSO rangeWeaponSO;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = rangeWeaponSO.weaponSprite;
    }

    public RangeWeaponSO GetRangeWeaponSO()
    {
        return rangeWeaponSO;
    }
}
