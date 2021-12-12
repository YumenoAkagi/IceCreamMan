using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeCombat : MonoBehaviour
{
    Animator animator;

    public Transform attPoint;
    public float attRange = 0.7f;
    public float attDmg = 20f;
    public float knockback = 3f;
    public float attRate = 2f;
    float nextAttTime = 0;

    public LayerMask enemyLayer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Time.time >= nextAttTime)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                MeleeAttack();
                nextAttTime = Time.time + 1f / attRate;
            }
        }
       
    }

    private void MeleeAttack()
    {
        animator.SetTrigger("melee");

        Collider2D[] enemies = Physics2D.OverlapCircleAll(attPoint.position, attRange, enemyLayer);

        foreach(Collider2D e in enemies)
        {
            Debug.Log("Enemy Hit!");
            EnemyHealthStatus enemyHealth = e.GetComponent<EnemyHealthStatus>();
            enemyHealth.TakeDamage((int)attDmg);

            // knockback enemy if can be knockback
            if(e.transform.name == "Slime")
            {
                Rigidbody2D eRb = e.transform.GetComponent<Rigidbody2D>();
                if(eRb != null)
                {
                    if (transform.position.x >= e.transform.position.x)
                    {
                        // enemy contact from left
                        eRb.velocity = new Vector2(-knockback, knockback);
                    }
                    else if (transform.position.x < e.transform.position.x)
                    {
                        // enemy contact from right
                        eRb.velocity = new Vector2(knockback, knockback);
                    }

                    SlimeAI ai = e.GetComponent<SlimeAI>();
                    ai.knockbackCount = ai.knockbackLength;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attPoint == null)
            return;
        Gizmos.DrawWireSphere(attPoint.position, attRange);
    }
}
