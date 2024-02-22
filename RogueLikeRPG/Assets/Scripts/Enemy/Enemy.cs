using Interfaces;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    #region Components

    [SerializeField] private EnemySO enemySO;
    [SerializeField] private LayerMask hittable;
    [SerializeField] private NavMeshAgent navMeshAgent;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _moveDirection;

    #endregion

    #region Stats

    [SerializeField] private float health;
    [SerializeField] private float speed;

    #endregion

    #region Knockback Parameters

    [SerializeField] private float knockbackDuration;
    [SerializeField] private float knockbackPower;

    #endregion

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
                Debug.Log("I hit player: " + gameObject.name);
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