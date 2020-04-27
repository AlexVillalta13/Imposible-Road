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
        player.GetInput();
    }
    public override void FixedUpdate(PlayerController_FSM player)
    {
        player.SetVelocity();
        Raycasting(player);
    }
    public override void OnCollisionEnter(PlayerController_FSM player, Collision collision)
    {
        
    }

    private void Raycasting(PlayerController_FSM player)
    {
        if (Physics.CheckSphere(player.transform.position, 4f, player.RampLayer) == false)
        {
            Debug.Log("Transition to Falling");
            player.TransitionToState(player.FallingState);
        }


        //Collider[] hitInfo =  Physics.OverlapSphere(player.transform.position, 1f);

        //foreach (Collider collider in hitInfo)
        //{
        //    if (collider.transform.GetComponentInParent<Ramp>())
        //    {
        //        return;
        //    }
        //}
    }


}
