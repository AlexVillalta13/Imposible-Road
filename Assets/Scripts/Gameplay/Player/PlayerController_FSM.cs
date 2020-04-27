using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController_FSM : MonoBehaviour
{
    // CONFIG DATA
    [SerializeField] float rotationVelocity = 100f;

    [SerializeField] float magnitudVelocity = 10f;
    [SerializeField] float maxYVelocity = 10f;

    [SerializeField] float timeToDie = 5f;
    public float TimeToDie { get { return timeToDie; } }
    public float countdownToDie = Mathf.Infinity;
    public bool canDie = true;

    [SerializeField] ForwardPointer directionTransform = null;
    public ForwardPointer DirectionTransform { get { return directionTransform; } }

    [SerializeField] Image fadeInDeathImage = null;

    [SerializeField] LayerMask rampLayer = 8;
    public LayerMask RampLayer { get { return rampLayer; } }

    [SerializeField] float bounceForce = 50f;
    [SerializeField] float timeToRotateLanded = 0.2f;
    public float TimeToRotateLanded { get { return timeToRotateLanded; } }
    public float countdownToRotate = Mathf.Infinity;
    public Quaternion landedRotation;

    // CACHE
    private Rigidbody rigid;

    // STATES
    public PlayerBaseState CurrentState { get; private set; }

    public readonly PlayerRunningState RunningState = new PlayerRunningState();
    public readonly PlayerFallingState FallingState = new PlayerFallingState();
    public readonly PlayerLandedState landedState = new PlayerLandedState();

    private void Awake()
    {
        CurrentState = RunningState;
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CurrentState.Update(this);
    }

    private void FixedUpdate()
    {
        CurrentState.FixedUpdate(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        CurrentState.OnCollisionEnter(this, collision);
    }

    public void GetInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            directionTransform.Rotate(-rotationVelocity);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            directionTransform.Rotate(rotationVelocity);
        }
    }

    public void SetVelocity()
    {
        Vector3 velocityVector = directionTransform.transform.forward * magnitudVelocity;
        float yVelocity = rigid.velocity.y;
        if (yVelocity < -maxYVelocity)
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

    public void Bounce()
    {
        Vector3 currentVelocity = rigid.velocity;
        rigid.velocity = Vector3.zero;

        rigid.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 4f);
    }
}
