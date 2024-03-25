using System;
using System.Collections;
using System.Collections.Generic;
using App.Scripts.MixedScenes.Weapon;
using UnityEngine;

public class SafelyZoneWithoutWeapon : MonoBehaviour
{
    [SerializeField] private GameObject playerWeapon;

    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
        if (player)
        {
            Debug.Log("Player found");
        }
        
        SwitchWeaponBetweenRangeAndMelee.Instance.WeaponAndHandsDisable();
    }
    
    
}
