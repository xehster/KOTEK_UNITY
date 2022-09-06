using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = System.Random;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] public int enemyHealthPoints;
    [SerializeField] private Collider2D enemyCollider;
    [SerializeField] private GameObject drop;
    private Random rnd = new Random();
    

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Projectile"))
        {
            DecreaseHealth();
        }
    }
    public virtual void DecreaseHealth()
    {
        enemyHealthPoints = enemyHealthPoints - 1;
        if (enemyHealthPoints < 1)
        {
            Die();
            Drop();
        }
    }

    public virtual void Die()
    {
        var animator = GetComponent<Animator>();
        animator.SetBool("isDead", true);

        enemyCollider.enabled = false;
        GetComponent<WaypointFollower>().enabled = false;
        StartCoroutine(DissapearBody());
    }

    IEnumerator DissapearBody()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    private void Drop()
    {
        int dropChance = rnd.Next(1, 101);
        Debug.Log(dropChance);
        if (dropChance > 50)
        {
            Instantiate(drop, transform.position, drop.transform.rotation);
        }
    }
}
