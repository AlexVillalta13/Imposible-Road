using Bayat.SaveSystem;
using Bayat.SaveSystem.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    string identifier = "ScoreManager.txt";

    int bestScore = -1;
    int runScore = -1;

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

    private event Action<int> onBestScoreUpdated;

    public void RegisterOnBestScoreUpdatedCallback(Action<int> callback)
    {
        onBestScoreUpdated += callback;
    }

    public void UnregisterOnBestScoreUpdatedCallback(Action<int> callback)
    {
        onBestScoreUpdated -= callback;
    }

    public void FireOnBestScoreUpdatedEvent()
    {
        onBestScoreUpdated(bestScore);
    }

    private void Awake()
    {
        loopManager = FindObjectOfType<GameLoopManager>();
    }

    private void OnEnable()
    {
        loopManager.RegisterOnStartGameCallback(ResetScore);
        loopManager.RegisterOnLoseGameCallback(UpdateBestScore);
    }

    private void OnDisable()
    {
        loopManager.UnregisterOnStartGameCallback(ResetScore);
        loopManager.UnregisterOnLoseGameCallback(UpdateBestScore);
    }

    async private void Start()
    {
        
        if (await SaveSystemAPI.ExistsAsync(identifier) == false)
        {
            return;
        }
        int loadedScore = await SaveSystemAPI.LoadAsync<int>(identifier);
        bestScore = loadedScore;
    }

    private void ResetScore()
    {
        runScore = 0;
        UpdateRunScoreUI();
    }

    public void AddScore(int scoreToAdd)
    {
        runScore = scoreToAdd;
        UpdateRunScoreUI();
    }

    private void UpdateRunScoreUI()
    {
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

        if(onBestScoreUpdated != null)
        {
            onBestScoreUpdated(bestScore);
        }

        SaveSystemAPI.SaveAsync(identifier, bestScore);
        
    }
}
