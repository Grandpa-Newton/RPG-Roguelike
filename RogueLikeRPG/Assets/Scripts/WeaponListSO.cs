using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponsList_", menuName = "Weapons/Weapons List")]
public class WeaponListSO : ScriptableObject
{
    public List<WeaponItemSO> weapons;
}
