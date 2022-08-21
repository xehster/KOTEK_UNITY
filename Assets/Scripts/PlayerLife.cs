using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private const int trapDamage = 1;
    private const int enemyDamage = 1;
    private const int smallHeart = 1;
    private int maxHealth;
    [SerializeField] int kotekHealthPoints;
    [SerializeField] private HealthCheck healthCheck;
    [SerializeField] private AudioSource deathSoundEffect;
    [SerializeField] private float recoveryTime = 1f;
    
    private Animator anim;
    private PlayerMovement move;
    private bool isDamaged;
    private float damageTime;
    
    

    
    private void Start()
    {
        maxHealth = kotekHealthPoints;
        move = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
        healthCheck.SetCurrentHealth(kotekHealthPoints);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Trap") && !isDamaged)
        {
            DecreaseHealth(trapDamage);
        }
        if (collider.gameObject.CompareTag("HP") && kotekHealthPoints < maxHealth)
        {
            Destroy(collider.gameObject);
            IncreaseHealth(smallHeart);
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Trap") && !isDamaged)
        {
            DecreaseHealth(trapDamage);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isDamaged)
        {
            DecreaseHealth(enemyDamage);
        } 
    }

    private void IncreaseHealth(int health)
    {
        if (health < maxHealth)
        {
            kotekHealthPoints = kotekHealthPoints + health;
            if (kotekHealthPoints > maxHealth)
            {
                kotekHealthPoints = maxHealth;
            }
            healthCheck.SetCurrentHealth(kotekHealthPoints);
        }
    }

    private void DecreaseHealth(int damage)
    {
        if (kotekHealthPoints > 0)
        {
            isDamaged = true;
            damageTime = recoveryTime;
            kotekHealthPoints = kotekHealthPoints - damage;
            healthCheck.SetCurrentHealth(kotekHealthPoints);
            if (kotekHealthPoints < 1)
            {
                Die();
            }
        }
    }

    private void DeathTimer()
    {
        if (isDamaged)
        {
            damageTime -= Time.deltaTime;
            if (damageTime <= 0)
            {
                isDamaged = false;
            }
        }
    }

    private void Update()
    {
        DeathTimer();
    }

    private void Die()
    {
        move.enabled = false;
        anim.SetTrigger("death");
        deathSoundEffect.Play();
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
    }

