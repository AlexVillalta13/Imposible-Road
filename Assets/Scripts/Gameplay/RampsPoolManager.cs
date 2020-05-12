using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampsPoolManager : MonoBehaviour
{
    [SerializeField] List<Ramp> rampsPrefab = new List<Ramp>();
    List<Ramp> rampsToInstantiateList = new List<Ramp>();
    List<Ramp> rampsPool = new List<Ramp>();
    List<Ramp> rampsSpawned = new List<Ramp>();


    [SerializeField] Ramp initialRamp = null;
    Vector3 initialRampPosition;
    Quaternion initialRampRotation;
    private Ramp newRamp;
    private Ramp oldRamp;

    [SerializeField] int rampsInitSpawn = 10;

    PlayerController_FSM player;
    ScoreManager scoreManager;
    GameLoopManager loopManager;

    int currentScoreBoxSum = 1;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController_FSM>();
        scoreManager = FindObjectOfType<ScoreManager>();
        loopManager = FindObjectOfType<GameLoopManager>();

        initialRampPosition = initialRamp.transform.position;
        initialRampRotation = initialRamp.transform.rotation;
    }

    private void OnEnable()
    {
        loopManager.RegisterOnLoseGameCallback(ResetRamps);
    }

    private void OnDisable()
    {
        loopManager.UnregisterOnLoseGameCallback(ResetRamps);
    }

    private void Start()
    {
        InitialisePool();
    }

    private void InitialisePool()
    {
        oldRamp = initialRamp;
        initialRamp.Init(this, player, scoreManager);


        for (int i = 0; i < rampsPrefab.Count; i++)
        {
            rampsToInstantiateList.Add(Instantiate(rampsPrefab[i], transform));
            rampsPool.Add(rampsToInstantiateList[i]);
            rampsToInstantiateList[i].gameObject.SetActive(false);
            rampsToInstantiateList[i].Init(this, player, scoreManager);
        }

        InstantiateFirstRamps();
    }

    private void InstantiateFirstRamps()
    {
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
        return rampsToInstantiateList[Random.Range(0, rampsToInstantiateList.Count)];
    }

    private void Spawn(Ramp rampToSpawn)
    {
        if (rampsPool.Contains(rampToSpawn))
        {
            int i = rampsPool.IndexOf(rampToSpawn);

            newRamp = rampsPool[i];
            rampsPool.RemoveAt(i);
            rampsSpawned.Add(newRamp);

            newRamp.gameObject.SetActive(true);
            newRamp.ActivateScoreBoxes(ref currentScoreBoxSum);
        }
        else
        {
            int i = rampsToInstantiateList.IndexOf(rampToSpawn);
            newRamp = Instantiate(rampsToInstantiateList[i], transform);
            newRamp.Init(this, player, scoreManager);
            newRamp.ActivateScoreBoxes(ref currentScoreBoxSum);

            rampsSpawned.Add(newRamp);
        }


        newRamp.transform.position = oldRamp.nextSpawnPosition;
        newRamp.transform.rotation = oldRamp.nextSpawnRotation;
        oldRamp = newRamp;
    }


    public void DeSpawn(Ramp ramp) 
    {
        rampsPool.Add(ramp);
        rampsSpawned.Remove(ramp);

        ramp.gameObject.SetActive(false);
        Spawn(GetRandomRamp());
    }

    private void ResetRamps()
    {
        foreach(Ramp ramp in rampsSpawned)
        {
            rampsPool.Add(ramp);
            ramp.gameObject.SetActive(false);
        }
        rampsSpawned.Clear();

        oldRamp = initialRamp;
        initialRamp.transform.position = initialRampPosition;
        initialRamp.transform.rotation = initialRampRotation;
        initialRamp.gameObject.SetActive(true);
        InstantiateFirstRamps();
    }
}
