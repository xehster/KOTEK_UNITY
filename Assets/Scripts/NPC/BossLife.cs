using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossLife : EnemyLife
{
    [SerializeField] private Rigidbody2D rigidbody;
    public UnityEvent action;

    public override void Die()
    {
        rigidbody.isKinematic = true;
        action?.Invoke();
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
