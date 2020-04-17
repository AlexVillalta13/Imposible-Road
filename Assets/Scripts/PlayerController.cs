using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] float rotationVelocity = 30f;

    [SerializeField] float magnitudVelocity = 10f;
    [SerializeField] float maxYVelocity = 10f;

    [SerializeField] Transform directionTransform = null;

    private Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        SetVelocity();
    }

    private void SetVelocity()
    {
        Vector3 velocityVector = directionTransform.forward * magnitudVelocity;
        float yVelocity = rigid.velocity.y;
        if (Mathf.Abs(yVelocity) > maxYVelocity)
        {
            yVelocity = -maxYVelocity;
        }
        velocityVector.y = yVelocity;

        rigid.velocity = velocityVector;
    }
}
