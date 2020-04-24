using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunningState : PlayerBaseState
{
    public override void EnterState(PlayerController_FSM player)
    {
        player.SetAlphaDeathImage(0f);
    }
    public override void Update(PlayerController_FSM player)
    {
        
    }
    public override void FixedUpdate(PlayerController_FSM player)
    {
        Raycasting(player);
    }
    public override void OnCollisionEnter(PlayerController_FSM player, Collision collision)
    {
        
    }

    private void Raycasting(PlayerController_FSM player)
    {
        Ray ray = new Ray(player.transform.position, Vector3.down);

        RaycastHit hitInfo;
        Debug.DrawRay(ray.origin, ray.direction * 3f, Color.red);

        if (Physics.Raycast(ray, out hitInfo, 3f) == false)
        {
            player.TransitionToState(player.FallingState);
        }
    }


}
