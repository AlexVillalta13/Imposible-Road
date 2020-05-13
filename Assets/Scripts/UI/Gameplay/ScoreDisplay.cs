using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
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
        scoreManager.RegisterOnScoreUpdatedCallback(UpdateScoreText);
        scoreManager.FireOnScoreUpdatedEvent();
    }

    private void OnDisable()
    {
        scoreManager.UnregisterOnScoreUpdatedCallback(UpdateScoreText);
    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString();
    }
}
