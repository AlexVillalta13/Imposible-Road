using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerWaitingToPlay : PlayerBaseState
{
    public override void EnterState(PlayerController_FSM player){ }

    public override void FixedUpdate(PlayerController_FSM player) { }

    public override void OnCollisionEnter(PlayerController_FSM player, Collision collision){ }

    public override void Update(PlayerController_FSM player)
    {
        bool screenIsPressed = Touchscreen.current.press.wasPressedThisFrame;
        if (screenIsPressed && EventSystem.current.IsPointerOverGameObject() == false)
        {
            player.GetComponent<GameLoopStatePlayerHandler>().FireStartGameEvent();
        }
    }
}
