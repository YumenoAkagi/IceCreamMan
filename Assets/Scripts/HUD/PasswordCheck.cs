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

    AudioSource doorOpenSFX, doorCloseSFX;

    private void Awake()
    {
        if(meleeCombat == null)
        {
            meleeCombat = FindObjectOfType<PlayerMeleeCombat>();
        }
    }

    public void CheckPassword()
    {
        if(UserInput.text == DoorPassword)
        {
            PasswordPanel.SetActive(false);
            PlayOpenSFX();
            StartCoroutine(Correct()); 
        } else
        {
            StartCoroutine(Incorrect());
            PlayCloseSFX();
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

        Door.SetActive(false);
    }

    IEnumerator Incorrect()
    {
        IncorrectPanel.SetActive(true);
        
        yield return new WaitForSeconds(waitTime);
        IncorrectPanel.SetActive(false);
        maxTry--;
    }

    void PlayOpenSFX()
    {
        if (doorOpenSFX == null)
        {
            if (FindObjectOfType<AudioManager>() == null)
                return;

            var audio = Array.Find(FindObjectOfType<AudioManager>().SFXAudios, x => x.name == "DoorOpenSFX");
            if (audio == null)
                return;

            doorOpenSFX = audio;
        }

        if (!doorOpenSFX.isPlaying)
            doorOpenSFX.Play();
    }

    void PlayCloseSFX()
    {
        if(doorCloseSFX == null)
        {
            if (FindObjectOfType<AudioManager>() == null)
                return;

            var audio = Array.Find(FindObjectOfType<AudioManager>().SFXAudios, x => x.name == "DoorCloseSFX");
            if (audio == null)
                return;

            doorCloseSFX = audio;
        }

        if (!doorCloseSFX.isPlaying)
            doorCloseSFX.Play();
    }
}
