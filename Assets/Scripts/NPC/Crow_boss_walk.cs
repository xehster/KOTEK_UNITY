using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crow_boss_walk : StateMachineBehaviour
{
    private Transform player;
    private Rigidbody2D rb;
    private Crow_boss boss;

    public float speed = 2.5f;
    public float attackRange = 2f;
    
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Crow_boss>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    { 
       boss.LookAtPlayer(); 
        
       Vector2 target = new Vector2(player.position.x, rb.position.y);
       Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
       rb.MovePosition(newPos);

       if (Vector2.Distance(player.position, rb.position) <= attackRange)
       {
           //Attack
           animator.SetTrigger("Attack");
       }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }

}
