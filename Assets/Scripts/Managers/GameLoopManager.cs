using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopManager : MonoBehaviour
{
    event Action onStartGame;
    event Action onPlayerDies;
    event Action onReturnToMenu;

    public void RegisterOnStartGameCallback(Action callback) 
    {
        onStartGame += callback;
    }

    public void UnregisterOnStartGameCallback(Action callback)
    {
        onStartGame -= callback;
    }

    public void FireStartGameEvent()
    {
        if(onStartGame != null)
        {
            onStartGame();
        }
    }

    public void RegisterOnLoseGameCallback(Action callback)
    {
        onPlayerDies += callback;
    }

    public void UnregisterOnLoseGameCallback(Action callback)
    {
        onPlayerDies -= callback;
    }

    public void FirePlayerDiesEvent()
    {
        if(onPlayerDies != null)
        {
            onPlayerDies();
        }
    }

    public void RegisterOnReturnToMenuCallback(Action callback)
    {
        onReturnToMenu += callback;
    }

    public void UnregisterOnReturnToMenuCallback(Action callback)
    {
        onReturnToMenu -= callback;
    }

    public void FireReturnToMenuEvent()
    {
        if(onReturnToMenu != null)
        {
            onReturnToMenu();
        }
    }
}
