using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponSO : ScriptableObject
{
    [Range(0.01f, 10)] public float attackRate;
    [Range(0, 100)] public float damage;
    
    public Sprite weaponSprite;
    public AudioClip weaponAttackSound;
    public string weaponName;
    public string weaponDescription;

    public void SetWeapon()
    {
        
    }
    
}
