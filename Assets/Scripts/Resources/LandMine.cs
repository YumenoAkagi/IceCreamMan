using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : MonoBehaviour
{
    bool IsDeployed = false;
    bool IsActivated = false;
    public float ExplosionRadius = 10f;
    public float Damage = 30f;
    public float ActivationTime = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsDeployed)
        {
            if (collision.CompareTag("Player"))
            {
                // pick up item
                FindObjectOfType<PlayerHotkeyInventory>().AddHotkeyItem2();
                Destroy(gameObject);
            }
        } 
        else if(IsActivated)
        {
            // deployed land mine
            // damage all nearby enemy and player
            Collider2D[] entities = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius);

            foreach(var e in entities)
            {
                if (e.CompareTag("Player"))
                {
                    // damage player
                    e.GetComponent<PlayerHealthStatus>().TakeDamage(Damage);
                } else if(e.CompareTag("Enemy"))
                {
                    e.GetComponent<EnemyHealthStatus>().TakeDamage((int)Damage);
                } else if (e.CompareTag("Boss"))
                {
                    e.GetComponent<IceCreamManAIControl>().TakeDamage(Damage);
                }
            }

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
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
    }
}
