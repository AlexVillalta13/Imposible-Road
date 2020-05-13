using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int bestScore;
    int runScore;

    GameLoopManager loopManager;

    private event Action<int> onScoreUpdated;

    public void RegisterOnScoreUpdatedCallback(Action<int> callback)
    {
        onScoreUpdated += callback;
    }

    public void UnregisterOnScoreUpdatedCallback(Action<int> callback)
    {
        onScoreUpdated -= callback;
    }

    public void FireOnScoreUpdatedEvent()
    {
        onScoreUpdated(runScore);
    }

    private void Awake()
    {
        loopManager = FindObjectOfType<GameLoopManager>();
    }

    private void OnEnable()
    {
        loopManager.RegisterOnStartGameCallback(ResetScore);
    }

    private void OnDisable()
    {
        loopManager.UnregisterOnStartGameCallback(ResetScore);
    }

    private void ResetScore()
    {
        runScore = 0;
        if(onScoreUpdated != null) 
        {
            onScoreUpdated(runScore);
        }
    }

    public void AddScore(int scoreToAdd)
    {
        runScore = scoreToAdd;
        if (onScoreUpdated != null)
        {
            onScoreUpdated(runScore);
        }
    }

    void UpdateBestScore()
    {
        if(runScore > bestScore) 
        {
            bestScore = runScore;
        }
    }
}
