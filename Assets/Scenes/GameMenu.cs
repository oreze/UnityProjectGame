using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public Text HighScore, HighScore2;
    private bool WasDeleted;

    public void Start()
    {
        HighScore.text = "HIGH SCORE\n" + PlayerPrefs.GetInt("HighScore", 0).ToString();
        HighScore2.text = "HIGH SCORE\n" + PlayerPrefs.GetInt("HighScore", 0).ToString();
        WasDeleted = false;
    }

    public void Update()
    {
        if (WasDeleted)
        {
            HighScore.text = "HIGH SCORE\n" + PlayerPrefs.GetInt("HighScore", 0).ToString();
            HighScore2.text = "HIGH SCORE\n" + PlayerPrefs.GetInt("HighScore", 0).ToString();
        }  
    }
    public void PlayFreeRun()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
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
}
