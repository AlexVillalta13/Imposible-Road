using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();

        scoreText.text = "0";
    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString();
    }
}
