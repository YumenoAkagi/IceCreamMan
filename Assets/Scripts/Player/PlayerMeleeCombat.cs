using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeCombat : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            EnemyMeleeAttack();
        }
    }

    private void EnemyMeleeAttack()
    {
        animator.SetTrigger("melee");
    }
}
