using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampsManager : MonoBehaviour
{
    [SerializeField] List<Ramp> rampsPrefab = new List<Ramp>();
    List<Ramp> rampsGameObjectsList = new List<Ramp>();
    List<Ramp> rampsPool = new List<Ramp>();

    [SerializeField] Ramp initialRamp = null;
    private Ramp newRamp;
    private Ramp oldRamp;

    [SerializeField] int rampsInitSpawn = 10;

    PlayerController_FSM player = null;

    int currentScoreBoxSum = 1;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController_FSM>();
    }

    private void Start()
    {
        InitialisePool();
    }

    private void InitialisePool()
    {
        oldRamp = initialRamp;
        initialRamp.Init(this, player);


        for (int i = 0; i < rampsPrefab.Count; i++)
        {
            rampsGameObjectsList.Add(Instantiate(rampsPrefab[i], transform));
            rampsPool.Add(rampsGameObjectsList[i]);
            rampsGameObjectsList[i].gameObject.SetActive(false);
            rampsGameObjectsList[i].Init(this, player);
        }

        for (int i = 0; i < rampsInitSpawn; i++)
        {
            Spawn(GetRandomRamp());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Spawn(GetRandomRamp());
        }
    }

    private Ramp GetRandomRamp()
    {
        return rampsGameObjectsList[Random.Range(0, rampsGameObjectsList.Count)];
    }

    private void Spawn(Ramp rampToSpawn)
    {
        if (rampsPool.Contains(rampToSpawn))
        {
            int i = rampsPool.IndexOf(rampToSpawn);

            newRamp = rampsPool[i];
            rampsPool.RemoveAt(i);

            newRamp.gameObject.SetActive(true);
            newRamp.ActivateScoreBoxes(ref currentScoreBoxSum);
        }
        else
        {
            int i = rampsGameObjectsList.IndexOf(rampToSpawn);
            newRamp = Instantiate(rampsGameObjectsList[i], transform);
            newRamp.Init(this, player);
            newRamp.ActivateScoreBoxes(ref currentScoreBoxSum);
        }


        newRamp.transform.position = oldRamp.nextSpawnPosition;
        newRamp.transform.rotation = oldRamp.nextSpawnRotation;
        oldRamp = newRamp;
    }


    public void DeSpawn(Ramp ramp) 
    {
        rampsPool.Add(ramp);
        ramp.gameObject.SetActive(false);
        Spawn(GetRandomRamp());
    }
}
