using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private static string NEW_GAME = "NewGame";
    public Animator transition;
    public float transitionTime = 1f;

    public void StartGame()
    {
        PlayerPrefs.SetInt(NEW_GAME, 1);
        LoadNextLevel();
    }

    public void StartGameskipIntro()
    {
        PlayerPrefs.SetInt(NEW_GAME, 1);
        StartCoroutine(LoadLevelSkipIntro());
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int index)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(index);
    }

    IEnumerator LoadLevelSkipIntro()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene("Level1");
    }
}
