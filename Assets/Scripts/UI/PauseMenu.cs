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
    public Text VolumeValue;
    public Text MusicVolumeValue;
    public AudioScript AudioHandler;


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
        if (isPaused)
        {
            VolumeValue.text = (PlayerPrefs.GetFloat("Volume", 0.5f) * 100).ToString("0");
            MusicVolumeValue.text = (PlayerPrefs.GetFloat("MusicVolume", 0.05f) * 1000).ToString("0");
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
        playerHasDied = false;
        UnityEngine.Debug.Log("QUIT");
        Application.Quit();
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        playerHasDied = false;
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
    public void TurnUpVolume()
    {
        float volume = PlayerPrefs.GetFloat("Volume", 0.5f);
        if (volume < 1) PlayerPrefs.SetFloat("Volume", volume + 0.05f);
        AudioHandler.VolumeWasChanged = true;
    }

    public void LowerDownVolume()
    {
        float volume = PlayerPrefs.GetFloat("Volume", 0.5f);
        if (volume > 0.04f) PlayerPrefs.SetFloat("Volume", volume - 0.05f);
        AudioHandler.VolumeWasChanged = true;
    }
    public void TurnUpMusicVolume()
    {
        float volume = PlayerPrefs.GetFloat("MusicVolume", 0.05f);
        if (volume < 0.1f) PlayerPrefs.SetFloat("MusicVolume", volume + 0.005f);
        AudioHandler.VolumeWasChanged = true;
    }

    public void LowerDownMusicVolume()
    {
        float volume = PlayerPrefs.GetFloat("MusicVolume", 0.05f);
        if (volume > 0.004f) PlayerPrefs.SetFloat("MusicVolume", volume - 0.005f);
        AudioHandler.VolumeWasChanged = true;
    }
}



