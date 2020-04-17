using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedRotation : MonoBehaviour
{
    [SerializeField] float rotationVelocity = 5f;

    [SerializeField] Vector3 rotation;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);

        if (Input.GetKey(KeyCode.A))
        {
            rotation.y -= rotationVelocity * Time.deltaTime;
            //rotation = Quaternion.AngleAxis(rotationVelocity * Time.deltaTime, Vector3.up);
            //transform.Rotate(Vector3.up, -rotationVelocity * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotation.y += rotationVelocity * Time.deltaTime;

            //rotation = Quaternion.AngleAxis(-rotationVelocity * Time.deltaTime, Vector3.up);

            //transform.Rotate(Vector3.up, rotationVelocity * Time.deltaTime, Space.World);
        }

        transform.rotation = Quaternion.Euler(rotation);
    }
}
