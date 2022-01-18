using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLock : MonoBehaviour
{

    bool isInteractable = false;
    public GameObject PasswordPanel;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && isInteractable)
        {
            // open panel
            PasswordPanel.SetActive(true);
        } else if(Input.GetKeyDown(KeyCode.X) && isInteractable)
        {
            // close panel
            PasswordPanel.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            isInteractable = true;
            collision.transform.GetComponent<PlayerMeleeCombat>().canAttack = false;
            collision.transform.GetComponent<PlayerRangedCombat>().DisableShoot();
            FindObjectOfType<OpenClueSystem>().canOpen = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    { 
        if (collision.transform.CompareTag("Player"))
        {
            isInteractable = false;
            collision.transform.GetComponent<PlayerMeleeCombat>().canAttack = true;
            collision.transform.GetComponent<PlayerRangedCombat>().EnableShoot();
            FindObjectOfType<OpenClueSystem>().canOpen = true;

        }
    }
}
