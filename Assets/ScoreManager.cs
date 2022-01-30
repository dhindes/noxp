using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public int highScore;
    public float secondsToShowRecentScore;
    int recentScore;
    float secondsSinceShowingRecentScore;

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
        recentScore += amount;
        References.canvas.recentScoreText.text = "+" + recentScore.ToString();
        secondsSinceShowingRecentScore = 0;
        References.canvas.scoreText.text = score.ToString();
        if (recentScore > 0)
        {
            References.canvas.recentScoreText.enabled = true;
        }
    }

    private void Update()
    {
        secondsSinceShowingRecentScore += Time.deltaTime;
        if (secondsSinceShowingRecentScore >= secondsToShowRecentScore)
        {
            recentScore = 0;
            References.canvas.recentScoreText.enabled = false;
        }
    }
}
