﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ramp : MonoBehaviour
{
    [SerializeField] Transform nextSpawnTransform = null;

    float heightToDestroy = 5f;

    PlayerController_FSM player = null;
    RampsPoolManager rampsManager = null;

    List<ScoreBox> scoreBoxes = new List<ScoreBox>();
    List<Gem> gems = new List<Gem>();

    public Vector3 nextSpawnPosition { get { return nextSpawnTransform.position; } }
    public Quaternion nextSpawnRotation{get { return nextSpawnTransform.rotation;} }

    private void Awake()
    {
        scoreBoxes = GetComponentsInChildren<ScoreBox>().ToList();
        gems = GetComponentsInChildren<Gem>().ToList();
    }

    public void Init(RampsPoolManager rampsManager, PlayerController_FSM player, ScoreManager scoreManager, GemsManager gemsManager) 
    {
        this.rampsManager = rampsManager;
        this.player = player;

        if (scoreBoxes != null)
        {
            foreach (ScoreBox scoreBox in scoreBoxes)
            {
                scoreBox.Init(scoreManager);
            }
        }

        if (gems != null)
        {
            foreach (Gem gem in gems)
            {
                gem.Init(gemsManager);
            }
        }
    }

    public void ReactivateRampItems(ref int currentScoreBoxSum)
    {
        if (scoreBoxes != null)
        {
            foreach (ScoreBox scoreBox in scoreBoxes)
            {
                scoreBox.UpdateHoldingScore(currentScoreBoxSum);
                currentScoreBoxSum++;
            }
        }

        if(gems != null) 
        {
            foreach (Gem gem in gems)
            {
                gem.ActivateMeshGameObject();
            }
        }
    }

    private void Update()
    {
        if (nextSpawnPosition.y - heightToDestroy > player.transform.position.y)
        {
            rampsManager.DeSpawn(this);
        }
    }
}
