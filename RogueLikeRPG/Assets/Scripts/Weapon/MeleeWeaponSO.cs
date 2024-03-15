using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeapon_", menuName = "Weapons/Melee Weapon")]
public class MeleeWeaponSO : WeaponItemSO
{
    public override bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
    {
        AgentWeapon weaponSystem = character.GetComponent<AgentWeapon>();
        IWeapon weapon;
        
        SwitchWeaponBetweenRangeAndMelee.Instance.SetActiveMeleeWeapon();
        weapon = GameObject.Find("MeleeWeapon").GetComponent<MeleeWeapon>();
        if (weaponSystem != null)
        {
            weaponSystem.SetMeleeWeapon(this, itemState == null ? DefaultParametersList : itemState);
            weapon.SetWeapon(this);
        }

        return false;
    }
}
