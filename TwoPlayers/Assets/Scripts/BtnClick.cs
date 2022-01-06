using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnClick : MonoBehaviour
{
    AudioSource sound;
    private void Start()
    {
        sound = GetComponent<AudioSource>();
    }
    public void ClickSound()
    {
        sound.Play();
    }
}
