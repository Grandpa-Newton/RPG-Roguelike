using System;
using App.Scripts.DungeonScene.Enemy;
using App.Scripts.GameScenes.Weapon.MeleeWeapon;
using UnityEngine;
using UnityEngine.Serialization;

namespace App.Scripts.GameScenes.Player
{
    public class MeleeWeaponTrigger : MonoBehaviour
    {
        public static MeleeWeaponTrigger Instance { get; private set; }

        [SerializeField] private MeleeWeaponSO meleeWeaponSO;

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            }
        }

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