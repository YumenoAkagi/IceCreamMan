using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSoundEffectTriggers : MonoBehaviour
{
    public float timeBeforeDisable = 5f;
    public AudioSource worldSFX;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // play sound effect
            StartCoroutine(playSFX());
        }
    }

    IEnumerator playSFX()
    {
        worldSFX.Play();

        yield return new WaitForSeconds(timeBeforeDisable);

        GetComponent<Collider2D>().enabled = false;
        enabled = false;
    }
}
