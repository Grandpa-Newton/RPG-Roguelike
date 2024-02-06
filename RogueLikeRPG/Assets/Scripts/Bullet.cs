using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   [SerializeField] private BulletSO bulletSO;

   public BulletSO GetBulletSO()
   {
      return bulletSO;
   }
   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Enemy"))
      {
         other.GetComponent<Enemy>().TakeDamage(bulletSO.damage);
         Destroy(gameObject);
      }
      else
      {
         Destroy(gameObject);
      }
   }
   /*private void Update()
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
   }*/
}

