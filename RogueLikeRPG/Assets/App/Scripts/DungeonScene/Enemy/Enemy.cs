using System;
using App.Scripts.AllScenes.Interfaces;
using App.Scripts.GameScenes.Player;
using App.Scripts.GameScenes.Player.Components;
using App.Scripts.GameScenes.Player.EditableValues;
using App.Scripts.MixedScenes;
using App.Scripts.MixedScenes.Player;
using App.Scripts.MixedScenes.Player.Control;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace App.Scripts.DungeonScene.Enemy
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private CharacteristicValueSO characteristicValueSO;

        [SerializeField] private SpriteRenderer enemyGFX;
        [SerializeField] private EnemySO enemySo;
        [SerializeField] private LayerMask hittable;

        public  UnityEvent<GameObject> OnHitWithReference;
        public  UnityEvent<GameObject> OnDeathWithReference;

        private float _health;

        private void Start()
        {
            _health = enemySo.health;
        }

        public event Action OnEnemyDie;

        public void TakeDamage(float damage, GameObject damageSender)
        {
            _health -= damage;
            if (_health > 0)
            {
                OnHitWithReference?.Invoke(damageSender);
            }
            else 
            {
                OnDeathWithReference?.Invoke(damageSender);
                enemyGFX.GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<CapsuleCollider2D>().enabled = false;
                OnEnemyDie?.Invoke();
            }
        }
    

        private void OnCollisionEnter2D(Collision2D other)
        {
            if ((hittable & 1 << other.gameObject.layer) != 0)
            {
                if (other.gameObject.TryGetComponent(out PlayerController player))
                {
                    PlayerHealth.Instance.ReduceHealth(enemySo.damage);
                }
            }
        }
    }
}