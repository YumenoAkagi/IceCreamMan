using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LandMine : MonoBehaviour
{
    bool IsDeployed = false;
    bool IsActivated = false;
    public float ExplosionRadius = 10f;
    public float Damage = 30f;
    public float ActivationTime = 5f;
    public float ExplosionForce = 100f;

    public GameObject indicatorLight, explosionEffect;
    public AudioSource explosionSFX;
    bool LightOn = false;
    float nextBeepTime;

    private void Start()
    {
        var audio = Array.Find(FindObjectsOfType<AudioSource>(), x => x.name == "landmineExpSFX");
        if (audio != null)
            explosionSFX = audio;
    }

    private void Update()
    {
        if (IsActivated && Time.time >= nextBeepTime)
        {
            LightOn = !LightOn;
            indicatorLight.SetActive(LightOn);
            nextBeepTime = Time.time + 1f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsDeployed)
        {
            if (collision.transform.CompareTag("Player"))
            {
                // pick up item
                FindObjectOfType<PlayerHotkeyInventory>().AddHotkeyItem2();
                Destroy(gameObject);
            }
        }
        else if (IsActivated)
        {
            // deployed land mine
            // damage all nearby enemy and player
            Collider2D[] entities = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius);

            foreach (var e in entities)
            {
                if (e.CompareTag("Player"))
                {
                    // damage player
                    e.GetComponent<PlayerHealthStatus>().TakeDamage(Damage);
                }
                else if (e.CompareTag("Enemy"))
                {
                    e.GetComponent<EnemyHealthStatus>().TakeDamage((int)Damage);
                }
                else if (e.CompareTag("Boss"))
                {
                    e.GetComponent<IceCreamManAIControl>().TakeDamage(Damage);
                }
            }

            Instantiate(explosionEffect, transform.position, transform.rotation);
            //Knockback();
            if (explosionSFX != null)
                explosionSFX.Play();

            Destroy(gameObject);
        }
    }

    public void DeployLandMine()
    {
        IsDeployed = true;
        StartCoroutine(ActivateLandMine());
    }


    IEnumerator ActivateLandMine()
    {
        yield return new WaitForSeconds(ActivationTime);
        IsActivated = true;
        nextBeepTime = Time.time + 1f;
    }

    void Knockback()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius);

        foreach(var col in cols)
        {
            Rigidbody2D rb = col.GetComponent<Rigidbody2D>();

            if(rb != null)
            {
                // apply knockback
                rb.AddExplosionForce(ExplosionForce, transform.position, ExplosionRadius);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
    }
}
