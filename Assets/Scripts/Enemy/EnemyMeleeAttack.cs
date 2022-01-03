using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    private static string ATTACK_TRIGGER = "meleeAttack";
    public float knockback = 3f;
    public float damage = 20f;

    public float attackWaitTime = 5f;
    float nextAttackTime;

    Rigidbody2D enemyRb;

    public Animator animator;

    bool canAttack = true;
    private void Awake()
    {
        enemyRb = transform.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(Time.time >= nextAttackTime)
        {
            canAttack = true;
            nextAttackTime = Time.time + attackWaitTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!canAttack)
        {
            return;
        }

        if (collision.transform.gameObject.CompareTag("Player"))
        {
            canAttack = false;
            PlayerHealthStatus health = collision.transform.GetComponent<PlayerHealthStatus>();
            health.TakeDamage(damage);
            Rigidbody2D playerRb = collision.transform.GetComponent<Rigidbody2D>();

            if(transform.position.x < playerRb.position.x)
            {
                // enemy contact from left
                playerRb.velocity = new Vector2(knockback, knockback);
            } else if(transform.position.x >= playerRb.position.x){
                // enemy contact from right
                playerRb.velocity = new Vector2(-knockback, knockback);
            }

            PlayerMovements m = collision.transform.GetComponent<PlayerMovements>();
            m.knockbackCount = m.knockbackLength;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!canAttack)
        {
            return;
        }

        if (collision.transform.gameObject.CompareTag("Player"))
        {
            canAttack = false;

            if(animator != null)
            {
                animator.SetTrigger(ATTACK_TRIGGER);
            }

            PlayerHealthStatus health = collision.transform.GetComponent<PlayerHealthStatus>();
            health.TakeDamage(damage);
            Rigidbody2D playerRb = collision.transform.GetComponent<Rigidbody2D>();

            if (transform.position.x < playerRb.position.x)
            {
                // enemy contact from left
                playerRb.velocity = new Vector2(knockback, knockback);
            }
            else if (transform.position.x >= playerRb.position.x)
            {
                // enemy contact from right
                playerRb.velocity = new Vector2(-knockback, knockback);
            }

            PlayerMovements m = collision.transform.GetComponent<PlayerMovements>();
            m.knockbackCount = m.knockbackLength;
        }
    }
}
