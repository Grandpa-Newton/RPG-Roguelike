using System;
using App.Scripts.DungeonScene.Enemy;
using App.Scripts.GameScenes.Weapon;
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

        [SerializeField] private MeleeWeaponSO _meleeWeaponSO;
        [SerializeField] private Transform _circleOrigin;
        [SerializeField] private float _radius = 0.55f;
        [SerializeField] private GameObject player;


        public void DetectColliders()
        {
            _meleeWeaponSO = PlayerCurrentWeapon.Instance.CurrentMeleeAndRangeWeaponsSO.EquippedMeleeWeapon;
            foreach (Collider2D enemyCollider in Physics2D.OverlapCircleAll(_circleOrigin.position, _radius))
            {
                if (enemyCollider.gameObject.TryGetComponent(out Enemy enemy))
                {
                    Debug.Log(enemyCollider.name);
                    enemy.TakeDamage(_meleeWeaponSO.damage,player);
                    Debug.Log(_meleeWeaponSO.damage + "dmg");
                }
            }
        }

        public void SetMeleeWeaponSO(MeleeWeaponSO weaponSO)
        {
            _meleeWeaponSO = weaponSO;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(_meleeWeaponSO.damage,player);
            }
        }
    }
}