using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangedCombat : MonoBehaviour
{
    public Transform firingPoint;
    public GameObject bulletPrefab;

    AudioSource gunshotSFX;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
            PlayGunShotSFX();
        }
    }

    void PlayGunShotSFX()
    {
        if(gunshotSFX == null)
        {
            if (FindObjectOfType<AudioManager>() == null)
                return;

            var audio = Array.Find(FindObjectOfType<AudioManager>().SFXAudios, x => x.name == "Player - Ranged");

            if (audio == null)
                return;

            gunshotSFX = audio;
        }

        gunshotSFX.Play();
    }
}
