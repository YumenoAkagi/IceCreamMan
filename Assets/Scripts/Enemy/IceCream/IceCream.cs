using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCream : MonoBehaviour
{
    public float knockback = 3f;
    public float damage = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            PlayerHealthStatus pH = collision.transform.GetComponent<PlayerHealthStatus>();
            Rigidbody2D playerRb = collision.transform.GetComponent<Rigidbody2D>();
            pH.TakeDamage(damage);

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
