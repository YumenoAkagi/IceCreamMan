using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PasswordCheck : MonoBehaviour
{
    private static string RETRY_LEVEL_KEY = "RetryLevel";

    public GameObject Door;
    public string DoorPassword;
    public string DoorPassword2;
    public bool doublePassword = false;
    public InputField UserInput, userInput2;
    public PlayerMeleeCombat meleeCombat;

    public GameObject CorrectPanel, IncorrectPanel, PasswordPanel;

    public int maxTry = 3;
    public float waitTime = 2f;

    public AudioSource doorOpenSFX, doorCloseSFX;

    private void Awake()
    {
        if (meleeCombat == null)
        {
            meleeCombat = FindObjectOfType<PlayerMeleeCombat>();
        }
    }

    public void CheckPassword()
    {
        if (UserInput.text == DoorPassword)
        {
            if(doublePassword)
            {
                if(userInput2.text == DoorPassword2)
                {
                    PasswordPanel.SetActive(false);
                    StartCoroutine(Correct());
                } else
                {
                    maxTry--;
                    StartCoroutine(Incorrect());
                }
            } else
            {
                PasswordPanel.SetActive(false);
                StartCoroutine(Correct());
            }
        }
        else
        {
            maxTry--;
            StartCoroutine(Incorrect());
        }

        if (maxTry < 0)
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
    }
    IEnumerator Correct()
    {
        CorrectPanel.SetActive(true);
        doorOpenSFX.Play();

        yield return new WaitForSeconds(waitTime);
        CorrectPanel.SetActive(false);
        meleeCombat.canAttack = true;

        Door.SetActive(false);
    }

    IEnumerator Incorrect()
    {
        IncorrectPanel.SetActive(true);
        doorCloseSFX.Play();

        yield return new WaitForSeconds(waitTime);
        IncorrectPanel.SetActive(false);
    }
}
