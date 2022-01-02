using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GrandpaAIControls : MonoBehaviour
{
    private static readonly string VELOCITY = "velocity";
    public Rigidbody2D target;
    public float detectionRange;
    public float attRangeBeforeDash;
    public float movementSpeed = 2f;

    Animator animator;
    Rigidbody2D body;
    float velocity;
    float nextDashTime;
    public float dashRate = 1f;
    public float dashDist = 50f;

    public float knockbackCount;
    public float knockbackLength;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        velocity = -movementSpeed;
        animator = GetComponent<Animator>();

        if(target == null)
        {
            var p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
                target = p.GetComponent<Rigidbody2D>();
        }
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            var p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
                target = p.GetComponent<Rigidbody2D>();
        }

        if (knockbackCount <= 0)
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, detectionRange);
            Collider2D[] attCols = Physics2D.OverlapCircleAll(transform.position, attRangeBeforeDash);
            if (Array.Exists(cols, c => c.transform.CompareTag(target.tag)))
            {
                if (!Array.Exists(attCols, ac => ac.transform.CompareTag(target.tag)))
                    ChasePlayer();
                else
                    DashMove();
            }
            else
            {
                Idle();
            }
        }
        else
        {
            knockbackCount -= Time.deltaTime;
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

    private void DashMove()
    {
        if (Time.time >= nextDashTime)
        {
            if (target.position.x < transform.position.x && velocity > 0.01f)
            {
                // player is on left
                Flip();
                velocity *= -1;
            }
            else if (target.position.x > transform.position.x && velocity < -0.01f)
            {
                // player is on right
                Flip();
                velocity *= -1;
            }
            StartCoroutine(Dash());
            nextDashTime = Time.time + 1f / dashRate;
        } 
        else
        {
            Idle();
        }
    }

    IEnumerator Dash()
    {
        var chance = UnityEngine.Random.Range(0f, 1f);
        body.velocity = new Vector2(body.velocity.x, 0f);
        if(chance < 0.7f)
        {
            body.AddForce(new Vector2(velocity * dashDist, 0f), ForceMode2D.Impulse);
        } 
        else
        {
            body.AddForce(new Vector2(-velocity * dashDist, 0f), ForceMode2D.Impulse);
        }
        
        float gravity = body.gravityScale;
        float drag = body.angularDrag;
        body.gravityScale = 0;
        body.angularDrag = 0;
        yield return new WaitForSeconds(1f / dashRate);
        body.gravityScale = gravity;
        body.angularDrag = drag;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.DrawWireSphere(transform.position, attRangeBeforeDash);
    }
}
