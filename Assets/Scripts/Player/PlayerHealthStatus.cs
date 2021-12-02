﻿using UnityEngine;

public class PlayerHealthStatus : MonoBehaviour {
    public float currHealth, maxHealth;
    public HealthBarSystem healthBarSystem;

    public void TakeDamage(float dmgTaken)
    {
        // reduce player health
        currHealth -= dmgTaken;

        if(currHealth <= 0f)
        {
            // trigger game over scene
        }

        // update healthbar UI
        healthBarSystem.UpdateHealthBar();
    }

    public void Heal(float healAmount)
    {
        currHealth += healAmount;
        if(currHealth > maxHealth)
        {
            currHealth = maxHealth;
        }

        healthBarSystem.UpdateHealthBar();
    }

    private void Update()
    {
        // damage taken debug test code
        // comment if not used
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(40);
        }
    }
}
