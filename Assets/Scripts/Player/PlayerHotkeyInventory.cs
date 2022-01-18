using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHotkeyInventory : MonoBehaviour
{
    int hotkeyQty1, hotkeyQty2, hotkeyQty3;
    public HotkeyMechanics hotkeyMechanics;
    public PlayerHealthStatus healthStatus;

    public GameObject landMinePrefab;
    public Transform deployPoint;
    public AudioSource itemObtainedSFX;

    private void Awake()
    {
        healthStatus = GetComponent<PlayerHealthStatus>();
        // initialize text for the first time
        hotkeyMechanics.UpdateItemQty();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(healthStatus.currHealth < healthStatus.maxHealth && hotkeyQty1 > 0)
            {
                hotkeyQty1 -= 1;
                healthStatus.Heal(20);
                hotkeyMechanics.UpdateItemQty();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (hotkeyQty2 <= 0)
                return;

            hotkeyQty2--;
            GameObject obj = Instantiate(landMinePrefab, deployPoint.position, Quaternion.identity);
            obj.GetComponent<LandMine>().DeployLandMine();
            hotkeyMechanics.UpdateItemQty();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {

        }
    }

    public void AddHotkeyItem1()
    {
        hotkeyQty1++;
        hotkeyMechanics.UpdateItemQty();
        itemObtainedSFX.Play();
    }

    public void AddHotkeyItem2()
    {
        hotkeyQty2++;
        hotkeyMechanics.UpdateItemQty();
        itemObtainedSFX.Play();
    }

    public void AddHotkeyItem3()
    {
        hotkeyQty3++;
        hotkeyMechanics.UpdateItemQty();
        itemObtainedSFX.Play();
    }

    public string getHotkeyQty(int hotkey)
    {
        string qty = "";
        switch (hotkey)
        {
            case 1:
                qty = hotkeyQty1.ToString();
                break;
            case 2:
                qty = hotkeyQty2.ToString();
                break;
            case 3:
                qty = hotkeyQty3.ToString();
                break;
            default:
                break;
        }

        return qty;
    }
}
