using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Transform player;
    public Text textScore;
    private int currentScore, positionValue;
    public int enemyScore;

    private void Start()
    {
        currentScore = 0;
        enemyScore = 0;
    }
    void Update()
    {
        positionValue = (int)player.position.x;

        if (currentScore < positionValue)
        {
            currentScore = positionValue;
        }
        textScore.text = (currentScore * 2 + enemyScore).ToString("0");
    }
}
