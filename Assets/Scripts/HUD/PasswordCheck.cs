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
    public PlayerMeleeCombat meleeCombat;

    public GameObject CorrectPanel, IncorrectPanel, PasswordPanel;

    public int maxTry = 3;
    public float waitTime = 2f;

    public void CheckPassword()
    {
        if(UserInput.text == DoorPassword)
        {
            PasswordPanel.SetActive(false);
            StartCoroutine(Correct()); 
        } else
        {
            StartCoroutine(Incorrect());
        }

        if(maxTry < 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
    IEnumerator Correct()
    {
        CorrectPanel.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        CorrectPanel.SetActive(false);
        meleeCombat.canAttack = true;
        Destroy(Door);
    }

    IEnumerator Incorrect()
    {
        IncorrectPanel.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        IncorrectPanel.SetActive(false);
        maxTry--;
    }
}
