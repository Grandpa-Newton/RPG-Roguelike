using Interfaces;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    #region Components

    [SerializeField] private EnemySO enemySO;
    [SerializeField] private LayerMask hittable;
    private Rigidbody2D rigidbody2D;
    private Vector2 moveDirection;

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
        rigidbody2D = GetComponent<Rigidbody2D>();
        InitializeStatsFromSO();
        player = FindObjectOfType<PlayerController>();
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
        rigidbody2D.velocity = moveDirection * speed;
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
        speed = enemySO.speed;
    }
}