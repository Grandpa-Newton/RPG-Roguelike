using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemySO enemySO;
    [SerializeField] private PlayerController player;
    private Rigidbody2D _rigidbody2D;

    private Vector2 moveDirection;
    
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindObjectOfType<PlayerController>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
    }

    private void Update()
    {
        if (player != null)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            moveDirection = direction;
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(moveDirection.x, moveDirection.y) * enemySO.speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}