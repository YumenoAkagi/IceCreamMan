using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PasswordCheck : MonoBehaviour
{
    public GameObject Door;
    public string DoorPassword;
    public InputField UserInput;
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
            PasswordPanel.SetActive(false);
            StartCoroutine(Correct());
        }
        else
        {
            StartCoroutine(Incorrect());
        }

        if (maxTry < 0)
        {
            SceneManager.LoadScene("GameOver");
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
        maxTry--;
    }
}
