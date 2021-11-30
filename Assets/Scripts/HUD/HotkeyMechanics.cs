using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotkeyMechanics : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Pressed");
            PlayerHealthStatus h = player.GetComponent<PlayerHealthStatus>();
            if(h.currHealth < h.maxHealth)
            {
                // if potion is available, heal character
                h.Heal(20);
            }
        }
    }
}
