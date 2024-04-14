using System;
using App.Scripts.DungeonScene.Enemy;
using App.Scripts.GameScenes.Weapon.MeleeWeapon;
using UnityEngine;
using UnityEngine.Serialization;

namespace App.Scripts.GameScenes.Player
{
    public class MeleeWeaponTrigger : MonoBehaviour
    {
        private static MeleeWeaponTrigger _instance;
        public static MeleeWeaponTrigger Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<MeleeWeaponTrigger>();
                }
                return _instance;
            }
        }

        [SerializeField] private MeleeWeaponSO meleeWeaponSO;

        

        public void SetMeleeWeaponSO(MeleeWeaponSO weaponSO)
        {
            meleeWeaponSO = weaponSO;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Enemy enemy))
            {
                Debug.Log("EnemyFound");
                enemy.TakeDamage(meleeWeaponSO.damage);
            }
        }
    }
}