using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BestScoreDisplay : MonoBehaviour
{
    ScoreManager scoreManager;

    TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();

        scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        scoreManager.RegisterOnBestScoreUpdatedCallback(SetBestScoreText);
        scoreManager.FireOnBestScoreUpdatedEvent();
    }

    private void OnDisable()
    {
        scoreManager.UnregisterOnBestScoreUpdatedCallback(SetBestScoreText);
    }

    public void SetBestScoreText(int score)
    {
        scoreText.text = "Best: " + score.ToString();
    }
}
