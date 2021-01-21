using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public Text HighScore, HighScore2, VolumeValue;
    private bool WasDeleted, VolumeWasChanged;
   

    public void Start()
    {
        HighScore.text = "HIGH SCORE\n" + PlayerPrefs.GetInt("HighScore", 0).ToString();
        HighScore2.text = "HIGH SCORE\n" + PlayerPrefs.GetInt("HighScore", 0).ToString();
        WasDeleted = false;
    }

    public void Update()
    {
        VolumeWasChanged = true;
        if (WasDeleted)
        {
            HighScore.text = "HIGH SCORE\n" + PlayerPrefs.GetInt("HighScore", 0).ToString();
            HighScore2.text = "HIGH SCORE\n" + PlayerPrefs.GetInt("HighScore", 0).ToString();
        }
        VolumeValue.text = (PlayerPrefs.GetFloat("Volume", 0.5f) * 100).ToString("0");

        if (VolumeWasChanged)
        {
            VolumeWasChanged = false;
            AudioListener.volume = PlayerPrefs.GetFloat("Volume", 0.5f)/2f;
        }
    }
    public void PlayFreeRun()
    {
        SceneManager.LoadScene("Generator");
    }
    public void QuitMenu()
    {
        UnityEngine.Debug.Log("QUIT");
        Application.Quit();
    }

    public void ResetScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        WasDeleted = true;

    }

    public void TurnUpVolume()
    {
        float volume = PlayerPrefs.GetFloat("Volume", 0.5f);
        if (volume < 1) PlayerPrefs.SetFloat("Volume", volume + 0.05f);
        VolumeWasChanged = true;
    }
    
    public void LowerDownVolume()
    {
        float volume = PlayerPrefs.GetFloat("Volume", 0.5f);
        if (volume > 0.04f) PlayerPrefs.SetFloat("Volume", volume - 0.05f);
        VolumeWasChanged = true;
    }
}
