using App.Scripts.DungeonScene.Enemy;
using App.Scripts.GameScenes.Weapon;
using App.Scripts.GameScenes.Weapon.MeleeWeapon;
using UnityEngine;

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
        [SerializeField] private Transform circleOrigin;
        [SerializeField] private float radius;
        [SerializeField] private GameObject player;


        public void DetectColliders()
        {
            meleeWeaponSO = PlayerCurrentWeapon.Instance.CurrentMeleeAndRangeWeaponsSO.EquippedMeleeWeapon;
            foreach (Collider2D enemyCollider in Physics2D.OverlapCircleAll(circleOrigin.position, radius))
            {
                if (enemyCollider.gameObject.TryGetComponent(out Enemy enemy))
                {
                    Debug.Log(enemyCollider.name);
                    enemy.TakeDamage(meleeWeaponSO.damage,player);
                    Debug.Log(meleeWeaponSO.damage + "dmg");
                }
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
                enemy.TakeDamage(meleeWeaponSO.damage,player);
            }
        }
    }
}