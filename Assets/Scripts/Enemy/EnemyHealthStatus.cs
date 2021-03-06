using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthStatus : MonoBehaviour
{
    public AudioSource hitSFX, deadSFX;
    public int maxHealth;
    int currHealth;

    public GameObject landMinePrefab;

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

            // chance drop item
            DropItem();

            // remove enemy
            Destroy(gameObject);
        }
    }

    void DropItem()
    {
        float chance = Random.Range(0, 100f);
        if(chance <= 60f)
        {
            FindObjectOfType<PlayerRangedCombat>().AddTotalAmmo(1);
        }
        if(chance <= 15f)
        {
            if (landMinePrefab == null)
                return;
            // drop landmine
            Instantiate(landMinePrefab, transform.position, Quaternion.identity);
        }
    }
}
