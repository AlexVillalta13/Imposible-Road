using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramp : MonoBehaviour
{
    [SerializeField] Transform nextSpawnTransform = null;

    public Vector3 nextSpawnPosition
    {
        get
        {
            return nextSpawnTransform.position;
        }
    }

    public Quaternion nextSpawnRotation
    {
        get
        {
            return nextSpawnTransform.rotation;
        }
    }
}
