using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public int highScore;

    private void Awake()
    {
        References.scoreManager = this;
    }

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore");
        References.canvas.highScoreText.text = highScore.ToString();
    }

    public void UpdateHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            References.canvas.highScoreText.text = highScore.ToString();
            PlayerPrefs.SetInt("highScore", highScore);
            PlayerPrefs.Save();
        }
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        References.canvas.scoreText.text = score.ToString();
    }
}
