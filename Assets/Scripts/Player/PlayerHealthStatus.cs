using UnityEngine;

public class PlayerHealthStatus : MonoBehaviour {
    public float currHealth, maxHealth;
    public HealthBarSystem healthBarSystem;

    public void TakeDamage(float dmgTaken)
    {
        // reduce player health
        currHealth -= dmgTaken;

        // update healthbar UI
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
