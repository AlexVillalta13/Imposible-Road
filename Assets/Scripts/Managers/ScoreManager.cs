using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int currentScore;
    int runScore;

    ScoreDisplay scoreDisplay;

    private void Awake()
    {
        scoreDisplay = FindObjectOfType<ScoreDisplay>();
    }

    public void AddScore(int scoreToAdd)
    {
        runScore = scoreToAdd;
        scoreDisplay.UpdateScoreText(runScore);
    }
}
