using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Chest : MonoBehaviour
{
    private static readonly string CHEST_OPEN_BOOL = "isOpen";
    private bool IsContactWithPlayer = false;
    Animator animator;

    public float MedKitDropChance = 20f;
    public GameObject medKitPrefab;
    public int clueIndex;
    public AudioSource chestOpenSFX;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(IsContactWithPlayer && Input.GetKeyDown(KeyCode.Z))
        {
            OpenChest();
        }
    }

    public void OpenChest()
    {
        animator.SetBool(CHEST_OPEN_BOOL, true);
        chestOpenSFX.Play();

        // unlock button in player's clue panel
        if(clueIndex != 0)
            FindObjectOfType<HealthBarSystem>().transform.GetComponentInChildren<CloseAllClues>(true).buttons[clueIndex - 1].SetActive(true);

        DrawMedKitChance();

        GetComponent<Collider2D>().enabled = false;
        enabled = false;
    }

    void DrawMedKitChance()
    {
        float chance = Random.Range(0, 100);
        if(chance <= MedKitDropChance) {
            Instantiate(medKitPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            IsContactWithPlayer = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            IsContactWithPlayer = false;
        }
    }
}
