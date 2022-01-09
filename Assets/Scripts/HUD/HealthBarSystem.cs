using UnityEngine;
using UnityEngine.UI;

public class HealthBarSystem : MonoBehaviour {
    private static string NEW_GAME = "NewGame";

    public static GameObject instance;

    public Image healthBarImg;
    public PlayerHealthStatus playerHealthStatus;
    public Animator hurtAnim;

    private void Awake()
    {
        if (PlayerPrefs.GetInt(NEW_GAME) == 1)
            instance = null;

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

        hurtAnim.SetTrigger("isHurt");
    }
}
