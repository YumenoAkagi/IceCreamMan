using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PasswordCheck : MonoBehaviour
{
    public GameObject Door;
    public string DoorPassword;
    public InputField UserInput;

    public GameObject CorrectPanel, IncorrectPanel;

    public int maxTry = 3;

    public void CheckPassword()
    {
        if(UserInput.text == DoorPassword)
        {
            CorrectPanel.SetActive(true);
            Destroy(Door);
        } else
        {
            IncorrectPanel.SetActive(true);
            maxTry--;
        }

        if(maxTry < 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
