using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider mainSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        mainSlider.value = Mathf.Exp((audioMixer.GetFloat("MainVolume", out float mainVolume) ? mainVolume : 0.75f) / 20f);
        musicSlider.value = Mathf.Exp((audioMixer.GetFloat("MusicVolume", out float musicVolume) ? musicVolume : 1f) / 20f);
        sfxSlider.value = Mathf.Exp((audioMixer.GetFloat("SFXVolume", out float sfxVolume) ? sfxVolume : 1f) / 20f);

        mainSlider.onValueChanged.AddListener(SetMainVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void SetMainVolume(float value)
    {
        audioMixer.SetFloat("MainVolume", Mathf.Log(value) * 20f);
    }

    private void SetMusicVolume(float value)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log(value) * 20f);
    }

    private void SetSFXVolume(float value)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log(value) * 20f);
    }
}