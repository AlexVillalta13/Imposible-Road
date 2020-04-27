using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardPointer : MonoBehaviour
{
    [SerializeField] Vector3 rotation;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(rotation);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(rotation), 1000 * Time.deltaTime);
    }

    public void Rotate(float rotationVelocity)
    {
        rotation.y += rotationVelocity * Time.deltaTime;
    }

    public void SetRotation(Vector3 newRotation)
    {
        rotation = newRotation;
    }

    public Quaternion GetForwardRotation()
    {
        return Quaternion.Euler(rotation);
    }
}
