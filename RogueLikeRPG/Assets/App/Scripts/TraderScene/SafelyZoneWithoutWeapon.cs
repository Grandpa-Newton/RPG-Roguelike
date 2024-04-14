using System;
using System.Collections;
using System.Collections.Generic;
using App.Scripts.GameScenes.Player;
using App.Scripts.GameScenes.Player.Components;
using UnityEngine;

public class SafelyZoneWithoutWeapon : MonoBehaviour
{
    [SerializeField] private GameObject playerWeapon;

    private GameObject _player;

    private bool _isHandsActive;
    
    private void Start()
    {
        _player = GameObject.Find("Player");
        if (_player)
        {
            Debug.Log("Player found");
        }
        
        //  PlayerWeaponSwitcher.Instance.WeaponAndHandsVisibility(_isHandsActive);
    }
    
    
}
