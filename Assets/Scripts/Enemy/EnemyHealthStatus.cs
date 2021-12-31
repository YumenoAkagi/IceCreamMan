using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthStatus : MonoBehaviour
{
    public AudioSource hitSFX;
    public int maxHealth;
    int currHealth;

    private void Awake()
    {
        currHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currHealth -= damage;
        hitSFX.Play();

        if(currHealth <= 0)
        {
            // dead animation
            // remove enemy
            Destroy(gameObject);
        }
    }
}
