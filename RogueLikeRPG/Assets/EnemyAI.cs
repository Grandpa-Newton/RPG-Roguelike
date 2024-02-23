using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private PlayerController player;
    private NavMeshAgent _agent;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    { 
        if (player == null)
        {
            player = FindObjectOfType<PlayerController>();
        }

        if (player != null && _agent.isOnNavMesh)
        {
            /*float distanceBetweenObjects = Vector3.Distance(transform.position, player.transform.position);
            if (distanceBetweenObjects < 1)
            {
                Vector3 direction = (player.transform.position - transform.position).normalized;
                _rigidbody2D.velocity = direction * _agent.speed;
            }*/
            //else
            //{
                _agent.SetDestination(player.transform.position);
            //}
        }
    }
}