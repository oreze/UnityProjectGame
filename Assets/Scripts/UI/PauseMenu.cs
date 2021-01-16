using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public static bool playerHasDied = false;
    public bool thereIsNewRecord = false;
    public GameObject pauseMenuUI;
    public GameObject deathMenu;
    public GameObject newRecord;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!playerHasDied)
            {
                if (isPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }
    

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void QuitGame()
    {
        UnityEngine.Debug.Log("QUIT");
        Application.Quit();
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("Menu");
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        isPaused = false;
        playerHasDied = false;
        SceneManager.LoadScene("Generator");
    }

    public void LoadDeathMenu()
    {
        Time.timeScale = 0f;
        playerHasDied = true;

        deathMenu.SetActive(true);
        if (thereIsNewRecord)
        {
            ActivateRecordText();
        }

    }

    public void ActivateRecordText()
    {
        newRecord.SetActive(true);
    }
}

