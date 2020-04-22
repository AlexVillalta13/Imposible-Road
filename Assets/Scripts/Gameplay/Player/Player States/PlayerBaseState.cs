using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public abstract class PlayerBaseState
{
    public abstract void Update(PlayerController_FSM player);
    public abstract void EnterState(PlayerController_FSM player);
}
