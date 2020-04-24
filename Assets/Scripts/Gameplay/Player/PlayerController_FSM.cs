using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController_FSM : MonoBehaviour
{
    [SerializeField] float rotationVelocity = 100f;

    [SerializeField] float magnitudVelocity = 10f;
    [SerializeField] float maxYVelocity = 10f;

    [SerializeField] float timeToDie = 5f;
    public float TimeToDie { get { return timeToDie; } }
    public float countdownToDie = Mathf.Infinity;
    public bool canDie = true;

    [SerializeField] ForwardPointer directionTransform = null;
    public ForwardPointer DirectionTransform { get { return directionTransform; } }

    private Rigidbody rigid;
    [SerializeField] Image fadeInDeathImage = null;

    public PlayerBaseState CurrentState { get; private set; }

    public readonly PlayerRunningState RunningState = new PlayerRunningState();
    public readonly PlayerFallingState FallingState = new PlayerFallingState();

    private void Awake()
    {
        CurrentState = RunningState;
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CurrentState.Update(this);

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
        CurrentState.FixedUpdate(this);

        SetVelocity();
    }

    private void OnCollisionEnter(Collision collision)
    {
        CurrentState.OnCollisionEnter(this, collision);
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
        CurrentState = state;
        CurrentState.EnterState(this);
    }

    public void SetAlphaDeathImage(float alpha)
    {
        Color temp = fadeInDeathImage.color;
        temp.a = alpha;
        fadeInDeathImage.color = temp;
    }
}
