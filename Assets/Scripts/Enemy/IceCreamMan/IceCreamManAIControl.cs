﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IceCreamManAIControl : MonoBehaviour
{
    public float MaxHealth = 100f;
    float CurrHealth;
    public float AttackDamage = 30f;
    public float AttackRange = 10f;
    public float MovementSpeed = 3f;
    public float TriggerRadius = 5f;

    public GameObject iceCreamPrefab, projectilePrefab;
    public Rigidbody2D target;
    public BossHealthbarSystem bossHealthbarSystem;
    public Transform shootPoint;
    public GameObject healthBarCanvas;

    public Animator animator;

    bool Initiated = false;
    bool FacingLeft = false;
    float TimeBeforeNextAttackPattern = 5f;

    void Start()
    {
        CurrHealth = MaxHealth;

        if (target == null && FindObjectOfType<PlayerMovements>() != null)
            target = FindObjectOfType<PlayerMovements>().GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!Initiated)
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, TriggerRadius);
            if(Array.Exists(cols, x => x.CompareTag("Player")))
            {
                Initiated = true;
                healthBarCanvas.SetActive(true);
            }
        }


        // triggered when player is in range
        if (!Initiated)
            return;

        animator.SetBool("Triggered", true);
    }

    void SummonIceCreamObstacles()
    {
        Instantiate(iceCreamPrefab, transform.position, Quaternion.identity);
    }

    public void LookAtPlayer()
    {
        if(target.position.x < transform.position.x && !FacingLeft)
        {
            Flip();
        }
        else if(target.position.x > transform.position.x && FacingLeft)
        {
            Flip();
        }
    }

    void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
        FacingLeft = !FacingLeft;
        MovementSpeed *= -1;
    }

    public void AttackPlayer()
    {
        LaunchProjectile();
    }

    public void TakeDamage(float dmg)
    {
        CurrHealth -= dmg;
        bossHealthbarSystem.UpdateHealthBar(Mathf.Clamp(CurrHealth / MaxHealth, 0, 1f));

        if(CurrHealth <= 0f)
        {
            // boss defeated
            enabled = false;
        }
    }

    void LaunchProjectile()
    {
        var o = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        o.GetComponent<Rigidbody2D>().velocity = CalculateAngle();
    }

    private Vector2 CalculateAngle()
    {
        float time = 1f;

        Vector2 shootPos = new Vector2(shootPoint.position.x, shootPoint.position.y);
        Vector2 dist = target.position - shootPos;
        Vector2 distX = dist;
        distX.y = 0f;

        float Vx = distX.x / time;
        float Vy = (dist.y + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time) / time;

        return new Vector2(Vx, Vy);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, TriggerRadius);
    }
}
