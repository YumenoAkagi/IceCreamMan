using UnityEngine;
using UnityEngine.UI;

public class HealthBarSystem : MonoBehaviour {

    public static GameObject instance;

    public Image healthBarImg;
    public PlayerHealthStatus playerHealthStatus;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if(instance == null)
        {
            instance = gameObject;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void UpdateHealthBar()
    {
        float currHealth = playerHealthStatus.currHealth;
        float maxHealth = playerHealthStatus.maxHealth;
        healthBarImg.fillAmount = Mathf.Clamp(currHealth / maxHealth, 0, 1f);
    }
}
