using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] protected PlayerController player;
    [SerializeField] protected EnemySO enemySO;

    private Rigidbody2D _rigidbody2D;
    private Vector2 moveDirection;
    
    [Header("Stats")]
    [SerializeField] protected float health;
    [SerializeField] protected float speed;
    
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindObjectOfType<PlayerController>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        CopyStatsFromSO();
    }

    private void Update()
    {
        if (player)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            moveDirection = direction;
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(moveDirection.x, moveDirection.y) * enemySO.speed;
    }

    public virtual void TakeDamage(float damage)
    {
        Debug.Log(health);
        health -= damage;
        Debug.Log(health);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void CopyStatsFromSO()
    {
        health = enemySO.health;
        speed = enemySO.speed;
    }
}