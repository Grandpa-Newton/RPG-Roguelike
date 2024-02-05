using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   [SerializeField] private float speed;
   [SerializeField] private float lifeTime;
   [SerializeField] private float distance;
   [SerializeField] private float damage;
   [SerializeField] private LayerMask whatIsSolid;

   private Rigidbody2D _rigidbody2D;
   private Vector2 direction;

   public void SetDirection(Vector2 direction)
   {
      this.direction = direction;
   }

   private void Awake()
   {
      _rigidbody2D = GetComponent<Rigidbody2D>();
   }

   private void Update()
   {
      RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direction, distance, whatIsSolid);
      if (hitInfo.collider != null)
      {
         if (hitInfo.collider.CompareTag("Enemy"))
         {
            hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
         }
         Destroy(gameObject);
      }
      transform.Translate(direction * speed * Time.deltaTime);
   }
}

