using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    public Slider musicSlider;
    public Slider soundSlider;

    private void Start()
    {
        UpdatePrefs();
    }

    public void UpdatePrefs()
    {
        musicSlider.value = PlayerPrefs.GetFloat("music_volume");
        soundSlider.value = PlayerPrefs.GetFloat("sound_volume");
    }

    public void UpdateMusic(float value)
    {
        PlayerPrefs.SetFloat("music_volume", value);
        PlayerPrefs.Save();
    }

    public void UpdateSound(float value)
    {
        PlayerPrefs.SetFloat("sound_volume", value);
        PlayerPrefs.Save();
    }
}
