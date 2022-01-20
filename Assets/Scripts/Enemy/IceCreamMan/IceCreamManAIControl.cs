using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IceCreamManAIControl : MonoBehaviour
{
    public float MaxHealth = 100f;
    float CurrHealth;
    public float AttackRange = 10f;
    public float MovementSpeed = 3f;
    public float TriggerRadius = 5f;
    public int projectileCount = 3;
    public float launchProjectileDelay = 1f;

    public bool isVulnerable = false;

    float nextLaunchTime = 0;

    public GameObject iceCreamPrefab, projectilePrefab;
    public Rigidbody2D target;
    public BossHealthbarSystem bossHealthbarSystem;
    public Transform shootPoint, attackPoint;
    public GameObject healthBarCanvas;

    public Animator animator;

    bool Initiated = false;
    bool FacingLeft = false;
    float TimeBeforeNextAttackPattern = 5f;

    int initIcecream = 0;

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

        if(!animator.GetBool("Triggered"))
        {
            animator.SetBool("Triggered", true);
        }

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

    public void TakeDamage(float dmg)
    {
        if (!isVulnerable)
            return;

        CurrHealth -= dmg;
        bossHealthbarSystem.UpdateHealthBar(Mathf.Clamp(CurrHealth / MaxHealth, 0, 1f));

        if(CurrHealth <= 100f && initIcecream < 2)
        {
            Instantiate(iceCreamPrefab, transform.position, Quaternion.identity);
            initIcecream++;
        }

        if(CurrHealth <= 0f)
        {
            // boss defeated
            animator.SetBool("isDead", true);

            GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            healthBarCanvas.SetActive(false);
            enabled = false;
        }
    }

    public void LaunchProjectile()
    {
        var o = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        o.GetComponent<Rigidbody2D>().velocity = CalculateAngle();
    }

    private Vector2 CalculateAngle()
    {
        Vector2 shootPos = new Vector2(shootPoint.position.x, shootPoint.position.y);
        Vector2 dist = target.position - shootPos;

        return dist;

        //float time = 1f;

        //Vector2 shootPos = new Vector2(shootPoint.position.x, shootPoint.position.y);
        //Vector2 dist = target.position - shootPos;
        //Vector2 distX = dist;
        //distX.y = 0f;

        //float Vx = distX.x / time;
        //float Vy = (dist.y + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time) / time;

        //return new Vector2(Vx, Vy);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, TriggerRadius);
    }
}
