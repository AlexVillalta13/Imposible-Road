using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBox : MonoBehaviour
{
    int scoreHolding;

    ScoreManager scoreManager;

    public void Init(ScoreManager scoreManager)
    {
        this.scoreManager = scoreManager;
    }

    public void UpdateHoldingScore(int score)
    {
        scoreHolding = score;
    }

    public void SumScore()
    {
        scoreManager.AddScore(scoreHolding);
    }
}
