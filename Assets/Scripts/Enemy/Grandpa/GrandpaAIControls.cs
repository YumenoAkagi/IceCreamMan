using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GrandpaAIControls : MonoBehaviour
{
    private static readonly string VELOCITY = "velocity";
    public Rigidbody2D target;
    public float detectionRange;
    public float movementSpeed = 2f;

    Animator animator;
    Rigidbody2D body;
    float velocity;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        velocity = -movementSpeed;
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, detectionRange);
        if(Array.Exists(cols, c => c.transform.CompareTag(target.tag))){
            ChasePlayer();
        } else
        {
            Idle();
        }
    }

    private void Idle()
    {
        body.velocity = Vector2.zero;
        animator.SetFloat(VELOCITY, 0);
    }

    private void ChasePlayer()
    {
        if(target.position.x < transform.position.x && velocity > 0.01f)
        {
            // player is on left
            Flip();
            velocity *= -1;
        }
        else if(target.position.x > transform.position.x && velocity < -0.01f)
        {
            // player is on right
            Flip();
            velocity *= -1;
        }

        animator.SetFloat(VELOCITY, movementSpeed);
        body.velocity = new Vector2(velocity, body.velocity.y);
    }

    private void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
