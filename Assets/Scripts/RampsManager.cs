using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampsManager : MonoBehaviour
{
    public Ramp[] rampsPrefab;
    public Ramp initialRamp;
    private Ramp newRamp;
    private Ramp oldRamp;

    [SerializeField] int rampsToInstantiate = 10;

    private void Start()
    {
        oldRamp = initialRamp;
        newRamp = Instantiate(rampsPrefab[Random.Range(0, rampsPrefab.Length)], transform);
        newRamp.transform.position = oldRamp.nextSpawnPosition;
        newRamp.transform.rotation = oldRamp.nextSpawnRotation;
        oldRamp = newRamp;

        for (int i = 0; i < rampsToInstantiate; i++)
        {
            newRamp = Instantiate(rampsPrefab[Random.Range(0, rampsPrefab.Length)], transform);
            newRamp.transform.position = oldRamp.nextSpawnPosition;
            newRamp.transform.rotation = oldRamp.nextSpawnRotation;
            oldRamp = newRamp;
        }
    }
}
