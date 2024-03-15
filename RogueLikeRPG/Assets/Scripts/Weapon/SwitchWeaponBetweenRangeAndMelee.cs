using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeaponBetweenRangeAndMelee : MonoBehaviour
{
    [SerializeField] private Transform meleeWeapon;
    [SerializeField] private Transform rangeWeapon;

    [SerializeField] private GameObject[] hands;

    private bool isMeleeWeapon = true;

    private void Start()
    {
        meleeWeapon.gameObject.SetActive(true);
        rangeWeapon.gameObject.SetActive(false);
        PlayerHandsVisible(false);
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            isMeleeWeapon = !isMeleeWeapon;
            if (isMeleeWeapon)
            {
                meleeWeapon.gameObject.SetActive(true);
                rangeWeapon.gameObject.SetActive(false);
            }
            else
            {
                meleeWeapon.gameObject.SetActive(false);
                rangeWeapon.gameObject.SetActive(true);
            }
        }
    }

    public void PlayerHandsVisible(bool isActive)
    {
        foreach (var hand in hands)
        {
            hand.SetActive(isActive);
        }
    }
}