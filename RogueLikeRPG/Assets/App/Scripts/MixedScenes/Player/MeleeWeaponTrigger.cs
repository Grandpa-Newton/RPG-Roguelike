using System;
using System.Collections;
using System.Collections.Generic;
using App.Scripts.DungeonScene.Enemy;
using App.Scripts.MixedScenes.Weapon.MeleeWeapon;
using UnityEngine;

public class MeleeWeaponTrigger : MonoBehaviour
{
    private static MeleeWeaponTrigger _instance;

    public static MeleeWeaponTrigger Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new MeleeWeaponTrigger();
            }

            return _instance;
        }
    }
    [SerializeField] private MeleeWeaponSO _meleeWeaponSO;

    public void Initialize(MeleeWeaponSO meleeWeaponSO)
    {
        _meleeWeaponSO = meleeWeaponSO;
    }
    public void SetMeleeWeaponSO(MeleeWeaponSO meleeWeaponSO)
    {
        _meleeWeaponSO = meleeWeaponSO;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            Debug.Log("EnemyFound");
            enemy.TakeDamage(_meleeWeaponSO.damage);
        }
    }
}
