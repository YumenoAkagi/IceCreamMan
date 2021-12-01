using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.CompareTag("Player"))
        {
            Transform player = collision.transform;
            PlayerHotkeyInventory hotkeyInventory = player.GetComponent<PlayerHotkeyInventory>();

            hotkeyInventory.AddHotkeyItem1();

            Destroy(gameObject);
        }
    }
}
