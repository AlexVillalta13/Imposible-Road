using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopManager : MonoBehaviour
{
    event Action onStartGame;
    event Action onLoseGame;
    event Action onReturnToMenu;

    public void RegisterOnStartGameCallback(Action callback) 
    {
        onStartGame += callback;
    }

    public void UnregisterOnStartGameCallback(Action callback)
    {
        onStartGame -= callback;
    }

    public void StartGame()
    {
        onStartGame();
    }

    public void RegisterOnLoseGameCallback(Action callback)
    {
        onLoseGame += callback;
    }

    public void UnregisterOnLoseGameCallback(Action callback)
    {
        onLoseGame -= callback;
    }

    public void PlayerDies()
    {
        onLoseGame();
    }

    public void RegisterOnReturnToMenuCallback(Action callback)
    {
        onReturnToMenu += callback;
    }

    public void UnregisterOnReturnToMenuCallback(Action callback)
    {
        onReturnToMenu -= callback;
    }

    public void ReturnToMenu()
    {
        onReturnToMenu();
    }
}
