using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const int nonJumpOffLayer = 8;
    public Transform attackPoint;
    public float attackRange = 3f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private GameObject interactIcon;
    [SerializeField] private Weapon weapon;
    [SerializeField] private UI_Inventory uiInventory;
    [SerializeField] private HideAndShowInventory hideAndShowInventory;
    private MovementState state;
    private bool isAnimationStarting;
    private Rigidbody2D rb;
    private Animator anim;
    private float dirX;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private float timer;
    private float shootTime = 0.5f;
    private float meleeTime = 0.4f;
    private const float axisDeadZone = 0.2f;
    private enum MovementState { idle, running, jumping, falling, shooting, attack }
    private Vector2 boxSize = new Vector2(0.1f, 1f);
    public LayerMask enemyLayers;
    private Inventory inventory;
    private Collision2D currentCollision;
    private bool jumpOffCoroutineIsRunning;
    private bool facingRight = true;
    public float speedMultiplier = 1;



    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        interactIcon.SetActive(false);
    }

    // Update is called once per frame
    
    void Update()
    {
        Movement();
        
        if (Input.GetButtonDown("Jump") && IsOnGround())
        {
            Jump();
        }

        if (Input.GetAxis("Vertical") < -axisDeadZone)
        {
            JumpOff();
        }
        
        if (Input.GetKeyUp(KeyCode.E))
        {
            CheckInteraction();
        }

        ShootAnimationTimer();
        UpdateAnimation();

        HideShowInventoryPanel();


    }

    private void Movement()
    {
        dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        if (IsOnGround() && rb.velocity.magnitude > 0)
        {
            anim.SetFloat("moveSpeed", rb.velocity.magnitude);
            PlayerManager.Instance.PlayerSounds.Walking(true, rb.velocity.magnitude * speedMultiplier);
        }
        else
        {
            PlayerManager.Instance.PlayerSounds.Walking(false, rb.velocity.magnitude * speedMultiplier);
        }
    }

    private void Jump()
    {
        PlayerManager.Instance.PlayerSounds.Jump();
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void JumpOff()
    {
        if (currentCollision == null) return;
        if (!jumpOffCoroutineIsRunning && currentCollision.gameObject.layer != nonJumpOffLayer)
        {
            Debug.Log("Jump Off");
            StartCoroutine(JumpOffWithTimer(currentCollision.collider));
        }
    }



    private IEnumerator JumpOffWithTimer(Collider2D collider)
    {
        jumpOffCoroutineIsRunning = true;
        Physics2D.IgnoreCollision(collider, coll, true);
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreCollision(collider, coll, false);
        currentCollision = null;
        jumpOffCoroutineIsRunning = false;
    }
    
    private void Awake()
    {
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
        hideAndShowInventory.hideInventoryPanel();
    }

    private void HideShowInventoryPanel()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (hideAndShowInventory.isVisible)
            {
                hideAndShowInventory.hideInventoryPanel();
            }
            else
            {
                hideAndShowInventory.showInventoryPanel();
            }

        }
    }
    
    
    

    private void OnTriggerEnter2D(Collider2D collider)
    {
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            //touching item
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
            uiInventory.SetInventory(inventory);
        }
    }

    private void ShootAnimationTimer()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            isAnimationStarting = false;
            anim.SetBool("isAttack", false);
        }
    }


    

    private void UpdateAnimation()
    {
        if (Input.GetButtonDown("Fire1") && timer <= 0)
        {
            PlayerShoot();
        }
        
        if (Input.GetButtonDown("Fire3") && timer <= 0)
        {
            PlayerAttack();
        }

        if (isAnimationStarting)
        {
            return;
        }
        
        PlayerMove();
    }
    
    private void SetMovementState(MovementState newState)
    {
        state = newState;
        anim.SetInteger("state", (int)state);
    }
    
    private bool IsOnGround()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        currentCollision = other;
    }

    private void PlayerShoot()
    {
        if (PlayerManager.Instance.itemCollector.kittensouls > 0)
        {
            SetMovementState(MovementState.shooting);
            anim.Play("Player_Shoot");
            anim.SetBool("isAttack", true );
            weapon.Shoot();
            PlayerManager.Instance.itemCollector.KittenSoulDecrease();
            isAnimationStarting = true;
            timer = shootTime;

        }
    }

    private void PlayerMelee()
    {
        SetMovementState(MovementState.attack);
        PlayerManager.Instance.PlayerSounds.Melee();
        isAnimationStarting = true;
        timer = meleeTime;
    }
    
    private void PlayerAttack()
    {
        //anim
        PlayerMelee();
        //detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        //damage
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyLife>().DecreaseHealth();
        }
    }

    private void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void PlayerMove()
    {
        if (dirX > 0f)
        {
            state = MovementState.running;
            if (facingRight == false)
            {
                facingRight = true;
                transform.Rotate(0f, 180f, 0f);
            }
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            if (facingRight == true)
            {
                transform.Rotate(0f, 180f, 0f);
                facingRight = false;  
            }
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }
        
        anim.SetInteger("state", (int)state);
    }
    
    public void OpenInteractableIcon()
    {
        interactIcon.SetActive(true);
    }
    
    public void CloseInteractableIcon()
    {
        interactIcon.SetActive(false);
    }

    private void CheckInteraction()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, 0, Vector2.zero);
        if (hits.Length > 0)
        {
            foreach (RaycastHit2D rc in hits)
            {
                if(rc.transform.GetComponent<Interactable>())
                {
                    rc.transform.GetComponent<Interactable>().Interact();
                    return;
                }
            }
        }
    }
    
}
