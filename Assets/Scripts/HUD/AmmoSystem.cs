using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoSystem : MonoBehaviour
{
    public PlayerRangedCombat playerAmmoStatus;
    public Text ammoText;

    public void UpdateAmmoUI()
    {
        ammoText.text = playerAmmoStatus.GetMagAmmo() + " / " + playerAmmoStatus.GetAmmoLeft();
    }
}
