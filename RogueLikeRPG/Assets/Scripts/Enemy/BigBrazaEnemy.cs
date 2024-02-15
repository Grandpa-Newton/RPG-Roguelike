using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBrazaEnemy : Enemy
{
   protected override void CopyStatsFromSO()
   {
      health = enemySO.health + 5;
      speed = enemySO.speed - 2;
      
   }

   public override void TakeDamage(float damage)
   {
      health -= damage / 1.5f;
      if (health <= 0)
      {
         Destroy(gameObject);
      }
   }
}
