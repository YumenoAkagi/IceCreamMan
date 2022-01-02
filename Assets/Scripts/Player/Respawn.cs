using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    private static string RETRY_LEVEL_KEY = "RetryLevel";
    public GameObject player, hud;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(RETRY_LEVEL_KEY))
            return;

        int retry = PlayerPrefs.GetInt(RETRY_LEVEL_KEY);

        if (retry != SceneManager.GetActiveScene().buildIndex)
            return;

        player.SetActive(true);
        hud.SetActive(true);

        PlayerPrefs.SetInt(RETRY_LEVEL_KEY, 0);
    }
}
