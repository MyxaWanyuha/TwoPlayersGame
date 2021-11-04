using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    Slider musicSlider;
    [SerializeField]
    AudioMixerGroup mixer;
    bool isMusicEnabled = false;
    public void LoadScene(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void ToggleMusic()
    {
        if (isMusicEnabled)
        {
            mixer.audioMixer.SetFloat("MusicVolume", 0);
        }
        else
            mixer.audioMixer.SetFloat("MusicVolume", -80);
        isMusicEnabled = !isMusicEnabled;

    }
    public void ChangeMusicVolume()
    {
        mixer.audioMixer.SetFloat("MusicVolume", musicSlider.value);
    }
}
