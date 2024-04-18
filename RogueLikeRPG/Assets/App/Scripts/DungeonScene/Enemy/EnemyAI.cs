using System;
using App.Scripts.GameScenes.Player;
using App.Scripts.MixedScenes.Player.Control;
using UnityEngine;
using UnityEngine.AI;

namespace App.Scripts.DungeonScene.Enemy
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private float distanceToPlayer;
        [SerializeField] private EnemySO enemySo;
        private NavMeshAgent _navMeshAgent;
        private PlayerController _player;
      

        
        public event Action<Vector2> OnEnemyMovement;
        void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.updateRotation = false;
            _navMeshAgent.updateUpAxis = false;
            InitializeStatsFromSO();
            _player = FindObjectOfType<PlayerController>();
        }

        void Update()
        { 
            if (!_player)
            {
                _player = FindObjectOfType<PlayerController>();
            }

            if (_player != null && _navMeshAgent.isOnNavMesh)
            {
                Debug.Log(_navMeshAgent.isOnNavMesh + "- на навмеше?");
                Debug.Log(  _player + "- игрок?");
                if (_navMeshAgent.remainingDistance < distanceToPlayer)
                {
                    Vector3 direction = _navMeshAgent.velocity.normalized;
                    OnEnemyMovement?.Invoke(new Vector2(direction.x, direction.z));
                    _navMeshAgent.SetDestination(_player.transform.position);
                }
                else
                {
                    OnEnemyMovement?.Invoke(new Vector2(0, 0));
                    _navMeshAgent.ResetPath();
                }
            }
        }
        private void InitializeStatsFromSO()
        {
            _navMeshAgent.speed = enemySo.speed;
        }
    }
}