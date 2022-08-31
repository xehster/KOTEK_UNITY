using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = System.Random;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private int enemyHealthPoints;
    [SerializeField] private GameObject drop;
    [SerializeField] private GameObject blood;
    [SerializeField] private GameObject explosion;
    [SerializeField] private Collider2D collider;
    private Random rnd = new Random();

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Projectile"))
        {
            if (blood != null)
            {
                var bloodObject = Instantiate(blood, transform.position, quaternion.identity);
                bloodObject.transform.parent = transform;
                bloodObject.transform.LookAt(collider.gameObject.transform);
                StartCoroutine(DestroyBlood(bloodObject));  
            }
            DecreaseHealth();
        }
    }

    IEnumerator DestroyBlood(GameObject destroyObject)
    {
        yield return new WaitForSeconds(1f);
        Destroy(destroyObject);
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
        if (explosion != null)
        {
            var bloodObject = Instantiate(explosion, transform.position, quaternion.identity);
        }

        var animator = GetComponent<Animator>();
        animator.SetBool("isDead", true);

        collider.enabled = false;
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
        if (dropChance > 70)
        {
            Instantiate(drop, transform.position, drop.transform.rotation);
        }

    }
}
