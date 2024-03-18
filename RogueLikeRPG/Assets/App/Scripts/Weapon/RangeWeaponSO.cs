using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "RangeWeapon_", menuName = "Weapons/Range Weapon")]
public class RangeWeaponSO : WeaponItemSO
{
    public BulletSO bulletSO;
    public float bulletSpeed;
    public uint magSize;
    public float reloadTime;


    public override bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
    {
        AgentWeapon weaponSystem = character.GetComponent<AgentWeapon>();
        Weapon weapon;
        
        SwitchWeaponBetweenRangeAndMelee.Instance.SetActiveRangeWeapon();
        weapon = GameObject.Find("RangeWeapon").GetComponent<RangeWeapon>();
        if (weaponSystem != null)
        {
            weaponSystem.SetRangeWeapon(this, itemState == null ? DefaultParametersList : itemState);
            weapon.SetWeapon(this);
        }
        return false;
    }
}
