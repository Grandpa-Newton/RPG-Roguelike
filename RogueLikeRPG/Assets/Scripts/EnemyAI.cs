using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float distanceToPlayer;
    private PlayerController player;
    private NavMeshAgent _agent;
    public event Action<Vector2> OnEnemyMovement;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    { 
        if (!player)
        {
            player = FindObjectOfType<PlayerController>();
        }

        if (player != null && _agent.isOnNavMesh)
        {
            _agent.SetDestination(player.transform.position);
            if (_agent.remainingDistance < distanceToPlayer)
            {
                Vector3 direction = _agent.velocity.normalized;
                OnEnemyMovement?.Invoke(new Vector2(direction.x, direction.z));
                _agent.SetDestination(player.transform.position);
            }
            else
            {
                OnEnemyMovement?.Invoke(new Vector2(0, 0));
                _agent.ResetPath();
            }
        }
    }

}