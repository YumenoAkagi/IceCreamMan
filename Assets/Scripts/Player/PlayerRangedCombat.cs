using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangedCombat : MonoBehaviour
{
    public Transform firingPoint;
    public GameObject bulletPrefab;
    public float ReloadTime = 2f;

    public AudioSource gunshotSFX, reloadSFX;

    int bulletMag = 5;
    bool canShoot = true;
    bool IsReloading = false;

    // Update is called once per frame
    void Update()
    {
        if(bulletMag <= 0 && canShoot)
        {
            canShoot = false;
        }

        if (Input.GetButtonDown("Fire2") && canShoot)
        {
            Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
            bulletMag--;
            gunshotSFX.Play();
        } else if(Input.GetButtonDown("Reload") || Input.GetButtonDown("Fire2") && !IsReloading)
        {
            // force reload
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        IsReloading = true;
        reloadSFX.Play();
        canShoot = false;
        yield return new WaitForSeconds(ReloadTime);
        IsReloading = false;
        bulletMag = 5; // hardcoded for testing
        canShoot = true;
    }
}
