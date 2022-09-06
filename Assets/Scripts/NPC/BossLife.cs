using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLife : EnemyLife
{
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private GameObject deathEffect;

    public override void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        rigidbody.isKinematic = true;
        base.Die();
    }

    public override void DecreaseHealth()
    {
        base.DecreaseHealth();
        if (enemyHealthPoints < 5)
        {
            GetComponent<Animator>().SetBool("isEnraged", true);
        }
    }
}
