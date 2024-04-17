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
        [SerializeField] private NavMeshAgent navMeshAgent;
        private Vector2 _moveDirection;
    

        [SerializeField] private float health;
        [SerializeField] private float speed;


        [SerializeField] private float knockbackDuration;
        [SerializeField] private float knockbackPower;

        public  UnityEvent<GameObject> OnHitWithReference;
        public  UnityEvent<GameObject> OnDeathWithReference;
        
        
        public event Action OnEnemyDie;
        
        private void Start()
        {
            InitializeStatsFromSO();
        }

        public void TakeDamage(float damage, GameObject damageSender)
        {
            
            health -= damage;
            if (health > 0)
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

 

        private void InitializeStatsFromSO()
        {
            health = enemySo.health;
            navMeshAgent.speed = enemySo.speed;
            //speed = enemySO.speed;
        }
    }
}