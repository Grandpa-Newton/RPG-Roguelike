using App.Scripts.AllScenes.Interfaces;
using App.Scripts.MixedScenes.Player;
using App.Scripts.MixedScenes.Player.Control;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace App.Scripts.DungeonScene.Enemy
{
    public class Enemy : MonoBehaviour, IDamageable
    {
    
        [FormerlySerializedAs("enemySO")] [SerializeField] private EnemySO enemySo;
        [SerializeField] private LayerMask hittable;
        [SerializeField] private NavMeshAgent navMeshAgent;
        private Vector2 _moveDirection;
    

        [SerializeField] private float health;
        [SerializeField] private float speed;


        [SerializeField] private float knockbackDuration;
        [SerializeField] private float knockbackPower;
    
        private PlayerController _player;

        private void Start()
        {
            InitializeStatsFromSO();
            _player = FindObjectOfType<PlayerController>();
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    

        private void OnCollisionEnter2D(Collision2D other)
        {
            if ((hittable & 1 << other.gameObject.layer) != 0)
            {
                if (other.gameObject.TryGetComponent(out Health healthComponent))
                {
                    StartCoroutine(_player.Knockback(knockbackDuration, knockbackPower, transform));
                    healthComponent.Reduce(enemySo.damage);
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