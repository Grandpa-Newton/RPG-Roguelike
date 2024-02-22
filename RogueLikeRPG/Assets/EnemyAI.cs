using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent _agent;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    void Update()
    {
        Debug.Log(_agent.isOnNavMesh);
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }

        if (player != null && _agent.isOnNavMesh)
        {
            _agent.SetDestination(player.transform.position);
        }
    }
}
