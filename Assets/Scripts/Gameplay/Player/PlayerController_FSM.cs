using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;

public class PlayerController_FSM : MonoBehaviour
{
    // CONFIG DATA
    [SerializeField] float rotationVelocity = 100f;

    [SerializeField] float magnitudVelocity = 10f;
    [SerializeField] float maxFallingVelocity = 10f;

    [SerializeField] float timeToDie = 4f;
    public float TimeToDie { get { return timeToDie; } }
    public float countdownToDie { get; set; }
    public bool canDie = true;

    [SerializeField] float bounceForce = 50f;
    [SerializeField] float timeToRotateLanded = 0.2f;
    public float TimeToRotateLanded { get { return timeToRotateLanded; } }
    public float countdownToRotate { get; set; }
    public Quaternion landedRotation { get; set; }

    float rotationInput;

    // CACHE
    //private Rigidbody rigid;
    private Rigidbody rigid;

    [SerializeField] ForwardPointer directionTransform = null;
    public ForwardPointer DirectionTransform { get { return directionTransform; } }

    [SerializeField] Image fadeInDeathImage = null;

    [SerializeField] LayerMask rampLayer = 8;
    public LayerMask RampLayer { get { return rampLayer; } }

    PlayerInputTouchHandler playerInput;
    GameLoopManager loopManager;

    // STATES
    public PlayerBaseState CurrentState { get; private set; }

    public readonly PlayerRunningState RunningState = new PlayerRunningState();
    public readonly PlayerFallingState FallingState = new PlayerFallingState();
    public readonly PlayerLandedState landedState = new PlayerLandedState();
    public readonly PlayerWaitingToPlay waitingToPlay = new PlayerWaitingToPlay();

    public void TransitionToState(PlayerBaseState state)
    {
        CurrentState = state;
        CurrentState.EnterState(this);
    }

    private void Awake()
    {
        countdownToDie = Mathf.Infinity;
        countdownToRotate = Mathf.Infinity;

        CurrentState = waitingToPlay;
        TouchSimulation.Enable();

        rigid = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInputTouchHandler>();
        loopManager = FindObjectOfType<GameLoopManager>();
    }

    private void Update()
    {
        CurrentState.Update(this);

        rotationInput = playerInput.GetTouch();
    }

    private void FixedUpdate()
    {
        CurrentState.FixedUpdate(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        CurrentState.OnCollisionEnter(this, collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        ScoreBox scoreBox = other.GetComponent<ScoreBox>();

        if(scoreBox == null) { return; }

        scoreBox.SumScore();
    }

    public void RotatePlayer()
    {
        directionTransform.Rotate(rotationVelocity * rotationInput);
    }

    public void SetVelocity()
    {
        Vector3 velocityVector = directionTransform.transform.forward * magnitudVelocity;
        float yVelocity = rigid.velocity.y;
        if (yVelocity < -maxFallingVelocity)
        {
            yVelocity = -maxFallingVelocity;
        }
        velocityVector.y = yVelocity;

        rigid.velocity = velocityVector;
    }

    public void SetAlphaDeathImage(float alpha)
    {
        Color temp = fadeInDeathImage.color;
        temp.a = alpha;
        fadeInDeathImage.color = temp;
    }

    public void Bounce()
    {
        rigid.velocity = Vector3.zero;

        rigid.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
    }

    // EDITOR ONLY
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 4f);
    }
}
