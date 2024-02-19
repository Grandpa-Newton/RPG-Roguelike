using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Components")] private PlayerController _player;
    [SerializeField] private EnemySO enemySO;
    [SerializeField] private LayerMask hittable;
    private Rigidbody2D _rigidbody2D;
    private Vector2 moveDirection;

    [Header("Stats")] [SerializeField] private float health;
    [SerializeField] private float speed;

    [Header("Knockback Parameters")] [SerializeField]
    private float knockbackDuration;

    [SerializeField] private float knockbackPower;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        CopyStatsFromSO();
        _player = FindObjectOfType<PlayerController>();
        Initialize(_player);
    }

    public void Initialize(PlayerController playerController)
    {
        _player = playerController;
    }

    private void Update()
    {
        if (_player)
        {
            Vector3 direction = (_player.transform.position - transform.position).normalized;
            moveDirection = direction;
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(moveDirection.x, moveDirection.y) * enemySO.speed;
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
            Health health = other.gameObject.GetComponent<Health>();
            if (health)
            {
                Debug.Log("I hit player:" + gameObject.name);
                StartCoroutine(_player.Knockback(knockbackDuration, knockbackPower, this.transform));
                health.Reduce(enemySO.damage);
            }
        }
    }

    private void CopyStatsFromSO()
    {
        health = enemySO.health;
        speed = enemySO.speed;
    }
}