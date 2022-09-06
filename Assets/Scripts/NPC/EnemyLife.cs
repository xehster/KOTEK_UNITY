using System.Collections;
using UnityEngine;
using Random = System.Random;

public class EnemyLife : MonoBehaviour
{
    private const string IsDeadAnimationBool = "isDead";
    private const string ProjectileTag = "Projectile";
    [SerializeField] public int enemyHealthPoints;
    private Collider2D enemyCollider;
    [SerializeField] private GameObject drop;
    private Animator animator;
    private Random rnd = new Random();
    private WaypointFollower waypointFollower;
    

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag(ProjectileTag))
        {
            DecreaseHealth();
        }
    }

    private void Awake()
    {
        waypointFollower = GetComponent<WaypointFollower>();
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();
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
        animator.SetBool("IsDead", true);
        enemyCollider.enabled = false;
        if (waypointFollower != null)
        {
            waypointFollower.enabled = false;
        }
        StartCoroutine(DissapearBody());
    }

    IEnumerator DissapearBody()
    {
        yield return new WaitForSeconds(1f);
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
