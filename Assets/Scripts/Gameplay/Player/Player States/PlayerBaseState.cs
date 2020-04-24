using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public abstract class PlayerBaseState
{
    public abstract void EnterState(PlayerController_FSM player);
    public abstract void Update(PlayerController_FSM player);
    public abstract void FixedUpdate(PlayerController_FSM player);
    public abstract void OnCollisionEnter(PlayerController_FSM player, Collision collision);
}
