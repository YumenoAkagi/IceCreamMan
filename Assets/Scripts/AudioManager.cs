using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static readonly string MasterVolumeKey = "MasterVolume";
    private static readonly string BGMVolumeKey = "BGMVolume";
    private static readonly string SFXVolumeKey = "SFXVolume";

    public AudioSource BGMAudio;
    public AudioSource[] SFXAudios;

    float masterVol, bgmVol, sfxVol;

    private void Awake()
    {
        UpdateSoundsVolume();
    }

    public void UpdateSoundsVolume()
    {
        masterVol = PlayerPrefs.GetFloat(MasterVolumeKey);
        bgmVol = PlayerPrefs.GetFloat(BGMVolumeKey);
        sfxVol = PlayerPrefs.GetFloat(SFXVolumeKey);

        if(BGMAudio != null)
            BGMAudio.volume = masterVol * bgmVol;

        // sfx audios update volume
        foreach(AudioSource s in SFXAudios)
        {
            // update volume
            s.volume = masterVol * sfxVol;
        }
    }

    public void UpdateSoundsVolume(float master, float bgm, float sfx)
    {
        masterVol = master;
        bgmVol = bgm;
        sfxVol = sfx;

        if (BGMAudio != null)
            BGMAudio.volume = masterVol * bgmVol;

        // sfx audios update volume
        foreach (AudioSource s in SFXAudios)
        {
            // update volume
            s.volume = masterVol * sfxVol;
        }
    }
}
