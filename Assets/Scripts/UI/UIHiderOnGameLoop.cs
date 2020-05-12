using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHiderOnGameLoop : MonoBehaviour
{
    GameLoopManager gameLoopManager;

    MainMenuHandler mainMenuHandler;
    GameplayUIHandler gameplayUIHandler;
    ScoreScreenHandler scoreScreenHandler;

    private void Awake()
    {
        gameLoopManager = FindObjectOfType<GameLoopManager>();

        mainMenuHandler = GetComponentInChildren<MainMenuHandler>();
        gameplayUIHandler = GetComponentInChildren<GameplayUIHandler>();
        scoreScreenHandler = GetComponentInChildren<ScoreScreenHandler>();
    }

    private void Start()
    {
        gameplayUIHandler.Enable(false);
        scoreScreenHandler.Enable(false);
    }

    private void OnEnable()
    {
        gameLoopManager.RegisterOnStartGameCallback(GameStarts);
        gameLoopManager.RegisterOnLoseGameCallback(EnterScoreScreen);
        gameLoopManager.RegisterOnReturnToMenuCallback(EnterMainMenu);
    }

    private void OnDisable()
    {
        gameLoopManager.UnregisterOnStartGameCallback(GameStarts);
        gameLoopManager.UnregisterOnLoseGameCallback(EnterScoreScreen);
        gameLoopManager.UnregisterOnReturnToMenuCallback(EnterMainMenu);
    }

    private void GameStarts()
    {
        mainMenuHandler.Enable(false);
        gameplayUIHandler.Enable(true);
    }

    private void EnterScoreScreen()
    {
        gameplayUIHandler.Enable(false);
        scoreScreenHandler.Enable(true);
    }

    private void EnterMainMenu()
    {
        mainMenuHandler.Enable(true);
        scoreScreenHandler.Enable(false);
    }
}
