using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthStatus : MonoBehaviour
{
    public AudioSource hitSFX, deadSFX;
    public int maxHealth;
    int currHealth;

    private void Awake()
    {
        currHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currHealth -= damage;
        if (hitSFX != null)
            hitSFX.Play();

        if(currHealth <= 0)
        {
            // do before dead
            if(deadSFX != null)
                deadSFX.Play();
            // remove enemy
            Destroy(gameObject);
        }
    }
}
