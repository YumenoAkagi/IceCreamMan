using UnityEngine;
using UnityEngine.UI;

public class HealthBarSystem : MonoBehaviour {
    public Image healthBarImg;
    public PlayerHealthStatus playerHealthStatus;

    public void UpdateHealthBar()
    {
        float currHealth = playerHealthStatus.currHealth;
        float maxHealth = playerHealthStatus.maxHealth;
        healthBarImg.fillAmount = Mathf.Clamp(currHealth / maxHealth, 0, 1f);
    }
}
