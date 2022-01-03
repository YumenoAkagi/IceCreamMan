using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangedCombat : MonoBehaviour
{
    public Transform firingPoint;
    public GameObject bulletPrefab;
    public float ReloadTime = 1f;
    public AmmoSystem ammoSystem;

    public AudioSource gunshotSFX, reloadSFX, emptyShotSFX;

    int bulletMag = 5;
    public int magCapacity = 5;
    int totalAmmo = 10;
    bool canShoot = true;
    bool IsReloading = false;
    bool bulletEmpty = false;


    private void Awake()
    {
        ammoSystem.UpdateAmmoUI();
    }

    void Update()
    {
        if (!canShoot)
            return;

        if(bulletMag <= 0 && !bulletEmpty)
        {
            bulletEmpty = true;
        }

       

        if (Input.GetButtonDown("Fire2") && !bulletEmpty)
        {
            Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
            bulletMag--;
            gunshotSFX.Play();
        } else if(totalAmmo == 0 && bulletEmpty && Input.GetButtonDown("Fire2")) {
            emptyShotSFX.Play();
            return;
        }
        else if(Input.GetButtonDown("Reload") || Input.GetButtonDown("Fire2") && !IsReloading)
        {
            // force reload
            StartCoroutine(Reload());
        }

        ammoSystem.UpdateAmmoUI();
    }

    IEnumerator Reload()
    {
        IsReloading = true;
        reloadSFX.Play();
        bulletEmpty = true;

        yield return new WaitForSeconds(ReloadTime);

        int totalBulletToRefill = magCapacity - bulletMag;

        if (totalAmmo <= totalBulletToRefill)
        {
            bulletMag += totalAmmo;
            totalAmmo = 0;
        }
        else
        {
            bulletMag += totalBulletToRefill;
            totalAmmo -= totalBulletToRefill;
        }

        IsReloading = false;
        bulletEmpty = false;
    }

    public void AddTotalAmmo(int ammo)
    {
        totalAmmo += ammo;
    }

    public int GetAmmoLeft()
    {
        return totalAmmo;
    }

    public int GetMagAmmo()
    {
        return bulletMag;
    }

    public void EnableShoot()
    {
        canShoot = true;
    }

    public void DisableShoot()
    {
        canShoot = false;
    }
}
