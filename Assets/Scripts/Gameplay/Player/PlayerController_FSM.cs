using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_FSM : MonoBehaviour
{
    [SerializeField] float rotationVelocity = 100f;

    [SerializeField] float magnitudVelocity = 10f;
    [SerializeField] float maxYVelocity = 10f;

    [SerializeField] ForwardPointer directionTransform = null;

    private Rigidbody rigid;

    private PlayerBaseState currentState;
    public PlayerBaseState CurrentState
    {
        get { return currentState; }
    }

    public readonly PlayerRunningState RunningState = new PlayerRunningState();
    public readonly PlayerFallingState FallingState = new PlayerFallingState();

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            directionTransform.Rotate(-rotationVelocity);
        }
        else if(Input.GetKey(KeyCode.D)) 
        {
            directionTransform.Rotate(rotationVelocity);
        }
    }

    private void FixedUpdate()
    {
        SetVelocity();
        Raycasting();
    }

    private void SetVelocity()
    {
        Vector3 velocityVector = directionTransform.transform.forward * magnitudVelocity;
        float yVelocity = rigid.velocity.y;
        if (Mathf.Abs(yVelocity) > maxYVelocity)
        {
            yVelocity = -maxYVelocity;
        }
        velocityVector.y = yVelocity;

        rigid.velocity = velocityVector;
    }

    public void TransitionToState(PlayerBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    private void Raycasting()
    {
        RaycastHit hitInfo;
        Physics.Raycast(transform.position, Vector3.down, out hitInfo, 10f);
        Debug.DrawRay(transform.position, directionTransform.transform.TransformDirection(Vector3.down) * 10f, Color.yellow);
    }
}
