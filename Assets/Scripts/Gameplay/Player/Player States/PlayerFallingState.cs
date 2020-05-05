using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFallingState : PlayerBaseState
{
    public override void EnterState(PlayerController_FSM player)
    {
        //player.canRotate = true;
        player.countdownToDie = player.TimeToDie;
    }
    public override void Update(PlayerController_FSM player)
    {
        player.RotatePlayer();

        if (player.canDie)
        {
            player.countdownToDie -= Time.deltaTime;

            float alpha = Mathf.Abs(player.countdownToDie / player.TimeToDie - 1f);
            player.SetAlphaDeathImage(alpha);

            if (player.countdownToDie <= 0f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
    public override void FixedUpdate(PlayerController_FSM player)
    {
        player.SetVelocity();
    }

    public override void OnCollisionEnter(PlayerController_FSM player, Collision collision)
    {
        Vector3 normal = collision.GetContact(0).normal;
        Vector3 collisionPoint = collision.GetContact(0).point;
        Vector3 normalInWorld = collisionPoint + normal;

        Vector3 direction = normalInWorld - collisionPoint;
        Vector3 directionInLocal = player.transform.TransformDirection(direction);

        Quaternion rotation = Quaternion.LookRotation(direction);
        Vector3 v = rotation.eulerAngles;
        v.x = 0f;

        player.landedRotation = Quaternion.Euler(v);

        Debug.DrawRay(collision.GetContact(0).point, normal, Color.red, 10f);


        player.TransitionToState(player.landedState);
    }
}
