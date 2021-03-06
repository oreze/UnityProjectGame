﻿using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Transform player;
    public Text textScore;
    public Text HighScore;
    public Text EndScore;
    private int currentScore, positionValue;
    public int enemyScore;
    public PauseMenu pauseMenu;

    private void Start()
    {
        currentScore = 0;
        enemyScore = 0;
        HighScore.text = "High Score " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
    void Update()
    {
        positionValue = (int)player.position.x;

        if (currentScore < positionValue)
        {
            currentScore = positionValue;
        }
        textScore.text = (currentScore * 2 + enemyScore).ToString("0");
        EndScore.text = ("SCORE: " + (currentScore * 2 + enemyScore).ToString("0"));

        if ((currentScore * 2 + enemyScore) > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", (currentScore * 2 + enemyScore));
            HighScore.text = "High Score " + (currentScore * 2 + enemyScore).ToString();
            if (!pauseMenu.thereIsNewRecord)
            {
                pauseMenu.thereIsNewRecord = true;
            }
        }

        if (Input.GetKey(KeyCode.R))
        {
            PlayerPrefs.DeleteKey("HighScore");
        }
    }

    
}
