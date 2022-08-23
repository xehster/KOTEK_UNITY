using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private int enemyHealthPoints;
    [SerializeField] private GameObject drop;
    private Random rnd = new Random();
    
    // Start is called before the first frame update
    void Start()
    {
        enemyHealthPoints = 3;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Projectile"))
        {
            DecreaseHealth();
        }
    }

    public void DecreaseHealth()
    {
        enemyHealthPoints = enemyHealthPoints - 1;
        if (enemyHealthPoints < 1)
        {
            Die();
            Drop();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void Drop()
    {
        int dropChance = rnd.Next(1, 101);
        Debug.Log(dropChance);
        if (dropChance > 70)
        {
            Instantiate(drop, transform.position, drop.transform.rotation);
        }

    }
}
