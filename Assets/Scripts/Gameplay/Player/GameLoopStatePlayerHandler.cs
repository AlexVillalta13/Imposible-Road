using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopStatePlayerHandler : MonoBehaviour
{
    GameLoopManager loopManager;
    Rigidbody rigid;
    PlayerController_FSM player;

    private void Awake()
    {
        loopManager = FindObjectOfType<GameLoopManager>();
        player = GetComponent<PlayerController_FSM>();
        rigid = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        loopManager.RegisterOnStartGameCallback(PlayerStartGame);
        loopManager.RegisterOnLoseGameCallback(PlayerDies);
        loopManager.RegisterOnReturnToMenuCallback(PlayerInInitialState);

    }

    private void OnDisable()
    {
        loopManager.UnregisterOnStartGameCallback(PlayerStartGame);
        loopManager.UnregisterOnLoseGameCallback(PlayerDies);
        loopManager.UnregisterOnReturnToMenuCallback(PlayerInInitialState);
    }

    private void PlayerInInitialState()
    {
        //rigid.isKinematic = true;
        //TransitionToState(waitingToPlay);
        //transform.position = Vector3.zero;
        //transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    private void PlayerStartGame()
    {
        rigid.isKinematic = false;
        player.TransitionToState(player.RunningState);
    }

    public void FireStartGameEvent()
    {
        loopManager.FireStartGameEvent();
    }

    private void PlayerDies()
    {
        rigid.isKinematic = true;
        player.TransitionToState(player.waitingToPlay);
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        player.ForwardPointer.SetRotation(Vector3.zero);
    }

    public void FireDieEvent()
    {
        loopManager.FirePlayerDiesEvent();
    }
}
