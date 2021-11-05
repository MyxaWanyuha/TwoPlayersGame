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
    Slider soundSlider;
    [SerializeField]
    AudioMixerGroup mixer;
    public void LoadScene(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void ToggleMusic(bool isEnabled)
    {
        if (isEnabled)
        {
            mixer.audioMixer.SetFloat("MusicVolume", musicSlider.value);
        }
        else
            mixer.audioMixer.SetFloat("MusicVolume", -80);
    }
    public void ToggleSound(bool isEnabled)
    {
        if (isEnabled)
        {
            mixer.audioMixer.SetFloat("SoundVolume", soundSlider.value);
        }
        else
            mixer.audioMixer.SetFloat("SoundVolume", -80);
    }
    public void ChangeMusicVolume(float volume)
    {
        mixer.audioMixer.SetFloat("MusicVolume", volume);
    }
    public void ChangeSoundVolume(float volume)
    {
        mixer.audioMixer.SetFloat("SoundVolume", volume);
    }
}
