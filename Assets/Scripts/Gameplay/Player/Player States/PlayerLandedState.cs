using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandedState : PlayerBaseState
{
    public override void EnterState(PlayerController_FSM player)
    {
        player.canRotate = false;
        player.Bounce();
        player.countdownToRotate = 0f;

        player.StartCoroutine(Rotate(player));
    }

    public override void FixedUpdate(PlayerController_FSM player)
    {
        
    }

    public override void OnCollisionEnter(PlayerController_FSM player, Collision collision)
    {
        
    }

    public override void Update(PlayerController_FSM player)
    {

    }

    private IEnumerator Rotate(PlayerController_FSM player)
    {
        while (player.countdownToRotate < player.TimeToRotateLanded)
        {
            player.countdownToRotate += Time.deltaTime;
            Quaternion step = Quaternion.RotateTowards(player.DirectionTransform.transform.rotation, player.landedRotation, 750f * Time.deltaTime);
            player.DirectionTransform.SetRotation(step.eulerAngles);
            yield return null;
        }

        player.TransitionToState(player.RunningState);
    }
}
