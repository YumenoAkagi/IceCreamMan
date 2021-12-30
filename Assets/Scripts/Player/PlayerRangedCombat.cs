using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangedCombat : MonoBehaviour
{
    public Transform firingPoint;
    public GameObject bulletPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
        }
    }
}
