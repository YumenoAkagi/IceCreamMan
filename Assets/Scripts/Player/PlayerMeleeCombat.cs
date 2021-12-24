﻿using System;
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
    public bool canAttack = true;

    public LayerMask enemyLayer;

    private AudioSource meleeSFX;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Time.time >= nextAttTime)
        {
            if (Input.GetKeyDown(KeyCode.Z) && canAttack)
            {
                MeleeAttack();
                nextAttTime = Time.time + 1f / attRate;
            }
        }
       
    }

    private void MeleeAttack()
    {
        animator.SetTrigger("melee");
        PlayMeleeSFX();

        Collider2D[] enemies = Physics2D.OverlapCircleAll(attPoint.position, attRange, enemyLayer);

        foreach(Collider2D e in enemies)
        {
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
            if (e.transform.name == "Grandpa")
            {
                Rigidbody2D eRb = e.transform.GetComponent<Rigidbody2D>();
                Debug.Log(eRb);
                if (eRb != null)
                {
                    if (transform.position.x >= e.transform.position.x)
                    {
                        // enemy contact from left
                        eRb.velocity = new Vector2(-knockback, 0f);
                    }
                    else if (transform.position.x < e.transform.position.x)
                    {
                        // enemy contact from right
                        eRb.velocity = new Vector2(knockback, 0f);
                    }

                    GrandpaAIControls ai = e.GetComponent<GrandpaAIControls>();
                    ai.knockbackCount = ai.knockbackLength;
                }
            }
        }
    }

    private void PlayMeleeSFX()
    {
        if (meleeSFX == null)
        {
            if (FindObjectOfType<AudioManager>() == null)
                return;

            var audio = Array.Find(FindObjectOfType<AudioManager>().SFXAudios, x => x.name == "Player - Melee");

            if (audio == null)
                return;

            meleeSFX = audio;
        }

        meleeSFX.Play();
    }

    private void OnDrawGizmosSelected()
    {
        if (attPoint == null)
            return;
        Gizmos.DrawWireSphere(attPoint.position, attRange);
    }
}
