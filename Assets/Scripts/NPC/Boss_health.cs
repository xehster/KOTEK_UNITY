using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class Boss_health : MonoBehaviour
{
   public int health = 20;
   public GameObject deathEffect;
   public bool isInvulnerable = false;

   public void TakeDamage(int damage)
   {
      if (isInvulnerable)
         return;

      health -= damage;

      if (health <= 0)
      {
         Die();
      }
   }

   void Die()
   {
      Instantiate(deathEffect, transform.position, Quaternion.identity);
      Destroy(gameObject);
   }
}
