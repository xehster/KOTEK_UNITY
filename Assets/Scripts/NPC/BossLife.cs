using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLife : EnemyLife
{
    [SerializeField] private Rigidbody2D rigidbody;

    public override void Die()
    {
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
