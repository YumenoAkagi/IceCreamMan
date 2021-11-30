using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHotkeyInventory : MonoBehaviour
{
    public int hotkeyQty1, hotkeyQty2, hotkeyQty3;
    public HotkeyMechanics hotkeyMechanics;
    public PlayerHealthStatus healthStatus;

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
            if (healthStatus.currHealth < healthStatus.maxHealth && hotkeyQty2 > 0)
            {
                hotkeyQty2 -= 1;
                healthStatus.Heal(10);
                hotkeyMechanics.UpdateItemQty();
            }  
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (healthStatus.currHealth < healthStatus.maxHealth && hotkeyQty3 > 0)
            {
                hotkeyQty3 -= 1;
                healthStatus.Heal(5);
                hotkeyMechanics.UpdateItemQty();
            }
        }
    }
}
