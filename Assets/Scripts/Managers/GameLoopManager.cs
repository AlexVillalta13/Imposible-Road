using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopManager : MonoBehaviour
{
    event Action onStartGame;
    event Action onLoseGame;
    event Action onReturnToMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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

    public void LoseGame()
    {
        onLoseGame();
    }

    public void ReturnToMenu()
    {
        onReturnToMenu();
    }
}
