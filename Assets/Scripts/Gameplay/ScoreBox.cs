using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBox : MonoBehaviour
{
    int scoreHolding;

    public void UpdateHoldingScore(int score)
    {
        scoreHolding = score;
    }

    public void SumScore()
    {
        Debug.Log("Score: " + scoreHolding);
    }
}
