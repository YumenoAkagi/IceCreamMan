using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotkeyMechanics : MonoBehaviour
{
    public Text hotkeyQty1, hotkeyQty2, hotkeyQty3;
    public Transform player;
    public PlayerHotkeyInventory hotkeyInventory;

    public void UpdateItemQty()
    {
        hotkeyQty1.text = hotkeyInventory.getHotkeyQty(1);
        hotkeyQty2.text = hotkeyInventory.getHotkeyQty(2);
        hotkeyQty3.text = hotkeyInventory.getHotkeyQty(3);
    }
}
