using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_weapon : MonoBehaviour
{
    public int attackDamage = 1;
    public int enragedAttackDamage = 2;

    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;

    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        Debug.Log(colInfo);
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerLife>().DecreaseHealth(attackDamage);
        }
    }
    
    public void enragedAttack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerLife>().DecreaseHealth(enragedAttackDamage);
        }
    }
}
