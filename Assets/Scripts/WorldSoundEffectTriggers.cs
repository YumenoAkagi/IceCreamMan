using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSoundEffectTriggers : MonoBehaviour
{
    public AudioSource worldSFX;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // play sound effect
            worldSFX.Play();
        }
    }
}
