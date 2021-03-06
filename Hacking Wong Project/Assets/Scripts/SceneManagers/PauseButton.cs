﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenu;

    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }

        if(Input.GetKeyDown(KeyCode.Q) && isPaused)
        {
            LoadMenu();
        }
    }
    public void LoadMenu()
    {
        TimeManager.timeKeeper = 0;
        TimeManager.timeScore = 0;
        Player_Movement.currentHealth = Player_Movement.maxHealth;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }
}
