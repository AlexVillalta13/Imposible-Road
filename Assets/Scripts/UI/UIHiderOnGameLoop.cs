using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHiderOnGameLoop : MonoBehaviour
{
    GameLoopManager gameLoopManager;

    [SerializeField] Transform mainMenu = null;
    [SerializeField] Transform gameplayUI = null;
    [SerializeField] Transform scoreScreen = null;
    [SerializeField] Transform skinShop = null;
    [SerializeField] Transform skinAcquiredUI = null;
    [SerializeField] Transform openBoxScreen = null;

    private void Awake()
    {
        gameLoopManager = FindObjectOfType<GameLoopManager>();
    }

    private void Start()
    {
        DisableUI(gameplayUI);
        DisableUI(scoreScreen);
        EnableUI(mainMenu);
        DisableUI(skinShop);
        DisableUI(skinAcquiredUI);
        DisableUI(openBoxScreen);
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
        DisableUI(mainMenu);
        EnableUI(gameplayUI);
    }

    private void EnterScoreScreen()
    {
        DisableUI(gameplayUI);
        EnableUI(scoreScreen);
    }

    private void EnterMainMenu()
    {
        EnableUI(mainMenu);
        DisableUI(skinShop);
        DisableUI(scoreScreen);
    }

    public void EnableUI(Transform UItransform)
    {
        foreach(Transform transform in UItransform)
        {
            transform.gameObject.SetActive(true);
        }
    }

    public void DisableUI(Transform UItransform)
    {
        foreach (Transform transform in UItransform)
        {
            transform.gameObject.SetActive(false);
        }
    }
}
