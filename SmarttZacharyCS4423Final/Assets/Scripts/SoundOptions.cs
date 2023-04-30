using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundOptions : MonoBehaviour
{
    public AudioMixer audio;

    public Slider masterSlider;
    public Slider sfxSlider;
    public Slider musicSlider;

    void Start() {
        if(PlayerPrefs.GetInt("set first time volume") == 0) {
            PlayerPrefs.SetInt("set first time volume",1);
            masterSlider.value = .5f;
            sfxSlider.value = .5f;
            musicSlider.value = .5f;
        }
        else {
            masterSlider.value = PlayerPrefs.GetFloat("Master");
            sfxSlider.value = PlayerPrefs.GetFloat("SFX");
            musicSlider.value = PlayerPrefs.GetFloat("Music");
        }
    }

    void SetMasterVolume() {
        SetVolume("Master", masterSlider.value);
    }

    void SetSFXSlider() {
        SetVolume("SFX", sfxSlider.value);
    }

    void SetMusicVolume() {
        SetVolume("Music", musicSlider.value);
    }

    void SetVolume(string name, float value) {
        float volume = Mathf.Log10(value) * 20;
        if(value == 0) {
            volume = -80;
        }

        audio.SetFloat(name,volume);
        PlayerPrefs.SetFloat(name,value);
    }
}
