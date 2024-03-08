using Interfaces;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    
    [SerializeField] private EnemySO enemySO;
    [SerializeField] private LayerMask hittable;
    [SerializeField] private NavMeshAgent navMeshAgent;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _moveDirection;
    

    [SerializeField] private float health;
    [SerializeField] private float speed;


    [SerializeField] private float knockbackDuration;
    [SerializeField] private float knockbackPower;
    
    private PlayerController player;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        InitializeStatsFromSO();
        player = FindObjectOfType<PlayerController>();
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
                StartCoroutine(player.Knockback(knockbackDuration, knockbackPower, transform));
                healthComponent.Reduce(enemySO.damage);
            }
        }
    }

    private void InitializeStatsFromSO()
    {
        health = enemySO.health;
        navMeshAgent.speed = enemySO.speed;
        //speed = enemySO.speed;
    }
}