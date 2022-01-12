using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthStatus : MonoBehaviour {
    private static string RETRY_LEVEL_KEY = "RetryLevel";
    public float currHealth, maxHealth;
    public HealthBarSystem healthBarSystem;
    public Animator hurtAnim;

    public void TakeDamage(float dmgTaken)
    {
        // reduce player health
        currHealth -= dmgTaken;

        if(currHealth <= 0f)
        {
            var objects = GameObject.FindObjectsOfType<GameObject>();
            foreach (var o in objects)
            {
                Destroy(o.gameObject);
            }

            // trigger game over scene
            PlayerPrefs.SetInt(RETRY_LEVEL_KEY, SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene("GameOverScene");
        }

        hurtAnim.SetTrigger("isHurt");
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

    public void InstantDeath()
    {
        TakeDamage(maxHealth);
    }

    private void Update()
    {
        // damage taken debug test code
        // comment if not used
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    TakeDamage(40);
        //}
    }
}
