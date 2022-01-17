using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamMan_walk : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    IceCreamManAIControl iceCreamMan;

    public float speed = 2.5f;
    float nextLaunchTime;
    public float launchProjectileDelay = 5f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = FindObjectOfType<PlayerHealthStatus>().transform;
        rb = animator.GetComponent<Rigidbody2D>();
        iceCreamMan = animator.GetComponent<IceCreamManAIControl>();
        iceCreamMan.isVulnerable = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        iceCreamMan.LookAtPlayer();

        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, Time.fixedDeltaTime * speed);
        rb.MovePosition(newPos);

        //if (Time.time >= nextLaunchTime)
        //{
        //    iceCreamMan.LaunchProjectile();
        //    nextLaunchTime = Time.time + launchProjectileDelay;
        //}
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
