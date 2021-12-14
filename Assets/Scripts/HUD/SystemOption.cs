using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemOption : MonoBehaviour
{
    private static readonly string MasterVolumeKey = "MasterVolume";
    private static readonly string BGMVolumeKey = "BGMVolume";
    private static readonly string SFXVolumeKey = "SFXVolume";
    private static readonly string FirstPlayKey = "FirstPlay";

    private float BGMVolumeValue;
    private float MasterVolumeValue;
    private float SFXVolumeValue;

    public Slider MasterVolumeSlider;
    public Slider BGMVolumeSlider;
    public Slider SFXSlider;

    public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey(FirstPlayKey) || PlayerPrefs.GetInt(FirstPlayKey) == 0)
        {
            MasterVolumeValue = BGMVolumeValue = SFXVolumeValue = 1f;
            MasterVolumeSlider.value = MasterVolumeValue;
            BGMVolumeSlider.value = BGMVolumeValue;
            SFXSlider.value = SFXVolumeValue;

            PlayerPrefs.SetFloat(MasterVolumeKey, MasterVolumeValue);
            PlayerPrefs.SetFloat(BGMVolumeKey, BGMVolumeValue);
            PlayerPrefs.SetFloat(SFXVolumeKey, SFXVolumeValue);
            PlayerPrefs.SetInt(FirstPlayKey, -1);
        } else
        {
            // get and apply settings
            MasterVolumeValue = PlayerPrefs.GetFloat(MasterVolumeKey, 1f);
            MasterVolumeSlider.value = MasterVolumeValue;

            BGMVolumeValue = PlayerPrefs.GetFloat(BGMVolumeKey, 1f);
            BGMVolumeSlider.value = BGMVolumeValue;

            SFXVolumeValue = PlayerPrefs.GetFloat(SFXVolumeKey, 1f);
            SFXSlider.value = SFXVolumeValue;
        }
    }

    public void UpdateSettings()
    {
        // apply settings to env
        audioManager.UpdateSoundsVolume(MasterVolumeSlider.value, BGMVolumeSlider.value, SFXSlider.value);
    }

    public void SafeOptionSettings()
    {
        PlayerPrefs.SetFloat(MasterVolumeKey, MasterVolumeSlider.value);
        PlayerPrefs.SetFloat(BGMVolumeKey, BGMVolumeSlider.value);
        PlayerPrefs.SetFloat(SFXVolumeKey, SFXSlider.value);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
            SafeOptionSettings();
    }
}
