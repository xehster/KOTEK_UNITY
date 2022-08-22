using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private MovementState state;
    private bool isAnimationStarting;
    private Rigidbody2D rb;
    private Animator anim;
    private float dirX;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private float timer;
    private float shootTime = 0.05f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private GameObject interactIcon;
    private enum MovementState { idle, running, jumping, falling, shooting }
    private Vector2 boxSize = new Vector2(0.1f, 1f);
    
    

    private bool facingRight = true;
    [SerializeField] private AudioSource jumpSoundEffect;
        
    
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
        dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        
        if (Input.GetButtonDown("Jump") && IsOnGround())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        ShootAnimationTimer();
        UpdateAnimation();

        if (Input.GetKeyUp(KeyCode.E))
        {
            CheckInteraction();
        }
    }

    private void ShootAnimationTimer()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            isAnimationStarting = false;
        }
    }
    

    private void UpdateAnimation()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            state = MovementState.shooting;
            anim.SetInteger("state", (int)state);
            isAnimationStarting = true;
            timer = shootTime;
        }

        if (isAnimationStarting)
        {
            return;
        }
        
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

    private bool IsOnGround()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
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