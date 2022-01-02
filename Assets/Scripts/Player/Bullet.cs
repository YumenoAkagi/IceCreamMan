﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 20f;
    public Rigidbody2D bulletRb;

    public AudioSource groundHitSFX;

    // Start is called before the first frame update
    void Start()
    {
        bulletRb.velocity = transform.right * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) 
        {
            collision.GetComponent<EnemyHealthStatus>().TakeDamage(20);
        } else
        {
            groundHitSFX.Play();
        }

        Destroy(gameObject);
    }
}
