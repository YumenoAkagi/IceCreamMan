using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private static string RETRY_LEVEL_KEY = "RetryLevel";

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Retry()
    {
        int retryScene = PlayerPrefs.GetInt(RETRY_LEVEL_KEY);
        SceneManager.LoadScene(retryScene);
    }
}
