using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    public float thrust = 3f;

    Rigidbody2D enemyRb;

    private void Awake()
    {
        enemyRb = transform.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.gameObject.CompareTag("Player"))
        {
            PlayerHealthStatus health = collision.transform.GetComponent<PlayerHealthStatus>();
            health.TakeDamage(20);
            Rigidbody2D playerRb = collision.transform.GetComponent<Rigidbody2D>();
            playerRb.velocity = new Vector2(thrust * enemyRb.velocity.x, thrust * enemyRb.velocity.x);
        }
    }
}
