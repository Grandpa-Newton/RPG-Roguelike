using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeaponBetweenRangeAndMelee : MonoBehaviour
{
  [SerializeField] private Transform meleeWeapon;
  [SerializeField] private Transform rangeWeapon;

  private bool isMeleeWeapon;

  private void Start()
  {
    meleeWeapon.gameObject.SetActive(true);
    rangeWeapon.gameObject.SetActive(false);
    
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Mouse1))
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
}
