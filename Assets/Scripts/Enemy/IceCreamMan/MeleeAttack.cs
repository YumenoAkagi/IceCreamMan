using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float meleeRadius = 3f;
    public float damage = 20f;

    public void meleeAttack()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(attackPoint.position, meleeRadius);

        foreach(var col in cols)
        {
            if (col.CompareTag("Player"))
            {
                col.GetComponent<PlayerHealthStatus>().TakeDamage(damage);
                return;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, meleeRadius);
    }
}
