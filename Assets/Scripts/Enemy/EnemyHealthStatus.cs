using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthStatus : MonoBehaviour
{
    public int maxHealth;
    int currHealth;

    public void TakeDamage(int damage)
    {
        currHealth -= damage;

        if(currHealth <= 0)
        {
            // dead animation
            // remove enemy
            Destroy(gameObject);
        }
    }
}
