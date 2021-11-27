using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    [SerializeField]GameObject pauseCanvas;
    bool isGamePause;
    private void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePause)
            {
                Resume();
            }
            else
            {
                Pauses();
            }
        }  
    }

    private void Pauses()
    {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
        isGamePause = true;
    }

    public void Resume()
    {
        isGamePause = false;
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
