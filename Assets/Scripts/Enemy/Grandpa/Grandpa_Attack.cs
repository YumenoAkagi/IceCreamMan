using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grandpa_Attack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 2f;
    public float Damage = 20f;

    public void Attack()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        foreach(var col in cols)
        {
            if(col.CompareTag("Player"))
            {
                col.GetComponent<PlayerHealthStatus>().TakeDamage(Damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
